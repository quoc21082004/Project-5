using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Button Item_btn;
    public Image icon;
    public TextMeshProUGUI stackItem_text;
    public ItemSO item;

    public void AddItem(ItemSO newItemSO)
    {
        item = newItemSO;
        icon.sprite = item.icon;
        icon.enabled = true;
        if (item.currentAmt > 1) // stack
        {
            stackItem_text.text = "" + item.currentAmt;
            return;
        }
        if (item.currentAmt < 1)
        {
            stackItem_text.text = "";
            return;
        }
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        stackItem_text.text = "";
    }
    public void SelectItem()
    {
        GetComponentInParent<InventoryUI>().SelectItem(this);
    }
}
