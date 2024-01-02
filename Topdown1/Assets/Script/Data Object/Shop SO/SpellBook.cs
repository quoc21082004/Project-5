using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Spell Book")]  
public class SpellBook : Consumable
{
    public SkillSObject spell;
    ActiveAbility ability;
    public static float CD;
    private void Awake()
    {
        if (ability == null)
            ability = PartyController.player.GetComponent<ActiveAbility>();
        CD = spell.baseCoolDown;
    }
    public override void Use()
    {
        base.Use();
        switch(type)
        {
            case SpellBookType.ExplosionCircle:
                spell.isUnlock = true;
                break;
            case SpellBookType.ExplosionBuilet:
                spell.isUnlock = true;
                break;
            case SpellBookType.PoisonZone:
                spell.isUnlock = true;
                break;
            case SpellBookType.LightingCircle:
                spell.isUnlock = true;
                break;
            default:
                break;
        }
        if (spell != null)
        {
            ability.skillInfo = spell;
            ability.TryUse();
        }
    }
    public static float GetSpellCooldown(SpellBookType type)
    {
        switch(type)
        {
            case SpellBookType.ExplosionCircle:
                return CD + 1;
                break;
            case SpellBookType.ExplosionBuilet:
                return CD + 2;
                break;
            case SpellBookType.PoisonZone:
                return CD + 4;
                break;
            case SpellBookType.LightingCircle:
                return CD + 8;
                break;
            default:
                return -1;
        }
    }
}
