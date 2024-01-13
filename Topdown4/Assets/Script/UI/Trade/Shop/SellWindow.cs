using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SellWindow : AmtConfirmWindow
{
    protected override void OnEnable()
    {
        base.OnEnable();
        plus_btn.onClick.AddListener(() =>
        {
            PlusBtn();
        });

        minus_btn.onClick.AddListener(() =>
        {
            MinusBtn();
        });
    }
    void MinusBtn()
    {
        if (selectAmt > 1)
        {
            AudioManager.instance.PlaySfx("Click");
            selectAmt--;
            amt_txt.text = selectAmt.ToString();
        }
    }
    void PlusBtn()
    {
        if (selectAmt < InventoryUI.selectedItem.currentAmt)   
        {
            AudioManager.instance.PlaySfx("Click");
            selectAmt++;
            amt_txt.text = selectAmt.ToString();
        }
    }
    public override void ConfirmAmt()
    {
        AudioManager.instance.PlaySfx("Click");
        amtPanel.gameObject.SetActive(false);
        confirmPanel.gameObject.SetActive(true);
        confirmAction_txt.text = string.Format("Selling\n"
                                             + "{0} x{1} \n"
                                             + "for \n"
                                             + "{2}" + "  <sprite=3> \n"
                                             + "Confirm ?",
                                             InventoryUI.selectedItem.name, selectAmt, InventoryUI.selectedItem.sellPrice * selectAmt);
    }
    public override void ConfirmAction()
    {
        InventoryUI.selectedItem.SellForGold(selectAmt);
        AudioManager.instance.PlaySfx("Purchase");
        gameObject.SetActive(false);
        confirmPanel.gameObject.SetActive(false);
        amtPanel.gameObject.SetActive(true);
    }
}
