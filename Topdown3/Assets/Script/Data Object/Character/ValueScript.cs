using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueScript : Singleton<ValueScript>
{
    public DataScriptObject playerdata;
    private void Update()
    {
        float randomCritDamage = Random.Range(playerdata.basicAttack.minCritDamage, playerdata.basicAttack.maxCritDamage);
        PlayerPrefs.SetFloat("critDamage", randomCritDamage);
    }
    private void Awake()
    {
        // basic stats
        PlayerPrefs.SetFloat("heath", playerdata.basicStats.health);
        PlayerPrefs.SetFloat("mana", playerdata.basicStats.mana);
        PlayerPrefs.SetFloat("speed", playerdata.basicStats.movementSpeed);
        PlayerPrefs.SetFloat("healthregen", playerdata.basicStats.healthRegen);
        PlayerPrefs.SetFloat("manaregen", playerdata.basicStats.manaRegen);
        PlayerPrefs.Save();

        // basic attack
        PlayerPrefs.SetFloat("wandDamage", playerdata.basicAttack.wandDamage);
        PlayerPrefs.SetFloat("percentageDamage", playerdata.extraBuff.percentDamage);
        PlayerPrefs.SetFloat("attackSpeed", playerdata.basicAttack.attackSpeed);
        PlayerPrefs.SetFloat("builetSpeed", playerdata.basicAttack.builetSpeed);
        PlayerPrefs.SetFloat("cdr", playerdata.basicAttack.coolDown);
        PlayerPrefs.SetFloat("critchance", playerdata.basicAttack.critChance);
        PlayerPrefs.Save();

        // basic level
        PlayerPrefs.SetInt("level", playerdata.levelUp.level) ;
        PlayerPrefs.SetInt("skillpoint", playerdata.levelUp.skillPoint);
        PlayerPrefs.SetFloat("Exp", playerdata.levelUp.exp);
        PlayerPrefs.SetFloat("Explevelup", playerdata.levelUp.expToLvl);
        PlayerPrefs.Save();

        // basic buff
        PlayerPrefs.SetInt("extraCoins", playerdata.extraBuff.extraCoins);
        PlayerPrefs.SetFloat("extraExp", playerdata.extraBuff.extraExp);
        PlayerPrefs.SetFloat("bounsSpeed", playerdata.extraBuff.extraSpeedMove);
        PlayerPrefs.SetFloat("bonusAttack", playerdata.extraBuff.extraSpeedAttack);
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("costMP", playerdata.otherStats.costMP);
        PlayerPrefs.SetInt("doubleDrop", 1);
        PlayerPrefs.SetFloat("damageReduction", playerdata.otherStats.damageReduction);
        PlayerPrefs.SetString("blockDamage", "false");
        PlayerPrefs.SetInt("isDamageInt", 0);
        PlayerPrefs.Save();

        // menu
        PlayerPrefs.SetInt("isDamageInd", 0);
        PlayerPrefs.SetInt("isCheat", 0);
        PlayerPrefs.SetInt("isShake", 0);
        PlayerPrefs.SetInt("isHealth", 0);
        PlayerPrefs.SetInt("isMana", 0);
        PlayerPrefs.SetInt("isDamage", 0);
        PlayerPrefs.Save();

        // system upgrade
        PlayerPrefs.SetInt("powerlevel", playerdata.upgrade.powerlevel);
        PlayerPrefs.SetInt("magiclevel", playerdata.upgrade.magiclevel);
        PlayerPrefs.SetInt("hastelevel", playerdata.upgrade.hastelevel);
        PlayerPrefs.SetInt("vitalitylevel", playerdata.upgrade.vitalitylevel);
        PlayerPrefs.SetInt("greedlevel", playerdata.upgrade.greedlevel);
        PlayerPrefs.Save();

        // Skill Upgrade
        PlayerPrefs.SetString("doubleShot", "false");
        PlayerPrefs.SetString("BoomAOE", "false");
        PlayerPrefs.SetString("immueSlow", "false");
    }
}
