using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoots : LootItem
{
    [SerializeField] public ItemSO item;
    [SerializeField] public int quantity;
    bool wasPickUp;
    protected override void PickUp()
    {
        base.PickUp();
        if (quantity > 0)
        {
            ItemSO clone = Instantiate(item);
            clone.currentAmt = quantity;
            wasPickUp = PartyController.inventoryG.AddItem(clone);
        }
        else if (quantity == 0)  
            wasPickUp = PartyController.inventoryG.AddItem(item);

    }
}
