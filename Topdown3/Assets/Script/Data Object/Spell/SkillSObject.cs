using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rune", menuName = "Rune/SkillSO")]
public class SkillSObject : ScriptableObject
{
    public SkillData skillInfo;
    public SpellBookType type;
    public bool isUnlock = false;
    [SerializeField] public int learnCost;
    [SerializeField] public float baseCastDelay;
    [SerializeField] public float baseCostMana;
    [SerializeField] public float baseCoolDown;
    [SerializeField] public float maxUseRange;
    [SerializeField] public float baseDamage;
    public GameObject spellPrefab;
}
