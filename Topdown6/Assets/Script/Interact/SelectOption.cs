using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
public class SelectOption : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI select_txt;
    [SerializeField] Button select_btn;
    public Action ClickAction;
    private void Awake()
    {
        select_btn.onClick.AddListener(() =>
        {
            CallBackAction();
        });
    }
    private void OnDestroy()
    {
        select_btn.onClick.RemoveListener(() =>
        {
            CallBackAction();
        });
    }
    public void ShowOptionBox(string message, Action action)
    {
        gameObject.SetActive(true);
        select_txt.text = message.ToString();
        ClickAction += action;
    }
    private void CallBackAction()
    {
        ClickAction?.Invoke();
        ClickAction = null;
    }
}

