using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyHurt : MonoBehaviour
{
    public Enemy enemy;
    [SerializeField] float thurst;
    LootSpawner lootspawner;
    Transform builetpool;
    KnockBack knockBack;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        builetpool = GameObject.Find("BuiletPool").GetComponent<Transform>();
        lootspawner = GetComponent<LootSpawner>();
        knockBack = GetComponent<KnockBack>();
    }
    private void Update()
    {
        if (enemy.isDead)
        {
            Destroy(this.gameObject);
        }
    }
    public virtual void TakeDamage(float amount, bool isCrit)
    {
        enemy.heath = Mathf.Clamp(enemy.heath - amount, 0, enemy.maxhealth);
        enemy.myanim.SetTrigger("Hit");
        DamagePopManager.instance.CreateDamagePop(isCrit, amount, new Vector2(transform.position.x, transform.position.y + 0.75f), transform.parent);
        knockBack.GetKnockBack(PartyController.player.transform, thurst);
        if (enemy.heath <= 0)
            EnemyDead();
    }
    private void DropLootItem()
    {
        lootspawner.SpawnLoot(transform.position, builetpool.transform);
        PartyController.AddGold(enemy.enemystat.goldReward);
        GameManager.instance?.AddExperience(enemy.enemystat.expReward);
    }
    public void EnemyDead()
    {
        enemy.isDead = true;
        DropLootItem();
        Destroy(this.gameObject);
    }
}
