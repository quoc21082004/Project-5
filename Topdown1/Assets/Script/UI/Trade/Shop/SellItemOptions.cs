using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItemOptions : ItemOptions
{
    public Button sell_btn;
    public GameObject sellWindow;


    private void OnEnable()
    {
        if (InventoryUI.selectedItem == null)
        {
            sell_btn.interactable = false;
        }
        else if (InventoryUI.selectedItem != null)
        {
            sell_btn.interactable = !(InventoryUI.selectedItem.GetType() == (typeof(Consumable)));
            sell_btn.interactable = (InventoryUI.selectedItem.GetType() == (typeof(SpellBook)));
        }
        sell_btn.Select();
        sell_btn.OnSelect(null);
    }
    public void OnSellButton()
    {
        sellWindow.SetActive(true);
        this.gameObject.SetActive(false);
        sellWindow.gameObject.GetComponent<AmtConfirmWindow>().InitAmt(1);
    }
}
