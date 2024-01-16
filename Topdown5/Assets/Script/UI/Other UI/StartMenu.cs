using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    public string LevelToLoad;
    public Button startMenuBtn, loadBtn, howToPlayBtn, quitBtn, backBtn;
    public GameObject howToPlay;
    public void PlayGame()
    {
        FadeManager.instance.OnFadeSceneChange(LevelToLoad);
        startMenuBtn.interactable = false;
        StartCoroutine(startMenuDelay());
    }
    public void LoadGame()
    {

    }
    public void HowToPlay()
    {
        howToPlayBtn.onClick.AddListener(() =>
        {
            howToPlay.gameObject.SetActive(true);
        });
        howToPlayBtn.Select();
        howToPlayBtn.OnSelect(null);
    }
    IEnumerator startMenuDelay()
    {
        yield return new WaitForSeconds(3f);
        startMenuBtn.interactable = true;
    }
    public void HowToPlay_Back()
    {
        backBtn.onClick.AddListener(() =>
        {
            howToPlay.gameObject.SetActive(false);
        });
    }
    public void Quit()
    {
        Debug.Log("Quit Game !!");
        Application.Quit();
    }

}
