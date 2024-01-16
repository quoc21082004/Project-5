using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpell : MonoBehaviour, ISpell
{
    private ActiveAbility activeAbility;
    public float shieldDuration;
    private void Awake()
    {
        Destroy(this.gameObject, shieldDuration);
    }
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        activeAbility = ability;
        transform.position = PartyController.player.transform.position;
        transform.parent = PartyController.player.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyBuilet>(out EnemyBuilet builet))  // destroy builet
        {
            GameObject cloneblock = Instantiate(AssetManager.instance.assetData.blockEffect, collision.transform.position, Quaternion.identity);
            cloneblock.transform.parent = transform.parent;
            DamagePopManager.instance.BlockPop(collision.transform.position, collision.transform.rotation, transform.parent);
            AudioManager.instance.PlaySfx("ShieldBlock");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.TryGetComponent<EnemyHurt>(out EnemyHurt enemy))
        {
            int rate = Random.Range(0, 101);
            bool isCrit = PlayerPrefs.GetFloat("critchance") > rate ? true : false;
            float totalDamage = 0;
            if (isCrit)
                totalDamage = CaculateDamage() * PlayerPrefs.GetFloat("critDamage");
            else
                totalDamage = CaculateDamage();
            enemy.TakeDamage(totalDamage, isCrit);
        }
    }
    float CaculateDamage()
    {
        float totalDamage = activeAbility.skillInfo.baseDamage + PlayerPrefs.GetFloat("wandDamage");
        return totalDamage;
    }
}
