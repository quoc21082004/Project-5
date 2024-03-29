﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public AttackSO baseAttack;
    [SerializeField] GameObject damageBurstFX;
    Transform builetPool;
    private float rand;
    float critRate, critdamage, percentageDamage, wandDamage;
    private void Awake()
    {
        rand = Random.Range(0f, 101f);
        critRate = PlayerPrefs.GetFloat("critchance");
        critdamage = PlayerPrefs.GetFloat("critDamage");
        percentageDamage = PlayerPrefs.GetFloat("percentageDamage");
        wandDamage = PlayerPrefs.GetFloat("wandDamage") + baseAttack.baseDamage;
        builetPool = GameObject.Find("BuiletPool").GetComponent<Transform>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float totaldamage = wandDamage * critdamage;
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                if (rand < critRate)
                    enemy.gameObject.GetComponent<EnemyHurt>().TakeDamage(totaldamage + (totaldamage * percentageDamage) / 100, true);
                else
                    enemy.gameObject.GetComponent<EnemyHurt>().TakeDamage(wandDamage + (wandDamage * percentageDamage) / 100, false);
            }
            AssetManager.instance.assetData.SpawnBloodSfx(collision);
            Instantiate(damageBurstFX, collision.transform.position, Quaternion.identity, builetPool.transform);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall") && !collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(damageBurstFX, transform.position, Quaternion.identity, builetPool.transform);
            Destroy(this.gameObject);
        }
    }
    protected void BloodSfx(Collider2D collision)
    {
        ParticleSystem ps = Instantiate(AssetManager.instance.assetData.bloodEffect, collision.transform.position, Quaternion.identity, builetPool.transform);
        ps.transform.parent = collision.transform.parent;
    }
}
