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

    private void OnEnable()
    {
        if (slotsItem == null)
            slotsItem = selecthotKeyItemPanel.gameObject.GetComponentsInChildren<InventorySlot>();
        if (slotsSpell == null)
            slotsSpell = selectHotKeySpellPanel.gameObject.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slotsItem.Length; i++)
        {
            if (ItemHotKeyManager.instance.hotkeyItems[i] != null)  // have item in hotkey
            {
                slotsItem[i].AddItem(ItemHotKeyManager.instance.hotkeyItems[i]);
            }
            else if (ItemHotKeyManager.instance.hotkeyItems[i] == null) // nothing
            {
                slotsItem[i].ClearSlot();
                slotsItem[i].Item_btn.interactable = true;
            }
        }
    }
    private void Update()
    {
                                        // Item Hot Key
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SelectSlotHotKeyItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            SelectSlotHotKeyItem(1);
                                        // Spell Hot Key
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectSlotHotKeySpell(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectSlotHotKeySpell(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectSlotHotKeySpell(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectSlotHotKeySpell(3);
    }
    public void SelectSlotHotKeyItem(int NumKey)
    {
        ItemHotKeyManager.instance.SetHotKeyItem(NumKey, (Consumable)InventoryUI.selectedItem);
        gameObject.SetActive(false);
    }
    public void SelectSlotHotKeySpell(int NumKey)
    {
        SpellHotKeyManager.instance.SetHotKeySpell(NumKey, (SpellBook)InventoryUI.selectedItem);
        gameObject.SetActive(false);
    }
    public void CancelBtn()
    {
        this.gameObject.SetActive(false);
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectSlotbtn.Select();
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectSlotbtn.OnSelect(null);
    }
}
