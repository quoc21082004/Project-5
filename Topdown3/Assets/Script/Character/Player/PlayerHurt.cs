using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHurt : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject textprefab, gameOverprefab;
    [SerializeField] float thurstKnock = 15f;
    bool isHealthRegen, isManaRegen;
    KnockBack knockBack;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    #region Take Damage & Dead
    public void TakeDamage(Transform hit, float ammount)
    {
        int rand = 50;
        if (PlayerPrefs.GetString("blockDamage") == "true")
            rand = Random.Range(0, 100);
        if (rand > 20)
        {
            if (PlayerPrefs.GetFloat("damageReduction") != 1)
                ammount = ammount - (ammount / PlayerPrefs.GetFloat("damageReduction"));
            player.health -= ammount;
            DamagePopManager.instance.CreateDamagePop(false, ammount, new Vector2(transform.position.x, transform.position.y + 0.75f), transform);
            if (player.health <= 0)
            {
                PartyController.player.gameObject.SetActive(false);
                Instantiate(gameOverprefab, transform.position, Quaternion.identity);
                Time.timeScale = 0;
            }
        }
    }
    #endregion

    #region Heal Regen - Mana Regen

    public void RegenRecover()
    {
        if (player.health < PlayerPrefs.GetFloat("heath") && !isHealthRegen) // health < max health
            StartCoroutine(hpRegen());
        if (player.mana < PlayerPrefs.GetFloat("mana") && !isManaRegen) // mana < max mana
            StartCoroutine(mpRegen());
    }
    IEnumerator hpRegen()
    {
        isHealthRegen = true;
        player.health += PlayerPrefs.GetFloat("healthregen");
        AssetManager.instance.assetData.SpawnRecoverEffect(ConsumableType.HealthPotion, PartyController.player.transform);
        yield return new WaitForSeconds(2.5f);
        isHealthRegen = false;
    }
    IEnumerator mpRegen()
    {
        isManaRegen = true;
        player.mana += PlayerPrefs.GetFloat("manaregen");
        AssetManager.instance.assetData.SpawnRecoverEffect(ConsumableType.ManaPotion, PartyController.player.transform);
        yield return new WaitForSeconds(2.5f);
        isManaRegen = false;
    }
    #endregion

    #region Recover From Potion
    public void PlayerRecoverHP(float hpPots)
    {
        player.health += hpPots;
        DamagePopManager.instance.CreateRecoverPop(ConsumableType.HealthPotion, hpPots, new Vector2(transform.position.x, transform.position.y + 0.75f), PartyController.player.transform);
    }
    public void PlayerRecoverMP(float hpPots)
    {
        player.mana += hpPots;
        DamagePopManager.instance.CreateRecoverPop(ConsumableType.ManaPotion, hpPots, new Vector2(transform.position.x, transform.position.y + 0.75f), PartyController.player.transform);
    }
    #endregion
}
