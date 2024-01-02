using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SkillData : IItem
{
    public string Name;
    [TextArea] public string Description;
    public Sprite Icon;
    public Sprite AbilityIcon;
    public ItemRarity Rarity;
    public Action onDestroy;
    public void Destroy()
    {
        onDestroy?.Invoke();
    }
    public string GetDescription()
    {
        throw new NotImplementedException();
    }
}
