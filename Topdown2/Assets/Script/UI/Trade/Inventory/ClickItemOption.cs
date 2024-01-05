using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickItemOption : MonoBehaviour, IPointerClickHandler
{
    public ItemOptions itemOptionWindow;
    private void Start()
    {
        itemOptionWindow = GetComponentInParent<InventoryUI>().itemOptionsWindow;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable = true && eventData.button == PointerEventData.InputButton.Right)
        {
            GetComponentInParent<InventorySlot>().SelectItem();
            itemOptionWindow.gameObject.SetActive(true);
            itemOptionWindow.transform.position = Input.mousePosition;
            itemOptionWindow.selectSlotbtn = GetComponentInChildren<Button>();
        }
    }
}
