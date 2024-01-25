using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SelectHotKeyWindow : MonoBehaviour
{
    public Transform selecthotKeyItemPanel;
    public Transform selectHotKeySpellPanel;
    InventorySlot[] slotsItem;
    InventorySlot[] slotsSpell;
    bool isPotion, isBookSpell;

    private void OnEnable()
    {
        if (slotsItem == null)
            slotsItem = selecthotKeyItemPanel.gameObject.GetComponentsInChildren<InventorySlot>();
        if (slotsSpell == null)
            slotsSpell = selectHotKeySpellPanel.gameObject.GetComponentsInChildren<InventorySlot>();

        isPotion = InventoryUI.selectedItem.GetType().Equals(typeof(Potion));
        isBookSpell = InventoryUI.selectedItem.GetType().Equals(typeof(SpellBook));

        for (int i = 0; i < slotsItem.Length; i++)
        {
            if (ItemHotKeyManager.instance.hotkeyItems[i] != null)  // have item in hotkey
                slotsItem[i].AddItem(ItemHotKeyManager.instance.hotkeyItems[i]);
            else if (ItemHotKeyManager.instance.hotkeyItems[i] == null) // nothing
            {
                slotsItem[i].ClearSlot();
                slotsItem[i].Item_btn.interactable = true;
            }
        }
        for (int i = 0; i < slotsSpell.Length; i++)
        {
            if (SpellHotKeyManager.instance.hotkeySpell[i] != null)
                slotsSpell[i].AddItem(SpellHotKeyManager.instance.hotkeySpell[i]);
            else
            {
                slotsSpell[i].ClearSlot();
                slotsSpell[i].Item_btn.interactable = true;
            }
        }
    }
    private void Update()
    {
                                        // Item Hot Key
        if (isPotion)
        {
            if (Input.GetKeyDown(KeyCode.X))
                SelectSlotHotKeyItem(0);
            if (Input.GetKeyDown(KeyCode.C))
                SelectSlotHotKeyItem(1);
        }
                                        // Spell Hot Key
        if (isBookSpell)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SelectSlotHotKeySpell(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SelectSlotHotKeySpell(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SelectSlotHotKeySpell(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                SelectSlotHotKeySpell(3);
        }
    }
    public void SelectSlotHotKeyItem(int NumKey)
    {
        AudioManager.instance.PlaySfx("Click");
        Potion potion = (Potion)InventoryUI.selectedItem;
        if (potion != null)
        {
            ItemHotKeyManager.instance.SetHotKeyItem(NumKey, potion);
            gameObject.SetActive(false);
        }
    }
    public void SelectSlotHotKeySpell(int NumKey)
    {
        AudioManager.instance.PlaySfx("Click");
        SpellBook spellbook = (SpellBook)InventoryUI.selectedItem;
        if (spellbook != null)
        {
            SpellHotKeyManager.instance.SetHotKeySpell(NumKey, spellbook);
            gameObject.SetActive(false);
        }
    }
    public void CancelBtn()
    {
        AudioManager.instance.PlaySfx("Click");
        this.gameObject.SetActive(false);
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectSlotbtn.Select();
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectSlotbtn.OnSelect(null);
    }
}
