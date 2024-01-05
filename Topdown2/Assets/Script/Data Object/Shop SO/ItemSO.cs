using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Mobs Item")]
public class ItemSO : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public int itemNumber;
    public int currentAmt = 1;
    public bool isStackable;
    [TextArea(0, 2)] public string itemDescription;
    [TextArea(0, 2)] public string flavor;
    public int buyPrice;
    public int sellPrice;
    public virtual void Use()
    {
        Debug.Log("Using + " + name);
    }
    public int GetAmtInInventory()
    {
        return PartyController.inventoryG.GetItemAmt(this);
    }
    public void RemoveFromInventory(int amt)
    {
        PartyController.inventoryG.Remove(this, amt);
    }
    public void BoughtForGold(int selectAmt)
    {
        PartyController.inventoryG.Gold -= selectAmt * buyPrice;
        ItemSO clone = Instantiate(this);
        clone.currentAmt = selectAmt;
        PartyController.inventoryG.AddItem(clone);
    }
    public void SellForGold(int selectamt)
    {
        PartyController.inventoryG.Gold += selectamt * sellPrice;
        PartyController.inventoryG.Remove(this, selectamt);
    }
}
