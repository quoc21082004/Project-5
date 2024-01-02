using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ActiveAbility : MonoBehaviour, IActiveAbility
{
    public SkillSObject skillInfo;
    public float NextUseTime { get; protected set; } = Mathf.NegativeInfinity;
    public float CastDelay => skillInfo.baseCastDelay;
    public float MaxUseRange => skillInfo.maxUseRange;
    public float CurrentCoolDown => Mathf.Max(0, NextUseTime - Time.deltaTime);
    public float GetCoolDownTime() => skillInfo.baseCoolDown;
    public bool IsEnoughMana() => PartyController.player.mana >= skillInfo.baseCostMana;
    public bool IsInstantCast() => skillInfo.baseCastDelay <= Mathf.Epsilon;
    public bool IsReady() => Time.time > NextUseTime;
    protected void MarkCoolDownUse()
    {
        NextUseTime = Time.time + GetCoolDownTime();
    }
    public Respond TryUse()
    {
        if (!IsEnoughMana())
            return Respond.NotEnoughMana;
        StartSpell();
        return Respond.Success;
    }
    private void StartSpell()
    {
        if (!IsEnoughMana())   
            return;
        PartyController.player.mana -= skillInfo.baseCostMana;
        if (Instantiate(skillInfo.spellPrefab).TryGetComponent<ISpell>(out var spell))
            spell.KickOff(this, transform.position - PartyController.player.transform.position);
    }
}
