using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LearnWindow : MonoBehaviour
{
    public TextMeshProUGUI learncost_txt;
    public Button confirm_btn, cancel_btn;
    SpellBook spellbook;
    private void OnEnable()
    {
        spellbook = (SpellBook)InventoryUI.selectedItem; // ep du lieu (ItemSO) -> (SpellBook)
        learncost_txt.text = spellbook.spell.learnCost.ToString() + " <sprite=5>";
        bool checkCost = PartyController.inventoryG.Gold > spellbook.spell.learnCost ? true : false;
        if (checkCost)
            learncost_txt.color = Color.white;
        else
            learncost_txt.color = Color.red;
        cancel_btn.onClick.AddListener(() =>
        {
            CancelButton();
        });
        confirm_btn.onClick.AddListener(() =>
        {
            ConfirmButton();
        });
    }
    void ConfirmButton()
    {
        if (PartyController.inventoryG.Gold > spellbook.spell.learnCost)
        {
            PartyController.AddGold(-spellbook.spell.learnCost);
            spellbook.spell.isUnlock = true;
            spellbook.Use();
            spellbook.RemoveFromInventory(1);
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Don't have enough money");
            this.gameObject.SetActive(false);
        }
    }
    void CancelButton()
    {
        this.gameObject.SetActive(false);
    }
}
