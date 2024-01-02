using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullMapManager : Singleton<FullMapManager>
{
    public GameObject mapUI;
    public static bool isMapOpen = false;

    public void OpenMap()
    {
        isMapOpen = true;
        mapUI.SetActive(true);
        PauseMenu.instance.Pause();
    }
    public void CloseMap()
    {
        mapUI.SetActive(false);
        StartCoroutine(ResumeNextFrame());
    }
    IEnumerator ResumeNextFrame()
    {
        yield return null;
        isMapOpen = false;
        PauseMenu.instance.Resume();
    }
}
