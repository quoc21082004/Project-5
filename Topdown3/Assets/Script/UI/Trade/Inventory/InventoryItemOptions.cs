using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable] 
public class GameObjectWindow
{
    public GameObject cannotHotKeyWindow;
}
public class InventoryItemOptions : ItemOptions
{
    public GameObject hotKeyWindow, setKeyItemWindow, setkeySpellWindow, LearnWindow, NotlearnWindow, CompeleteLearnWindow, discardWindow;
    public Button Usebtn, Learnbtn, Hotkeybtn, Discardbtn, Backbtn;
    public GameObjectWindow objectWindow;
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
        AudioManager.instance.PlaySfx("Click");
        Potion item = (Potion)InventoryUI.selectedItem; // ep du lieu (ItemSO) -> Consumable bcs same child class
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
        AudioManager.instance.PlaySfx("Click");
        discardWindow.gameObject.SetActive(true);
        discardWindow.gameObject.GetComponent<AmtConfirmWindow>().InitAmt(1);
        this.gameObject.SetActive(false);
    }
    public void OnSelectHotKeyItem()
    {
        AudioManager.instance.PlaySfx("Click");
        if (InventoryUI.selectedItem.GetType().Equals(typeof(Potion)))
        {
            hotKeyWindow.gameObject.SetActive(true);
            setkeySpellWindow.gameObject.SetActive(false);
            setKeyItemWindow.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        if (InventoryUI.selectedItem.GetType().Equals(typeof(SpellBook)))
        {
            SpellBook spellbook = (SpellBook)InventoryUI.selectedItem; // type data (ItemSO) -> (SpellBook) bcs same child class
            if (!spellbook.spell.isUnlock)
            {
                hotKeyWindow.gameObject.SetActive(true);
                objectWindow.cannotHotKeyWindow.gameObject.SetActive(true); // not yet learned skill
                return;
            }
            hotKeyWindow.gameObject.SetActive(true);
            setKeyItemWindow.gameObject.SetActive(false);
            setkeySpellWindow.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
    public void OnLearnAbility()
    {
        AudioManager.instance.PlaySfx("Click");
        SpellBook spellbook = (SpellBook)InventoryUI.selectedItem;
        if (!spellbook.spell.isUnlock)
        {
            LearnWindow.gameObject.SetActive(true);
            NotlearnWindow.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (spellbook.spell.isUnlock)
        {
            NotlearnWindow.gameObject.SetActive(false);
            LearnWindow.gameObject.SetActive(true); 
            CompeleteLearnWindow.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
