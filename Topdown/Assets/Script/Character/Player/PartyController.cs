using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : Singleton<PartyController>
{
    public static Player player;
    public static InventoryG inventoryG;
    public static Player playerController;
    public static QuestChain questChain;
    public static QuestSO quest;
    private void Update()
    {
        if (inventoryG == null)
            inventoryG = new InventoryG { Gold = 0 };

        if (player == null)
            player = transform.GetChild(0).gameObject.GetComponent<Player>();


        if (ItemHotKeyManager.instance == null)
            return;
        else
        {
            bool[] hotkeyInputs = new bool[2]
            {
                Input.GetKeyDown(KeyCode.Alpha1),
                Input.GetKeyDown(KeyCode.Alpha2),
            };
            for (int i = 0; i < ItemHotKeyManager.instance.NumOfHotKey; i++)
                if (hotkeyInputs[i] && !ItemHotKeyManager.instance.IsHotKeyItemOnCoolDown(i)) // cool down = false (run)
                    ItemHotKeyManager.instance.UseHotKeyItem(i);
        }
        if (DialogueManager.instance.dialogueBox.activeSelf && DialogueManager.isDialogueOpen) 
            if (Input.GetKeyDown(KeyCode.Z))
                DialogueManager.instance.SkipDialogue(Input.GetKeyDown(KeyCode.K));
    }
    public void Respawn(int lostRateExp,int lostRateGold)
    {
        player.isAlve = true;
        player.gameObject.SetActive(true);
        //player.exp -= (int)((lostRateExp * player.exp) / 100);
        GameManager.instance.RespawnAfterDie(lostRateExp);
        inventoryG.Gold -= (int)((lostRateGold * PartyController.inventoryG.Gold) / 100);
    }
    public void FullRestore()
    {
        player.health = Mathf.Max(player.health, player.maxhealth);
        player.mana += Mathf.Max(player.mana, player.maxmana);
    }

    public static void EnemyKilled(string tag)
    {
        if (quest != null && quest.isActive)
        {
            quest.questGoal.EnemyKilled(tag);
            if (quest.questGoal.IsReached())
            {
                AddGold(quest.goldReward);
                AddExperience(quest.expReward);
                QuestManager.instance.OpenComplete(quest, questChain);
            }
        }
    }
    public static void ItemGet(string tag, int amt)
    {
        if (quest != null && quest.isActive)
        {
            quest.questGoal.ItemGet(tag, amt);
            if (quest.questGoal.IsReached())
            {
                AddGold(quest.goldReward);
                AddExperience(quest.expReward);
                QuestManager.instance.OpenComplete(quest, questChain);
            }
        }
    }
    public static void AddGold(int amount)
    {
        inventoryG.Gold += amount;
        PlayerPrefs.SetInt("coins", inventoryG.Gold);
    }
    public static void AddExperience(float amount)
    {
        //player.AddExperience(amount);
        GameManager.instance.AddExperience(amount);
    }
}
