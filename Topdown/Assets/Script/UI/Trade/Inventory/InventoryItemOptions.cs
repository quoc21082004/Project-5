using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemOptions : ItemOptions
{
    public GameObject SethotkeyWindow, learnWindow, discardWindow;
    public Button Usebtn, Learnbtn, Hotkeybtn, Discardbtn, Backbtn;
    private void OnEnable()
    {
        Backbtn.onClick.AddListener(() =>
        {
            OnBackButton();
        });

        if (InventoryUI.selectedItem == null)
        {
            Usebtn.interactable = false;
            Hotkeybtn.interactable = false;
            Discardbtn.interactable = false;
            Learnbtn.interactable = false;
        }
        else if (InventoryUI.selectedItem != null) 
        {
            Usebtn.interactable = InventoryUI.selectedItem.GetType().Equals(typeof(Potion));
            Learnbtn.interactable = InventoryUI.selectedItem.GetType().Equals(typeof(SpellBook));
            Hotkeybtn.interactable = InventoryUI.selectedItem.GetType().IsSubclassOf(typeof(Consumable));
            Discardbtn.interactable = true;
        }
        Usebtn.Select();
        Usebtn.OnSelect(null);
    }
    public void UseItem()
    {
        Consumable item = (Consumable)InventoryUI.selectedItem; // ep du lieu (ItemSO) -> Consumable bcs same child class
        if (!ItemHotKeyManager.instance.IsItemOnCoolDown(item))
        {
            ItemHotKeyManager.instance.UseItem(item);
            gameObject.SetActive(false);
            selectSlotbtn.Select();
            selectSlotbtn.OnSelect(null);
        }
    }
    public void OnDiscardItem()
    {
        discardWindow.gameObject.SetActive(true);
        discardWindow.gameObject.GetComponent<AmtConfirmWindow>().InitAmt(1);
        this.gameObject.SetActive(false);
    }
    public void OnSelectHotKeyItem()
    {
        SethotkeyWindow.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void OnLearnAbility()
    {
        learnWindow.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
