using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public TextMeshProUGUI gold_text;
    public Button back_btn;

    public ItemOptions itemOptionsWindow;
    public SelectedItemDisplay selectItemDisplay;
    public AmtConfirmWindow amtConfirmWindow;

    public static ItemSO selectedItem;
    //public static SpellBook
    protected InventoryG inventory;
    protected InventorySlot[] slots;

    private void Start()
    {
        inventory = PartyController.inventoryG;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventory.onItemChangedCallBack += UpdateUI;
    }
    void OnEnable()
    {
        back_btn.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
        if (inventory == null || inventory != PartyController.inventoryG)
        {
            inventory = PartyController.inventoryG;
            inventory.onItemChangedCallBack += UpdateUI;
        }
        if (slots == null)
        {
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        }

        if (itemOptionsWindow != null)
            itemOptionsWindow.gameObject.SetActive(false);
        if (amtConfirmWindow != null)
            amtConfirmWindow.gameObject.SetActive(false);

        UpdateUI();
    }

    protected virtual void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]); 
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        if (selectedItem != null && selectedItem.currentAmt <= 0)
        {
            selectedItem = null;
        }
        selectItemDisplay.UpdateUI();
        gold_text.text = "" + inventory.Gold;
    }
    public void SelectItem(InventorySlot slot) // make option show
    {
        if (itemOptionsWindow != null)
        {
            itemOptionsWindow.gameObject.SetActive(false);
        }
        selectedItem = slot.item;
        selectItemDisplay.UpdateUI();
    }
    public void AddGoldFree()
    {
        int random = Random.Range(500, 800);
        PartyController.AddGold(random);
        gold_text.text = "" + inventory.Gold;
    }
}
