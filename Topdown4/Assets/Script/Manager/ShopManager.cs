using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] GameObject ShopMenuUI;
    [SerializeField] GameObject BuyUI;
    [SerializeField] GameObject SellUI;

    [SerializeField] Button buybtn, sellbtn;
    public static bool isShopOpen = false;

    public void OpenShop()
    {
        isShopOpen = true;
        ShopMenuUI.SetActive(true);
        PauseMenu.instance.Pause();
    }
    public void CloseShop()
    {
        isShopOpen = false;
        ShopMenuUI.SetActive(false);
        PauseMenu.instance.Resume();
    }
    public void ShowBuyUI()
    {
        BuyUI.SetActive(true);
    }
    public void HideBuyUI()
    {
        BuyUI.SetActive(false);
        SelectBtn(buybtn);
    }
    public void ShowSellUI()
    {
        SellUI.SetActive(true);
    }
    public void HideSellUI()
    {
        SellUI.SetActive(false);
        SelectBtn(sellbtn);
    }
    public void SelectBtn(Button btn)
    {
        if (btn != null)
        {
            btn.Select();
            btn.OnSelect(null);
        }
    }
}
