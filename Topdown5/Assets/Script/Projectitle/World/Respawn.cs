using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Respawn : MonoBehaviour
{
    public string loadScene = "Base 1";
    public Vector3 respawnRecord;
    public TextMeshProUGUI respawn_txt;
    public static int expLostRate = 30;
    public static int GoldLostRate = 30;
    private void OnEnable()
    {
        respawn_txt.text = "Respawn In Town (-" + expLostRate + " %Exp And " + GoldLostRate + " %Gold)"; 
    }
    public void RespawnAfterDie()
    {
        Time.timeScale = 1;
        PartyController.instance.FullRestore();
        PartyController.instance.Respawn(expLostRate, GoldLostRate);
        FadeManager.instance.OnFadeSceneChange(loadScene);
        PartyController.player.transform.position = respawnRecord;
    }
}
