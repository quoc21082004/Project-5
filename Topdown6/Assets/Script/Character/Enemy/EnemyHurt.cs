using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemyHurt : MonoBehaviour
{
    Animator myanim;
    public Enemy enemy;
    [SerializeField] float thurst;
    Transform builetpool;
    KnockBack knockBack;
    [SerializeField] private UnityEvent OnStartCombat;
    [SerializeField] private UnityEvent OnEndCombat;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        //lootspawner = GetComponent<LootSpawner>();
        knockBack = GetComponent<KnockBack>();
        myanim = GetComponent<Animator>();
    }
    public virtual void TakeDamage(float amount, bool isCrit)
    {
        OnStartCombat?.Invoke();
        amount = Mathf.Min(amount, amount - enemy.defense);
        enemy.heath = Mathf.Clamp(enemy.heath - amount, 0, enemy.maxhealth);
        enemy.myanim.SetTrigger("Hit");
        DamagePopManager.instance.CreateDamagePop(isCrit, amount, new Vector2(transform.position.x, transform.position.y + 0.75f), transform);
        knockBack.GetKnockBack(PartyController.player.transform, thurst, transform);
        if (enemy.heath <= 0)
            Dead();
    }
    private void DropLootItem()
    {
        DropManager.instance.SpawnLoot(enemy.enemystat.Type, transform.position, builetpool.transform);
        PartyController.AddGold(enemy.enemystat.goldReward);
        GameManager.instance?.AddExperience(enemy.enemystat.expReward);
    }
    public void Dead()
    {
        enemy.isDead = true;
        DropLootItem();
        OnEndCombat?.Invoke();
    }
}
