using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeStats : MonoBehaviour
{
    int powerlevel, vitalitylevel, magiclevel, hastelevel, greedlevel;
    [SerializeField] TextMeshProUGUI skillPointsText;

    public UpgradeText upgradeText;
    [Space]
    public StatsInfoText infotext;
    [Space]
    public UpgradeButton upgradebtn;
    private void Update()
    {
        skillPointsText.text = "" + "Skill Points: " + (PartyController.player.playerdata.levelUp.skillPoint).ToString();
        StatsLevel_text();
        StatsInfo_text();
        SetInteractBtn();
        SetFullText();
    }
    #region StatsLevel & StatsInfo
    private void StatsLevel_text()
    {
        upgradeText.powerlevel_text.text = "POWER\n" + "<size=250>" + (PartyController.player.playerdata.upgrade.powerlevel).ToString() + "</size>";
        upgradeText.vitalitylevel_text.text = "VITALITY\n" + "<size=250>" + (PartyController.player.playerdata.upgrade.vitalitylevel).ToString() + "</size>";
        upgradeText.hastelevel_text.text = "HASTE\n" + "<size=250>" + (PartyController.player.playerdata.upgrade.hastelevel).ToString() + "</size>";
        upgradeText.greedlevel_text.text = "GREED\n" + "<size=250>" + (PartyController.player.playerdata.upgrade.greedlevel).ToString() + "</size>";
        upgradeText.magiclevel_text.text = "MAGIC\n" + "<size=250>" + (PartyController.player.playerdata.upgrade.magiclevel).ToString() + "</size>";
    }
    private void StatsInfo_text()
    {
        infotext.powerinfo_text.text = "" + (PartyController.player.playerdata.basicAttack.wandDamage).ToString("F1") + " DAMAGE\n"
            + "" + (PartyController.player.playerdata.basicAttack.critChance).ToString("F1") + "% CRIT";

        infotext.vitalityinfo_text.text = "" + (PartyController.player.playerdata.basicStats.health).ToString() + " HEALTH\n"
            + "" + (PartyController.player.playerdata.basicStats.healthRegen).ToString("F1") + " HP REGEN\n"
            + "" + (PartyController.player.playerdata.otherStats.damageReduction).ToString("F2") + "% DAMAGE REDUCE";               // save in data although reset

        infotext.magicinfo_text.text = "" + (PartyController.player.playerdata.basicStats.mana).ToString() + " MANA\n"                             // save in data but not save when reset
            + "" + (PartyController.player.playerdata.basicStats.manaRegen).ToString() + " MP REGEN\n";

        infotext.greedinfo_Text.text = "" + (PartyController.player.playerdata.extraBuff.extraExp).ToString("F1") + "% XPBONUS\n"
            + "" + (PartyController.player.playerdata.extraBuff.percentDamage).ToString("F1") + "% DamagePlus";

        infotext.hasteinfo_text.text = "" + (PartyController.player.playerdata.extraBuff.extraSpeedMove).ToString("F1") + "% SP\n"
            + "" + PartyController.player.rangePickup.ToString("F1") + "m RANGE PICK";
    }
    #endregion

    #region Update_Stats Button
    public void UpdatePower_btn()
    {
        AudioManager.instance.PlaySfx("Click");
        if (PartyController.player.playerdata.levelUp.skillPoint > 0)
        {
            PartyController.player.playerdata.upgrade.powerlevel += 1;
            PartyController.player.playerdata.levelUp.skillPoint -= 1;
            float upgradePerLevel = PartyController.player.playerdata.upgrade.powerlevel * 1.5f;
            powerlevel = PartyController.player.playerdata.upgrade.powerlevel;
            PartyController.player.playerdata.basicAttack.wandDamage += (5 + upgradePerLevel) * 1.2f;
            PartyController.player.playerdata.basicAttack.critChance += (1f + upgradePerLevel) / 3;
            PartyController.player.playerdata.basicAttack.minCritDamage += 0.1f;
            PartyController.player.playerdata.basicAttack.maxCritDamage += 0.15f;
            if (powerlevel == 4)
            {
                PartyController.player.playerdata.basicAttack.minCritDamage += 1f;
                PartyController.player.playerdata.basicAttack.maxCritDamage += 2f;
            }
            PlayerPrefs.Save();
        }
    }
    public void UpdateVitality_btn()
    {
        AudioManager.instance.PlaySfx("Click");
        if (PartyController.player.playerdata.levelUp.skillPoint > 0) 
        {
            PartyController.player.playerdata.upgrade.vitalitylevel += 1;
            PartyController.player.playerdata.levelUp.skillPoint -= 1;
            float upgradePerLevel = PartyController.player.playerdata.upgrade.vitalitylevel * 1.5f;
            vitalitylevel = PartyController.player.playerdata.upgrade.vitalitylevel;
            PartyController.player.playerdata.basicStats.health += (15 + upgradePerLevel) * 1.2f;
            PartyController.player.playerdata.basicStats.healthRegen += (0.5f + upgradePerLevel) / 2.5f;
            PartyController.player.playerdata.otherStats.damageReduction += 0.15f;
            if (vitalitylevel == 5)
                PartyController.player.playerdata.basicStats.health += 200;
        }
    }
    public void UpdateMagic_btn()
    {
        AudioManager.instance.PlaySfx("Click");
        if (PartyController.player.playerdata.levelUp.skillPoint > 0)
        {
            PartyController.player.playerdata.upgrade.magiclevel += 1;
            PartyController.player.playerdata.levelUp.skillPoint -= 1;
            float upgradePerLevel = PartyController.player.playerdata.upgrade.magiclevel * 1.5f;
            magiclevel = PartyController.player.playerdata.upgrade.magiclevel;
            PartyController.player.playerdata.basicStats.mana += (10 + upgradePerLevel) * 1.2f;
            PartyController.player.playerdata.basicStats.manaRegen += (0.5f + upgradePerLevel) / 2;
            if (magiclevel == 5)
                PartyController.player.playerdata.basicStats.mana += 100;
        }
    }
    public void UpdateHaste_btn()
    {
        AudioManager.instance.PlaySfx("Click");
        if (PartyController.player.playerdata.levelUp.skillPoint > 0)
        {
            PartyController.player.playerdata.upgrade.hastelevel += 1;
            PartyController.player.playerdata.levelUp.skillPoint -= 1;
            float upgradePerLevel = PartyController.player.playerdata.upgrade.hastelevel * 1.5f;
            hastelevel = PartyController.player.playerdata.upgrade.hastelevel;
            PartyController.player.playerdata.basicStats.movementSpeed += 2f;
            PartyController.player.rangePickup += 0.15f;
            if (hastelevel == 4)
                PartyController.player.playerdata.levelUp.skillPoint += 8;
        }
    }
    public void UpdateGreed_btn()
    {
        AudioManager.instance.PlaySfx("Click");
        if (PartyController.player.playerdata.levelUp.skillPoint > 0)
        {
            PartyController.player.playerdata.upgrade.greedlevel += 1;
            PartyController.player.playerdata.levelUp.skillPoint -= 1;
            float upgradePerLevel = PartyController.player.playerdata.upgrade.greedlevel * 1.5f;
            greedlevel = PartyController.player.playerdata.upgrade.greedlevel;
            PartyController.player.playerdata.extraBuff.extraExp += 1.25f;
            PartyController.player.playerdata.extraBuff.percentDamage += 0.4f;
            if (greedlevel == 15)
                PartyController.player.playerdata.extraBuff.percentDamage += 3f;
        }
    }
    void SetFullText()
    {
        if (PartyController.player.playerdata.upgrade.powerlevel == 15)
        {
            upgradebtn.power_btn.interactable = false;
            upgradebtn.power_btn.GetComponentInChildren<TextMeshProUGUI>().text = "<size=50>MAX</size>";
        }
        if (PartyController.player.playerdata.upgrade.vitalitylevel == 15)
        {
            upgradebtn.vitality_btn.interactable = false;
            upgradebtn.vitality_btn.GetComponentInChildren<TextMeshProUGUI>().text = "<size=50>MAX</size>";
        }
        if (PartyController.player.playerdata.upgrade.magiclevel == 15)
        {
            upgradebtn.magic_btn.interactable = false;
            upgradebtn.magic_btn.GetComponentInChildren<TextMeshProUGUI>().text = "<size=50>MAX</size>";
        }
        if (PartyController.player.playerdata.upgrade.hastelevel == 15)
        {
            upgradebtn.haste_btn.interactable = false;
            upgradebtn.haste_btn.GetComponentInChildren<TextMeshProUGUI>().text = "<size=50>MAX</size>";
        }
        if (PartyController.player.playerdata.upgrade.greedlevel == 15)
        {
            upgradebtn.greed_btn.interactable = false;
            upgradebtn.greed_btn.GetComponentInChildren<TextMeshProUGUI>().text = "<size=50>MAX</size>";
        }
    }
    #endregion

    #region Interact 
    void SetInteractBtn()
    {
        if (PartyController.player.playerdata.levelUp.skillPoint == 0)
        {
            upgradebtn.power_btn.interactable = false;
            upgradebtn.vitality_btn.interactable = false;
            upgradebtn.magic_btn.interactable = false;
            upgradebtn.haste_btn.interactable = false;
            upgradebtn.greed_btn.interactable = false;
        }
        else if (PartyController.player.playerdata.levelUp.skillPoint > 0)
        {
            upgradebtn.power_btn.interactable = true;
            upgradebtn.vitality_btn.interactable = true;
            upgradebtn.magic_btn.interactable = true;
            upgradebtn.haste_btn.interactable = true;
            upgradebtn.greed_btn.interactable = true;
        }
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