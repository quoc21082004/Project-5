using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySO: ScriptableObject 
{
    [Header("Stats Enemy")]
    public float heath;
    public float damage;
    public float defense;
    public float attackTimer;
    public float alertRange;
    public float range;
    public float builetSpeed;
    [Space]
    [SerializeField] public TypeEnemy Type;
    [SerializeField] public bool isDead;
    [SerializeField] public bool isAlert;
    [SerializeField] public bool isAttack;
    [Space]
    [SerializeField] public int goldReward;
    [SerializeField] public int expReward;
    [Space]
    public GrowStat growstats;
    
}
public class GrowStat
{
    public float healthGrow;
    public float damageGrow;
    public float defenseGrow;
}