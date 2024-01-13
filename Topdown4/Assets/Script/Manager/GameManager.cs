using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public float exp, exptolevel;
    [HideInInspector] public int level;
    private void Update()
    {
        #region Map
        if (!FullMapManager.isMapOpen)
        {
            if (Input.GetKeyDown(KeyCode.M) && !PauseMenu.isGamePause)
            {
                FullMapManager.instance.OpenMap();
            }
        }
        else if (FullMapManager.isMapOpen)
        {
            if (Input.GetKeyDown(KeyCode.M) && PauseMenu.isGamePause)
            {
                FullMapManager.instance.CloseMap();
            }
        }
        #endregion
        #region Shop 
        if (!ShopManager.isShopOpen)
        {
            if (Input.GetKeyDown(KeyCode.U) && !PauseMenu.isGamePause)
            {
                ShopManager.instance.OpenShop();
            }
        }
        else if (ShopManager.isShopOpen)
        {
            if (Input.GetKeyDown(KeyCode.U) && PauseMenu.isGamePause)
            {
                ShopManager.instance.CloseShop();
            }
        }
        #endregion
    }
    private void OnEnable()
    {
        exp = PlayerPrefs.GetFloat("Exp");
        exptolevel = PlayerPrefs.GetFloat("Explevelup");
        level = PlayerPrefs.GetInt("level");
    }
    public void AddExperience(float expToAdd)
    {
        PlayerPrefs.SetFloat("Exp", PlayerPrefs.GetFloat("Exp") + expToAdd + (expToAdd * PlayerPrefs.GetFloat("extraExp")) / 100);
        while (PlayerPrefs.GetFloat("Exp") >= PlayerPrefs.GetFloat("Explevelup"))
            LevelUp();
        exptolevel = PlayerPrefs.GetFloat("Explevelup");
        exp = PlayerPrefs.GetFloat("Exp");
        PlayerPrefs.Save();
    }
    public void LevelUp()
    {
        PlayerPrefs.SetFloat("Exp", PlayerPrefs.GetFloat("Exp") - PlayerPrefs.GetFloat("Explevelup"));
        PartyController.player.playerdata.levelUp.skillPoint += 1;
        PlayerPrefs.SetFloat("Explevelup", PlayerPrefs.GetFloat("Explevelup") * 1.20f);
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        level = PlayerPrefs.GetInt("level");
        PlayerPrefs.Save();
    }
    public void RespawnAfterDie(float lostexp)
    {
        exp -= (int)((exp * lostexp) / 100); 
    }
}
