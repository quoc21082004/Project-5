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
            if (Input.GetKeyDown(KeyCode.I))
                OpenInventory();
        else
            if (Input.GetKeyDown(KeyCode.I))
                CloseInventory();
    }
    public void OpenInventory()
    {
        AudioManager.instance.PlaySfx("Click");
        inventoryScreen.gameObject.SetActive(true);
        isInventoryScreen = true;
    }
    public void CloseInventory()
    {
        AudioManager.instance.PlaySfx("Click");
        inventoryScreen.gameObject.SetActive(false);
        isInventoryScreen = false;
    }
    public void Pause()
    {
        AudioManager.instance.PlaySfx("Click");
        Time.timeScale = 0;
        isGamePause = true;
    }
    public void Resume()
    {
        StartCoroutine(ResumeCourtine());
    }
    IEnumerator ResumeCourtine()
    {
        AudioManager.instance.PlaySfx("Click");
        yield return null;
        Time.timeScale = 1f;
        isGamePause = false;
    }
}
