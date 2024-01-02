using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Singleton<PauseMenu>
{
    public static bool isGamePause = false;
    public GameObject inventoryScreen;
    bool isInventoryScreen = false;
    private void Update()
    {
        if (!isInventoryScreen)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                OpenInventory();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                CloseInventory();
            }
        }
    }
    public void OpenInventory()
    {
        inventoryScreen.gameObject.SetActive(true);
        isInventoryScreen = true;
    }
    public void CloseInventory()
    {
        inventoryScreen.gameObject.SetActive(false);
        isInventoryScreen = false;
    }
    public void Pause()
    {
        Time.timeScale = 0;
        isGamePause = true;
    }
    public void Resume()
    {
        StartCoroutine(ResumeCourtine());
    }
    IEnumerator ResumeCourtine()
    {
        yield return null;
        Time.timeScale = 1f;
        isGamePause = false;
    }
}
