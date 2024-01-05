using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventoryUI : InventoryUI
{
    public List<ItemSO> shopItemList;
    protected override void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < shopItemList.Count)
            {
                shopItemList.Sort((x1, x2) => x1.buyPrice.CompareTo(x2.buyPrice)); // sort price 
                slots[i].AddItem(shopItemList[i]);
            }
            else
                slots[i].ClearSlot();
        }
        if (selectedItem != null && selectedItem.currentAmt < 0)
            selectedItem = null;
        selectItemDisplay.UpdateUI();
        gold_text.text = "" + inventory.Gold;
    }
}
