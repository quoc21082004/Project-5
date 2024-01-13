using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class statusStats : MonoBehaviour
{
    [Header("STATUS")]
    public Player player;
    float currenthp, maxhp, percentage_hp;
    [SerializeField] private Image fill_hp;
    [SerializeField] private TextMeshProUGUI hp_text;
    float currentmp, maxmp, percentage_mp;
    [SerializeField] private Image fill_mp;
    [SerializeField] private TextMeshProUGUI mp_text;
    float currentexp, maxexp, percentage_exp;
    [SerializeField] private Image fill_exp;
    [SerializeField] private TextMeshProUGUI exp_text;

    private void Update()
    {
        if (PartyController.player == null)
            return;
        else
            player = PartyController.player.GetComponent<Player>();

        ReadStats();
        fillImage();
    }
    private void ReadStats()
    {
        maxhp = player.maxhealth;
        maxmp = player.maxmana;
        maxexp = GameManager.instance.exptolevel;
        currenthp = PartyController.player.health;//PlayerPrefs.GetFloat("currenthealth");
        currentmp = PartyController.player.mana;//PlayerPrefs.GetFloat("currentmana");
        currentexp = GameManager.instance.exp;
    }
    private void fillImage()
    {
        percentage_hp = currenthp / maxhp;
        fill_hp.fillAmount = percentage_hp;
        hp_text.text = "" + currenthp.ToString("F0") + " / " + maxhp.ToString("F0");
        percentage_mp = currentmp / maxmp;
        fill_mp.fillAmount = percentage_mp;
        mp_text.text = "" + currentmp.ToString("F0") + " / " + maxmp.ToString("F0");
        percentage_exp = currentexp / maxexp;
        fill_exp.fillAmount = percentage_exp;
        exp_text.text = "" + currentexp.ToString("F0") + " / " + maxexp.ToString("F0");
    }
}
