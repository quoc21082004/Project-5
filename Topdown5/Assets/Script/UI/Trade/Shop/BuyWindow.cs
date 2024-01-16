using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuyWindow : AmtConfirmWindow
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    private void Start()
    {
        plus_btn.onClick.AddListener(() =>
        {
            Plusbtn();
        });
        minus_btn.onClick.AddListener(() =>
        {
            Minusbtn();
        });
    }
    void Plusbtn()
    {
        if ((selectAmt + 1) * InventoryUI.selectedItem.buyPrice <= PartyController.inventoryG.Gold)  
        {
            AudioManager.instance.PlaySfx("Click");
            selectAmt++;
            amt_txt.text = "" + selectAmt;
        }
    }
    void Minusbtn()
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
        confirmAction_txt.text = string.Format("Buying \n"
                            + "{0} x{1} \n"
                            + "for\n"
                            + "{2}" + "  <sprite=3>" + "\n"
                            + "Confirm ?",
                            InventoryUI.selectedItem.name, selectAmt, InventoryUI.selectedItem.buyPrice * selectAmt);
    }
    public override void ConfirmAction()
    {
        InventoryUI.selectedItem.BoughtForGold(selectAmt);
        AudioManager.instance.PlaySfx("Purchase");
        gameObject.SetActive(false);
    }
}
