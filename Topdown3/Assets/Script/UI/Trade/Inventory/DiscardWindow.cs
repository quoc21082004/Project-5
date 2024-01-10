using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DiscardWindow : AmtConfirmWindow
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    private void Start()
    {
        plus_btn.onClick.AddListener(() =>
        {
            PlusButton();
        });

        minus_btn.onClick.AddListener(() =>
        {
            MinusButton();
        });
    }
    private void PlusButton()
    {
        if (selectAmt < InventoryUI.selectedItem.currentAmt) 
        {
            AudioManager.instance.PlaySfx("Click");
            selectAmt++;
            amt_txt.text = selectAmt.ToString();
        }
    }
    private void MinusButton()
    {
        if (selectAmt > 1)
        {
            AudioManager.instance.PlaySfx("Click");
            selectAmt--;
            amt_txt.text = "" + selectAmt;
        }
    }
    public override void ConfirmAmt()
    {
        AudioManager.instance.PlaySfx("Click");
        amtPanel.gameObject.SetActive(false);
        confirmPanel.gameObject.SetActive(true);
        confirmAction_txt.text = string.Format("Discarding\n"
                                + "{0} x{1} \n"
                                + "Confirm ?", InventoryUI.selectedItem.name, selectAmt);
    }
    public override void ConfirmAction()
    {
        InventoryUI.selectedItem.RemoveFromInventory(selectAmt);
        AudioManager.instance.PlaySfx("Purchase");
        this.gameObject.SetActive(false);
    }
}
