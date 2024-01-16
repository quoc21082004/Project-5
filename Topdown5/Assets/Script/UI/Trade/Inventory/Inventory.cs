using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryG 
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    public int space = 24;
    public int Gold;
    public List<ItemSO> items = new List<ItemSO>();
    public bool AddItem(ItemSO item)
    {
        if (items.Count >= space)
            return false;
        if (item.isStackable && items.Find(x => x.name.Equals(item.name)) != null) 
        {
            ItemSO itemInInventory = items.Find(x => x.name.Equals(item.name));
            itemInInventory.currentAmt += item.currentAmt;
        }
        else
        {
            items.Add(MonoBehaviour.Instantiate(item));       // add item into list
            items.Sort((x1, x2) => x1.itemNumber.CompareTo(x2.itemNumber)); // sort item number 
        }
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
        return true;
    }

    public void Remove(ItemSO itemSO, int amt)
    {
        /*for (int i = 0; i < items.Count; i++)                         // way 2
        {
            if (itemSO == items[i])
            {
                itemInInventory = items[i];
            }
        }*/
        ItemSO itemInInventory = items.Find(x => x.Equals(itemSO)); // way 1
        itemInInventory.currentAmt -= amt;
        if (itemInInventory.currentAmt <= 0)
        {
            items.Remove(itemInInventory);
            MonoBehaviour.Destroy(itemInInventory);
            items.Sort((x1, x2) => x1.itemNumber.CompareTo(x2.itemNumber));
        }
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
    public void Remove(ItemSO itemSO, bool toDestroy)
    {
        ItemSO itemInventory = items.Find(x => x.Equals(itemSO));
        items.Remove(itemInventory);
        items.Sort((x1, x2) => x1.itemNumber.CompareTo(x2.itemNumber));
        if (toDestroy)
        {
            MonoBehaviour.Destroy(itemInventory);
        }
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
    public int GetItemAmt(ItemSO item)
    {
        if (items.Find(x => x == item) == null)
            return 0;
        else if (items.Find(x => x == item) != null)
            return items.Find(x => x == item).currentAmt;
        return 0;
    }
    public ItemSO GetItem(int itemnumber)
    {
        return items.Find(x => x.itemNumber == itemnumber);
    }
}
