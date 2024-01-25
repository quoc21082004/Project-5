using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerHurt : MonoBehaviour
{
    Player player;
    [SerializeField] GameObject gameOverprefab;
    bool isHealthRegen, isManaRegen;

    [SerializeField] UnityEvent OnStartCombat;
    [SerializeField] UnityEvent OnEndCombat;
    
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    #region Take Damage & Dead
    public void TakeDamage(Transform hit, float amount)
    {
        int rand = Random.Range(0, 100);
        if (rand > 20)
        {
            if (PlayerPrefs.GetFloat("damageReduction") != 1)
                amount = amount - (amount / PlayerPrefs.GetFloat("damageReduction"));
            player.health -= amount;
            DamagePopManager.instance.CreateDamagePop(false, amount, new Vector2(transform.position.x, transform.position.y + 0.75f), transform);
            OnStartCombat?.Invoke();
            if (player.health <= 0)
                Dead();
        }
    }
    private void Dead()
    {
        OnEndCombat?.Invoke();
        Time.timeScale = 0;
    }
    #endregion

    #region Heal Regen - Mana Regen

    public void RegenRecover()
    {
        if ((player.health < player.maxhealth) && !isHealthRegen) // health < max health
            StartCoroutine(hpRegen());
        if ((player.mana < player.maxmana) && !isManaRegen) // mana < max mana
            StartCoroutine(mpRegen());
    }
    IEnumerator hpRegen()
    {
        isHealthRegen = true;
        player.health += PartyController.player.playerdata.basicStats.healthRegen;
        AssetManager.instance.assetData.SpawnRecoverEffect(ConsumableType.HealthPotion, transform.position, PartyController.player.transform);
        yield return new WaitForSeconds(2.5f);
        isHealthRegen = false;
    }
    IEnumerator mpRegen()
    {
        isManaRegen = true;
        player.mana += PartyController.player.playerdata.basicStats.manaRegen;
        AssetManager.instance.assetData.SpawnRecoverEffect(ConsumableType.ManaPotion, transform.position, PartyController.player.transform);
        yield return new WaitForSeconds(2.5f);
        isManaRegen = false;
    }
    #endregion

    #region Recover From Potion
    public void PlayerRecoverHP(float hpPots)
    {
        player.health = Mathf.Max(player.health, player.mana + hpPots);
        DamagePopManager.instance.CreateRecoverPop(ConsumableType.HealthPotion, hpPots, new Vector2(transform.position.x, transform.position.y + 0.75f), PartyController.player.transform);
    }
    public void PlayerRecoverMP(float mpPots)
    {
        player.mana = Mathf.Max(player.mana, player.mana + mpPots);
        DamagePopManager.instance.CreateRecoverPop(ConsumableType.ManaPotion, mpPots, new Vector2(transform.position.x, transform.position.y + 0.75f), PartyController.player.transform);
    }
    #endregion
}
