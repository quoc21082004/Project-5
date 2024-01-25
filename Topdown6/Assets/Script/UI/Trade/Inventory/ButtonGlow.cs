using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonGlow : MonoBehaviour , ISelectHandler , IDeselectHandler
{
    public GameObject buttonGlowImg;
    public void OnDisable()
    {
        buttonGlowImg.gameObject.SetActive(false);   
    }
    public void OnSelect(BaseEventData eventData)
    {
        buttonGlowImg.gameObject.SetActive(true);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        buttonGlowImg.gameObject.SetActive(false);
    }
}
