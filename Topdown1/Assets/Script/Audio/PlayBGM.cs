using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public Sound sound;
    private bool isBGM = false;
    private void Start()
    {
        if (AudioManager.instance != null && !isBGM)
        {
            AudioManager.instance.PlayMusicBGM(sound.name);
            isBGM = true;
        }
    }
}
