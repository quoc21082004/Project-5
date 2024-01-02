using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LearnWindow : MonoBehaviour
{
    public TextMeshProUGUI learncost_txt, completelearn_txt;
    public Button confirm_btn, cancel_btn, ok_btn;
    public GameObject completeLearnWindow;
    SpellBook spellbook;
    private void Update()
    {
        spellbook = (SpellBook)InventoryUI.selectedItem; // type data (ItemSO) -> (SpellBook) bcs same child class
        learncost_txt.text = spellbook.spell.learnCost.ToString() + " <sprite=5>";
        bool checkCost = PartyController.inventoryG.Gold > spellbook.spell.learnCost ? true : false;
        if (checkCost)
        {
            learncost_txt.color = Color.white;
            confirm_btn.interactable = true;
        }
        else
        {
            confirm_btn.interactable = false;
            learncost_txt.color = Color.red;
        }
        cancel_btn.onClick.AddListener(() =>
        {
            CancelButton();
        });
        confirm_btn.onClick.AddListener(() =>
        {
            ConfirmButton();
        });
        CheckIfCompleteLearn();
    }
    void ConfirmButton()
    {
        if (!spellbook.spell.isUnlock)
        {
            if (PartyController.inventoryG.Gold >= spellbook.spell.learnCost)
            {
                PartyController.AddGold(-spellbook.spell.learnCost);
                spellbook.spell.isUnlock = true;
                spellbook.Use();
                this.gameObject.SetActive(false);
                return;
            }
            else
                this.gameObject.SetActive(false);
        }
    }
    void CancelButton()
    {
        gameObject.SetActive(false);
    }
    public void CheckIfCompleteLearn()
    {
        if (spellbook.spell.isUnlock)
        {
            spellbook.spell.learnCost = 0;
            if (completeLearnWindow != null)
            {
                completelearn_txt.text = "\n You have already learned this ability !!!";
                ok_btn.onClick.AddListener(() =>
                {
                    completeLearnWindow.gameObject.SetActive(false);
                });
            }
        }
    }
}
