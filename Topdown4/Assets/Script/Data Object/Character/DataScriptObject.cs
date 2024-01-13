using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataScriptObject : ScriptableObject 
{
    [SerializeField] public static int a = 123;
    public BasicStats basicStats;
    [Space]
    public BasicAttack basicAttack;
    [Space]
    public OtherStats otherStats;
    [Space]
    public LevelUp levelUp;
    [Space]
    public ExtraBuff extraBuff;
    [Space]
    public Upgrade upgrade;
}

#region Stats Full
[System.Serializable]
public class BasicAttack
{
    public float wandDamage;
    public float critChance;
    public float minCritDamage;
    public float maxCritDamage;
    public float attackSpeed;
    public float builetSpeed;
    public float coolDown;
}
[System.Serializable]
public class OtherStats
{
    public int costMP;
    public float damageReduction;
}
[System.Serializable]
public class BasicStats
{
    public float health;
    public float mana;
    public float movementSpeed;
    public float DashSpeed;
    public float healthRegen;
    public float manaRegen;
}
[System.Serializable]
public class LevelUp
{
    public int level;
    public int skillPoint;
    public float exp;
    public float expToLvl;
}
[System.Serializable]
public class Upgrade
{
    public int powerlevel;
    public int magiclevel;
    public int hastelevel;
    public int vitalitylevel;
    public int greedlevel;
}
[System.Serializable]
public class ExtraBuff
{
    public float extraExp;
    public int extraCoins;
    public float percentDamage;
    public float extraSpeedMove;
}
#endregion