using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemHotKeyManager : Singleton<ItemHotKeyManager>
{
    public int NumOfHotKey = 2;
    public Image[] hotKeyItemIcons;
    public TextMeshProUGUI[] hotKeyItemStack_txt;
    public Image[] hotKeyItemCD;
    public Consumable[] hotkeyItems;

    public Dictionary<ConsumableType, float> consumableCDcounter;
    public Dictionary<ConsumableType, bool> isConsumableonCD;

    private void Start()
    {
        if (hotkeyItems.Length == 0)
            hotkeyItems = new Consumable[NumOfHotKey]; // init hotkey
        if (consumableCDcounter == null)
        {
            consumableCDcounter = new Dictionary<ConsumableType, float>();
            foreach(var type in (ConsumableType[])Enum.GetValues(typeof(ConsumableType)))
            {
                consumableCDcounter.Add(type, 0f);
            }
        }
        if (isConsumableonCD == null)
        {
            isConsumableonCD = new Dictionary<ConsumableType, bool>();
            foreach(var type in (ConsumableType[])Enum.GetValues(typeof(ConsumableType)))
            {
                isConsumableonCD.Add(type, false);
            }
        }
    }
    private void Update()
    {
        UpdateConsumableCoolDown();
        UpdateHotKeyIcons();
    }
    private void UpdateConsumableCoolDown()
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
    public float GetRemainnigCD(Consumable item)
    {
        return consumableCDcounter[item.consumableType];
    }
    public bool IsHotKeyItemOnCoolDown(int hotkeyinput)
    {
        if (hotkeyItems[hotkeyinput] == null)
            return true;
        else
            return isConsumableonCD[hotkeyItems[hotkeyinput].consumableType];
    }
    public void SetHotKeyItem(int numKey, Consumable item)
    {
        for (int i = 0; i < NumOfHotKey; i++)
        {
            if (hotkeyItems[i] == item)
            {
                hotkeyItems[i] = null;
                break;
            }
        }
        hotkeyItems[numKey] = item; 
    }
    public bool IsItemOnCoolDown(Consumable item)
    {
        return isConsumableonCD[item.consumableType];
    }
    public void UseHotKeyItem(int hotkeyNum)
    {
        UseItem(hotkeyItems[hotkeyNum]);
    }
    public void UseItem(Consumable item)
    {
        item.Use();
        item.RemoveFromInventory(1);
        ConsumableType type = item.consumableType;
        isConsumableonCD[type] = true;
        consumableCDcounter[type] = Consumable.GetConsumtableTypeCD(type);
        UpdateHotKeyIcons();
    }
    private void UpdateHotKeyIcons()
    {
        for (int i = 0; i < NumOfHotKey; i++)
        {
            if (hotkeyItems[i] != null)
            {
                hotKeyItemIcons[i].sprite = hotkeyItems[i].icon;
                hotKeyItemIcons[i].enabled = true;
                if (hotkeyItems[i].currentAmt > 0)
                {
                    hotKeyItemStack_txt[i].text = "" + hotkeyItems[i].currentAmt;
                }
                else
                {
                    hotKeyItemStack_txt[i].text = "";
                }
                ConsumableType type = hotkeyItems[i].consumableType;
                hotKeyItemCD[i].fillAmount = consumableCDcounter[type] / Consumable.GetConsumtableTypeCD(type);
            }
            else if (hotkeyItems[i] == null)
            {
                hotKeyItemIcons[i].sprite = null;
                hotKeyItemStack_txt[i].text = "";
                hotKeyItemIcons[i].enabled = false;
                hotKeyItemCD[i].fillAmount = 0;
            }
        }
    }
}
