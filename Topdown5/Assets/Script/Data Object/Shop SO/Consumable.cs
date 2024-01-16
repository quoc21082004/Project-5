using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : ItemSO
{
    public const float HealthPotionCD = 5f;
    public const float ManaPotionCD = 5f;
    public const float StatPotionCD = 0f;
    public ConsumableType consumableType;
    public SpellBookType type;
    public static float GetConsumtableTypeCD(ConsumableType type)
    {
        switch(type)
        {
            case ConsumableType.HealthPotion:
                return HealthPotionCD;
                break;
            case ConsumableType.ManaPotion:
                return ManaPotionCD;
                break;
            case ConsumableType.StatPotion:
                return StatPotionCD;
                break;
            default:
                return -1;
        }
    }
}