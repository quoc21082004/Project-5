using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeStats : MonoBehaviour
{
    int powerlevel, vitalitylevel, magiclevel, hastelevel, greedlevel;
    Player player;
    PlayerAttack playerAttack;
    int skillPoints;
    [SerializeField] TextMeshProUGUI skillPointsText;

    public UpgradeText upgradeText;
    [Space]
    public StatsInfoText infotext;
    [Space]
    public UpgradeButton upgradebtn;
    [Space]
    public PassivePower passivePower;
    public PassiveVitality passiveVitality;
    public PassiveMagic passiveMagic;
    public PassiveHaste passiveHaste;
    public PassiveGreed passiveGreed;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }
    private void Update()
    {
        skillPoints = PlayerPrefs.GetInt("skillpoint");
        skillPointsText.text = "" + "Skill Points: " + skillPoints.ToString();
        StatsLevel_text();
        StatsInfo_text();
        SetInteractBtn();
        DelayIcon();
        SetFullText();
    }
    #region StatsLevel & StatsInfo
    private void StatsLevel_text()
    {
        upgradeText.powerlevel_text.text = "POWER\n" + "<size=250>" + PlayerPrefs.GetInt("powerlevel").ToString() + "</size>";
        upgradeText.vitalitylevel_text.text = "VITALITY\n" + "<size=250>" + PlayerPrefs.GetInt("vitalitylevel").ToString() + "</size>";
        upgradeText.hastelevel_text.text = "HASTE\n" + "<size=250>" + PlayerPrefs.GetInt("hastelevel").ToString() + "</size>";
        upgradeText.greedlevel_text.text = "GREED\n" + "<size=250>" + PlayerPrefs.GetInt("greedlevel").ToString() + "</size>";
        upgradeText.magiclevel_text.text = "MAGIC\n" + "<size=250>" + PlayerPrefs.GetInt("magiclevel").ToString() + "</size>";
    }
    private void StatsInfo_text()
    {
        infotext.powerinfo_text.text = "" + PlayerPrefs.GetFloat("wandDamage") + " DAMAGE\n"
            + "" + PlayerPrefs.GetFloat("critchance") + "% CRIT";

        infotext.vitalityinfo_text.text = "" + PlayerPrefs.GetFloat("heath") + " HEALTH\n"
            + "" + PlayerPrefs.GetFloat("healthregen") + " HP REGEN\n"
            + "" + PlayerPrefs.GetFloat("damageReduction").ToString("F2") + "% DAMAGE REDUCE";               // save in data although reset

        infotext.magicinfo_text.text = "" + PlayerPrefs.GetFloat("mana") + " MANA\n"                             // save in data but not save when reset
            + "" + PlayerPrefs.GetFloat("manaregen") + " MP REGEN\n"
            + "" + PlayerPrefs.GetFloat("cdr").ToString("F2") + "% CD REDUCE";

        infotext.greedinfo_Text.text = "" + PlayerPrefs.GetFloat("extraExp").ToString("F1") + "% XPBONUS\n"
            + "" + PlayerPrefs.GetFloat("percentageDamage").ToString("F1") + "% DamagePlus";

        infotext.hasteinfo_text.text = "" + PlayerPrefs.GetFloat("bounsSpeed").ToString("F1") + "% SP\n"
            + "" + PlayerPrefs.GetFloat("attackSpeed").ToString("F1") + "% SPATK\n"
            + "" + player.rangePickup.ToString("F1") + "m RANGE PICK";
    }
    #endregion

    #region Update_Stats Button
    public void UpdatePower_btn()
    {
        AudioManager.instance.PlaySfx("Click");
        if (PlayerPrefs.GetInt("skillpoint") > 0)
        {
            PlayerPrefs.SetInt("powerlevel", PlayerPrefs.GetInt("powerlevel") + 1);
            PlayerPrefs.SetInt("skillpoint", PlayerPrefs.GetInt("skillpoint") - 1);
            powerlevel = PlayerPrefs.GetInt("powerlevel");
            player.playerdata.upgrade.powerlevel = powerlevel;
            PlayerPrefs.SetFloat("wandDamage", PlayerPrefs.GetFloat("wandDamage") + 5);
            PlayerPrefs.SetFloat("critchance", PlayerPrefs.GetFloat("critchance") + 1.5f);

            if (powerlevel == 4)
            {
                player.playerdata.basicAttack.minCritDamage += 2.5f;
                player.playerdata.basicAttack.maxCritDamage += 3f;
            }
            if (powerlevel == 9)
                playerAttack.isDoubleShot = true;
            if (powerlevel == 14)
                PlayerPrefs.SetString("BoomAOE", "true");
            PlayerPrefs.Save();
        }
    }
    public void UpdateVitality_btn()
    {
        AudioManager.instance.PlaySfx("Click");
        if (PlayerPrefs.GetInt("skillpoint") > 0) 
        {
            PlayerPrefs.SetInt("vitalitylevel", PlayerPrefs.GetInt("vitalitylevel") + 1);
            PlayerPrefs.SetInt("skillpoint", PlayerPrefs.GetInt("skillpoint") - 1);
            vitalitylevel = PlayerPrefs.GetInt("vitalitylevel");
            player.playerdata.upgrade.vitalitylevel = vitalitylevel;
            PlayerPrefs.SetFloat("heath", PlayerPrefs.GetFloat("heath") + 15);
            PlayerPrefs.SetFloat("healthregen", PlayerPrefs.GetFloat("healthregen") + 0.5f);
            PlayerPrefs.SetFloat("damageReduction", PlayerPrefs.GetFloat("damageReduction") + 0.15f);

            if (vitalitylevel == 5)
                PlayerPrefs.SetFloat("heath", PlayerPrefs.GetFloat("heath") + 200);
            if (vitalitylevel == 10)
                PlayerPrefs.SetString("immueSlow", "true");
        }
    }
    public void UpdateMagic_btn()
    {
        AudioManager.instance.PlaySfx("Click");
        if (PlayerPrefs.GetInt("skillpoint") > 0)
        {
            PlayerPrefs.SetInt("skillspent", PlayerPrefs.GetInt("skillspent") + 1);
            PlayerPrefs.SetInt("magiclevel", PlayerPrefs.GetInt("magiclevel") + 1);
            magiclevel = PlayerPrefs.GetInt("magiclevel");
            player.playerdata.upgrade.magiclevel = magiclevel;
            PlayerPrefs.SetFloat("mana", PlayerPrefs.GetFloat("mana") + 10);
            PlayerPrefs.SetFloat("manaregen", PlayerPrefs.GetFloat("manaregen") + 0.5f);
            PlayerPrefs.SetFloat("cdr", PlayerPrefs.GetFloat("cdr") + 1.25f);
        }
    }
    public void UpdateHaste_btn()
    {
        AudioManager.instance.PlaySfx("Click");
        if (PlayerPrefs.GetInt("skillpoint") > 0)
        {
            PlayerPrefs.SetInt("hastelevel", PlayerPrefs.GetInt("hastelevel") + 1);
            PlayerPrefs.SetInt("skillpoint", PlayerPrefs.GetInt("skillpoint") - 1);
            PlayerPrefs.SetFloat("bounsSpeed", PlayerPrefs.GetFloat("bounsSpeed") + 2);
            PlayerPrefs.SetFloat("attackSpeed", PlayerPrefs.GetFloat("attackSpeed") - 0.01f);
            hastelevel = PlayerPrefs.GetInt("hastelevel");
            player.playerdata.upgrade.hastelevel = hastelevel;
            player.rangePickup += 0.15f;
            if (hastelevel == 4) 
                PlayerPrefs.SetInt("skillpoint", PlayerPrefs.GetInt("skillpoint") + 5);
        }
    }
    public void UpdateGreed_btn()
    {
        AudioManager.instance.PlaySfx("Click");
        if (PlayerPrefs.GetInt("skillpoint") > 0)
        {
            PlayerPrefs.SetInt("greedlevel", PlayerPrefs.GetInt("greedlevel") + 1);
            PlayerPrefs.SetInt("skillpoint", PlayerPrefs.GetInt("skillpoint") - 1);
            greedlevel = PlayerPrefs.GetInt("greedlevel");
            player.playerdata.upgrade.greedlevel = greedlevel;
            PlayerPrefs.SetFloat("extraExp", PlayerPrefs.GetFloat("extraExp") + 1.25f);
            PlayerPrefs.SetFloat("percentageDamage", PlayerPrefs.GetFloat("percentageDamage") + 1f);

            if (greedlevel == 5)
                PlayerPrefs.SetInt("doubleDrop", PlayerPrefs.GetInt("doubleDrop") + 1);
            if (greedlevel == 10)
                PlayerPrefs.SetInt("extraConis", 2);
            if (greedlevel == 15)
                PlayerPrefs.SetFloat("percentageDamage", PlayerPrefs.GetFloat("percentageDamage") + 2.5f);
        }
    }
    void SetFullText()
    {
        if (PlayerPrefs.GetInt("powerlevel") == 15)
        {
            upgradebtn.power_btn.interactable = false;
            upgradebtn.power_btn.GetComponentInChildren<TextMeshProUGUI>().text = "<size=50>MAX</size>";
        }
        if (PlayerPrefs.GetInt("vitalitylevel") == 15)
        {
            upgradebtn.vitality_btn.interactable = false;
            upgradebtn.vitality_btn.GetComponentInChildren<TextMeshProUGUI>().text = "<size=50>MAX</size>";
        }
        if (PlayerPrefs.GetInt("magiclevel") == 15)
        {
            upgradebtn.magic_btn.interactable = false;
            upgradebtn.magic_btn.GetComponentInChildren<TextMeshProUGUI>().text = "<size=50>MAX</size>";
        }
        if (PlayerPrefs.GetInt("hastelevel") == 15)
        {
            upgradebtn.haste_btn.interactable = false;
            upgradebtn.haste_btn.GetComponentInChildren<TextMeshProUGUI>().text = "<size=50>MAX</size>";
        }
        if (PlayerPrefs.GetInt("greedlevel") == 15)
        {
            upgradebtn.greed_btn.interactable = false;
            upgradebtn.greed_btn.GetComponentInChildren<TextMeshProUGUI>().text = "<size=50>MAX</size>";
        }
    }
    #endregion

    #region Interact & PassiveIcon
    void SetInteractBtn()
    {
        if (PlayerPrefs.GetInt("skillpoint") == 0)
        {
            upgradebtn.power_btn.interactable = false;
            upgradebtn.vitality_btn.interactable = false;
            upgradebtn.magic_btn.interactable = false;
            upgradebtn.haste_btn.interactable = false;
            upgradebtn.greed_btn.interactable = false;
        }
        else if (PlayerPrefs.GetInt("skillpoint") > 0)
        {
            upgradebtn.power_btn.interactable = true;
            upgradebtn.vitality_btn.interactable = true;
            upgradebtn.magic_btn.interactable = true;
            upgradebtn.haste_btn.interactable = true;
            upgradebtn.greed_btn.interactable = true;
        }
    }
    void DelayIcon()
    {
        if (powerlevel >= 5)
            passivePower.PowerLv5.color = Color.white;
        else
            passivePower.PowerLv5.color = Color.grey;
        if (powerlevel >= 10)
            passivePower.PowerLv10.color = Color.white;
        else
            passivePower.PowerLv10.color = Color.grey;
        if (powerlevel >= 15)
            passivePower.PowerLv15.color = Color.white;
        else 
            passivePower.PowerLv15.color = Color.grey;
        if (vitalitylevel >= 5)
            passiveVitality.VitalityLv5.color = Color.white;
        else
            passiveVitality.VitalityLv5.color = Color.grey;
        if (vitalitylevel >= 10)
            passiveVitality.VitalityLv10.color = Color.white;
        else
            passiveVitality.VitalityLv10.color = Color.grey;
        if (vitalitylevel >= 15)
            passiveVitality.VitalityLv15.color = Color.white;
        else
            passiveVitality.VitalityLv15.color = Color.grey;
        if (magiclevel >= 5)
            passiveMagic.MagicLv5.color = Color.white;
        else
            passiveMagic.MagicLv5.color = Color.grey;
        if (magiclevel >= 10)
            passiveMagic.MagicLv10.color = Color.white;
        else
            passiveMagic.MagicLv10.color = Color.grey;
        if (magiclevel >= 15)
            passiveMagic.MagicLv15.color = Color.white;
        else
            passiveMagic.MagicLv15.color = Color.grey;
        if (hastelevel >= 5)
            passiveHaste.HasteLv5.color = Color.white;
        else
            passiveHaste.HasteLv5.color = Color.grey;
        if (hastelevel >= 10)
            passiveHaste.HasteLv10.color = Color.white;
        else
            passiveHaste.HasteLv10.color = Color.grey;
        if (hastelevel >= 15)
            passiveHaste.HasteLv15.color = Color.white;
        else
            passiveHaste.HasteLv15.color = Color.grey;
        if (greedlevel >= 5)
            passiveGreed.GreedLv5.color = Color.white;
        else
            passiveGreed.GreedLv5.color = Color.grey;
        if (greedlevel >= 10)
            passiveGreed.GreedLv10.color = Color.white;
        else
            passiveGreed.GreedLv10.color = Color.grey;
        if (greedlevel >= 15)
            passiveGreed.GreedLv15.color = Color.white;
        else
            passiveGreed.GreedLv15.color = Color.grey;
    }
    #endregion
}
#region                                 Upgrade System
[System.Serializable]
public class UpgradeText
{
    [SerializeField] public TextMeshProUGUI powerlevel_text;
    [SerializeField] public TextMeshProUGUI vitalitylevel_text;
    [SerializeField] public TextMeshProUGUI magiclevel_text;
    [SerializeField] public TextMeshProUGUI hastelevel_text;
    [SerializeField] public TextMeshProUGUI greedlevel_text;
}
[System.Serializable]
public class StatsInfoText
{
    [SerializeField] public TextMeshProUGUI powerinfo_text;
    [SerializeField] public TextMeshProUGUI vitalityinfo_text;
    [SerializeField] public TextMeshProUGUI magicinfo_text;
    [SerializeField] public TextMeshProUGUI hasteinfo_text;
    [SerializeField] public TextMeshProUGUI greedinfo_Text;
}
[System.Serializable]
public class UpgradeButton
{
    [SerializeField] public Button power_btn;
    [SerializeField] public Button vitality_btn;
    [SerializeField] public Button magic_btn;
    [SerializeField] public Button haste_btn;
    [SerializeField] public Button greed_btn;
}
#endregion