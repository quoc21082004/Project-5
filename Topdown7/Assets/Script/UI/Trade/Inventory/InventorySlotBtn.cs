using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotBtn :MonoBehaviour ,ISelectHandler
{
    public ItemOptions itemOptionWindow;
    private void Start()
    {
        itemOptionWindow = GetComponentInParent<InventoryUI>().itemOptionsWindow;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (InventoryUI.selectedItem != null)
            {
                itemOptionWindow.gameObject.SetActive(true);
                itemOptionWindow.transform.position = transform.position + new Vector3(30f, 0);
                itemOptionWindow.selectSlotbtn = GetComponent<Button>();
            }
        });
    }
    public void OnSelect(BaseEventData eventData)
    {
        GetComponentInParent<InventorySlot>().SelectItem();
    }
}
