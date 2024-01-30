﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public AttackSO baseAttack;
    [SerializeField] GameObject damageBurstFX;
    private float rand;
    float critRate, critdamage, percentageDamage, wandDamage;
    private void OnEnable()
    {
        rand = Random.Range(0f, 101f);
        critRate = PartyController.player.playerdata.basicAttack.critChance;
        critdamage = Random.Range(PartyController.player.playerdata.basicAttack.minCritDamage, PartyController.player.playerdata.basicAttack.maxCritDamage);
        percentageDamage = PartyController.player.playerdata.extraBuff.percentDamage;
        wandDamage = PartyController.player.playerdata.basicAttack.wandDamage + baseAttack.baseDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHurt>(out var cos))
        {
            float totaldamage = wandDamage * critdamage;
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                if (rand < critRate)
                    enemy.gameObject.GetComponent<EnemyHurt>().TakeDamage(totaldamage + (totaldamage * percentageDamage) / 100, true);
                else
                    enemy.gameObject.GetComponent<EnemyHurt>().TakeDamage(wandDamage + (wandDamage * percentageDamage) / 100, false);
            }
            AssetManager.instance.assetData.SpawnBloodSfx(collision.transform);
            GameObject clone = PoolManager.instance.Release(damageBurstFX, collision.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Wall") && !collision.gameObject.CompareTag("Enemy"))
        {
            GameObject clone = PoolManager.instance.Release(damageBurstFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
