using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemHotKeyManager : Singleton<ItemHotKeyManager> , IHotKey
{
    public int NumOfHotKeyItem = 2;
    public Image[] hotKeyItemIcons;
    public TextMeshProUGUI[] hotKeyItemStack_txt;
    public Image[] hotKeyItemCD;
    public Potion[] hotkeyItems;
    public Dictionary<ConsumableType, float> consumableCDcounter;
    public Dictionary<ConsumableType, bool> isConsumableonCD;

    private void Start()
    {
        if (hotkeyItems.Length == 0)
            hotkeyItems = new Potion[NumOfHotKeyItem]; // init hotkey
        if (consumableCDcounter == null)
        {
            consumableCDcounter = new Dictionary<ConsumableType, float>();
            foreach(var type in (ConsumableType[])Enum.GetValues(typeof(ConsumableType)))
                consumableCDcounter.Add(type, 0f);
        }
        if (isConsumableonCD == null)
        {
            isConsumableonCD = new Dictionary<ConsumableType, bool>();
            foreach(var type in (ConsumableType[])Enum.GetValues(typeof(ConsumableType)))
                isConsumableonCD.Add(type, false);
        }
    }
    private void Update()
    {
        UpdateCoolDown();
        UpdateHotKeyIcons();
    }
    public void SetHotKeyItem(int numKey, Potion item)
    {
        for (int i = 0; i < NumOfHotKeyItem; i++)
        {
            if (hotkeyItems[i] == item)
            {
                hotkeyItems[i] = null;
                break;
            }
        }
        hotkeyItems[numKey] = item; 
    }
    public bool IsItemOnCoolDown(Potion item)
    {
        return isConsumableonCD[item.consumableType];
    }
    public void UseItem(Potion item)
    {
        item.Use();
        item.RemoveFromInventory(1);
        ConsumableType type = item.consumableType;
        isConsumableonCD[type] = true;
        consumableCDcounter[type] = Potion.GetConsumtableTypeCD(type);
        UpdateHotKeyIcons();
    }
    private void UpdateHotKeyIcons()
    {
        for (int i = 0; i < NumOfHotKeyItem; i++)
        {
            if (hotkeyItems[i] != null)
            {
                hotKeyItemIcons[i].sprite = hotkeyItems[i].icon;
                hotKeyItemIcons[i].enabled = true;
                if (hotkeyItems[i].currentAmt > 0)
                    hotKeyItemStack_txt[i].text = "" + hotkeyItems[i].currentAmt;
                else
                    hotKeyItemStack_txt[i].text = "";
                ConsumableType type = hotkeyItems[i].consumableType;
                hotKeyItemCD[i].fillAmount = consumableCDcounter[type] / Potion.GetConsumtableTypeCD(type);
            }
            else if (hotkeyItems[i] == null)
            {
                hotKeyItemStack_txt[i].text = "";
                hotKeyItemIcons[i].enabled = true;
                hotKeyItemCD[i].fillAmount = 1;
            }
        }
    }

    public void UpdateCoolDown()
    {
        foreach (var type in (ConsumableType[])Enum.GetValues(typeof(ConsumableType)))
        {
            consumableCDcounter[type] -= Time.deltaTime;
            if (consumableCDcounter[type] < 0)
            {
                consumableCDcounter[type] = 0;
                isConsumableonCD[type] = false;
            }
        }
    }

    public bool IsHotKeyCoolDown(int numkey)
    {
        if (hotkeyItems[numkey] == null)
            return true;
        else
            return isConsumableonCD[hotkeyItems[numkey].consumableType];
    }

    public void UseHotKey(int numkey)
    {
        UseItem(hotkeyItems[numkey]);
    }
}
