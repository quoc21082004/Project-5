using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SelectHotKeyWindow : MonoBehaviour
{
    public Button cancel_btn;
    public Transform selecthotKeyPanel;
    InventorySlot[] slots;

    private void Start()
    {
        slots = selecthotKeyPanel.gameObject.GetComponentsInChildren<InventorySlot>();
    }
    private void OnEnable()
    {
        cancel_btn.onClick.AddListener(() =>
        {
            CancelBtn();
        });

        if (slots == null)
        {
            slots = selecthotKeyPanel.gameObject.GetComponentsInChildren<InventorySlot>();
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (ItemHotKeyManager.instance.hotkeyItems[i] != null)  // have item in hotkey
            {
                slots[i].AddItem(ItemHotKeyManager.instance.hotkeyItems[i]);
            }
            else if (ItemHotKeyManager.instance.hotkeyItems[i] == null) // nothing
            {
                slots[i].ClearSlot();
                slots[i].Item_btn.interactable = true;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectSlotHotKeyItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectSlotHotKeyItem(1);
    }
    public void SelectSlotHotKeyItem(int NumKey)
    {
        ItemHotKeyManager.instance.SetHotKeyItem(NumKey, (Consumable)InventoryUI.selectedItem);
        gameObject.SetActive(false);
    }
    void CancelBtn()
    {
        this.gameObject.SetActive(false);
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectSlotbtn.Select();
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectSlotbtn.OnSelect(null);
    }
}
