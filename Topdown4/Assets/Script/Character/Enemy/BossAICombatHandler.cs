using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BossAICombatHandler : MonoBehaviour
{
    private int idleHash = Animator.StringToHash("Idle");

    [SerializeField] private Boss bossStats;
    [SerializeField] private Animator animator;
    [SerializeField] private float updateInterval;

    [Header("Spell Use")]
    [SerializeField] private List<AbilityHandler> abilityHandlers;
    [SerializeField] private List<AbilityWorkFlowNode> workFlowState1;
    [SerializeField] private List<AbilityWorkFlowNode> workFlowState2;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossStats = GetComponent<Boss>();

    }
    [Serializable]
    public class AbilityHandler
    {
        public SkillSObject rune;
        public float prefixWait;
        public float suffixWait;
        public string animationName;
        public ActiveAbility ability;
        [HideInInspector]
        public bool hasAnimation;
        [HideInInspector]
        public int animationHash;
    }

    [Serializable]
    public class AbilityWorkFlowNode
    {
        public AbilityName abilityToUse;
        public float cooldown;
    }

    public enum AbilityName
    {
        HandFire,
        SpikeFire,
        LazerFire,
        StormSpell,
        DamageZone
    }

    float allowAbilityUseTime;
    int currentAbility;
    public IEnumerator StartCombatState()
    {
        yield return PreStartCombat();
        yield return State1();
        yield return State2();
        yield return EndCombat();
    }

    private IEnumerator PreStartCombat()
    {
        animator.Play("Unimmune");
        InitAbilities();
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator State1()
    {
        allowAbilityUseTime = Time.time;
        currentAbility = 0;
        while (bossStats.heath > 0f)
        {
            yield return AbilityUseCoroutine(workFlowState1);
            yield return new WaitForSeconds(updateInterval);
        }
    }
    private IEnumerator State2()
    {
        allowAbilityUseTime = Time.time;
        currentAbility = 0;
        while (!bossStats.isDead)
        {
            yield return AbilityUseCoroutine(workFlowState2);
            yield return new WaitForSeconds(updateInterval);
        }
    }

    private IEnumerator EndCombat()
    {
        yield return new WaitForSeconds(1.5f);
        animator.Play("Death");
    }

    private void InitAbilities()
    {
        foreach (var handler in abilityHandlers)
        {
            handler.animationHash = Animator.StringToHash(handler.animationName);
            handler.hasAnimation = animator.HasState(0, handler.animationHash);
        }
    }
    private IEnumerator AbilityUseCoroutine(List<AbilityWorkFlowNode> workFlowState)
    {
        if (Time.time > allowAbilityUseTime)
        {
            yield return UseAbility(abilityHandlers[(int)workFlowState[currentAbility].abilityToUse]);
            if (bossStats.isDead)
                yield break;
            allowAbilityUseTime = Time.time + workFlowState[currentAbility].cooldown;
            currentAbility++;
            currentAbility %= workFlowState.Count;
            animator.Play(idleHash);
        }
    }
    private IEnumerator UseAbility(AbilityHandler handler)
    {
        if (handler.hasAnimation)
            animator.Play(handler.animationHash);
        yield return new WaitForSeconds(handler.prefixWait);
        if (bossStats.isDead)
            yield break;
        bossStats.FlipCharacter();
        handler.ability.skillInfo = handler.rune;
        handler.ability.TryUse();
        yield return new WaitForSeconds(handler.suffixWait);
    }
}
