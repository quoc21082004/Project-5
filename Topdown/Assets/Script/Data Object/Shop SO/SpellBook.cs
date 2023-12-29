using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Spell Book")]  
public class SpellBook : Consumable
{
    public SkillSObject spell;
    public override void Use()
    {
        if (PartyController.inventoryG.Gold > spell.learnCost)
        base.Use();
        switch(type)
        {
            case SpellBookType.ExplosionCircle:
                Debug.Log("Use :" + type);
                spell.isUnlock = true;
                break;
            case SpellBookType.ExplosionBuilet:
                Debug.Log("Use :" + type);
                spell.isUnlock = true;
                break;
            case SpellBookType.PoisonZone:
                Debug.Log("Use :" + type);
                spell.isUnlock = true;
                break;
            case SpellBookType.LightingCircle:
                Debug.Log("Use :" + type);
                spell.isUnlock = true;
                break;
            default:
                break;
        }
    }
}
