using UnityEngine;
using UnityEngine.Events;

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
        DamagePopManager.instance.CreateDamagePop(isCrit, amount, new Vector3(transform.position.x, transform.position.y + 0.75f, 0f), transform);
        knockBack.GetKnockBack(PartyController.player.transform, thurst, transform);
        if (enemy.heath <= 0)
            Dead();
    }
    private void DropLootItem()
    {
        DropManager.instance.SpawnLoot(enemy.enemystat.Type, transform.position);
        PartyController.AddGold(enemy.enemystat.goldReward);
        GameManager.instance?.AddExperience(enemy.enemystat.expReward);
    }
    public void Dead()
    {
        enemy.isDead = true;
        gameObject.SetActive(false);
        DropLootItem();
        OnEndCombat?.Invoke();
    }
}
