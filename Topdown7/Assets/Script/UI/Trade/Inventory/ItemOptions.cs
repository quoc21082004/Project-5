using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemOptions : MonoBehaviour
{
    public Button selectSlotbtn;

    public void OnBackButton()
    {
        gameObject.SetActive(false);
        selectSlotbtn.Select();
        selectSlotbtn.OnSelect(null);
    }
}
