using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Potion")]
public class Potion : Consumable
{
    [Range(0, 100)] public int minPercent;
    [Range(0, 100)] public int maxPercent;
    public override void Use()
    {
        base.Use();
        int random = Random.Range(minPercent, maxPercent);
        switch(consumableType)
        {
            case ConsumableType.HealthPotion:
                float recoverHP = (random * PartyController.player.GetComponent<Player>().maxhealth) / 100;
                PartyController.player.gameObject.GetComponent<PlayerHurt>().PlayerRecoverHP(recoverHP);
                AssetManager.instance.assetData.SpawnRecoverEffect(consumableType, PartyController.player.transform.position, PartyController.player.transform);
                break;
            case ConsumableType.ManaPotion:
                float recoverMP = (random * PartyController.player.GetComponent<Player>().maxmana) / 100;
                PartyController.player.gameObject.GetComponent<PlayerHurt>().PlayerRecoverMP(recoverMP);
                AssetManager.instance.assetData.SpawnRecoverEffect(consumableType, PartyController.player.transform.position, PartyController.player.transform);
                break;
            default:
                break;
        }
    }
}
