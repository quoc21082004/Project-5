using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public abstract class AmtConfirmWindow : MonoBehaviour
{
    public int selectAmt;
    public GameObject amtPanel;
    public GameObject confirmPanel;
    public Button plus_btn, minus_btn;
    public TextMeshProUGUI amt_txt;
    public Button amtConfirm_btn;
    public Button amtCancel_btn;
    public TextMeshProUGUI confirmAction_txt;

    protected virtual void OnEnable()
    {
        amtPanel.gameObject.SetActive(true);
        confirmPanel.gameObject.SetActive(false);
    }
    public abstract void ConfirmAmt();
    public abstract void ConfirmAction();   
    public void CancelAmt()
    {
        AudioManager.instance.PlaySfx("Click");
        gameObject.SetActive(false);
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectSlotbtn.Select();
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectSlotbtn.OnSelect(null);
    }
    public void CancelComfirm()
    {
        AudioManager.instance.PlaySfx("Click");
        confirmPanel.gameObject.SetActive(false);
        amtPanel.gameObject.SetActive(true);
    }
    public void InitAmt(int amt)
    {
        selectAmt = amt;
        amt_txt.text = "" + selectAmt;
    }
}
