using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class AssetSO : ScriptableObject
{
    [Header("Recover Regen Effect")]
    public ParticleSystem healthEffect;
    public ParticleSystem manaEffect;

    [Header("Player Effect")]
    public ParticleSystem dashInEffect;
    public ParticleSystem dashOutEffect;

    [Header("Death and blood")]
    public ParticleSystem bloodEffect;
    public ParticleSystem deathEffect;

    [Header("KnockBack Effect")]
    public ParticleSystem knockbackEffect;
    public GameObject blockEffect;
    public void SpawnDeathEffect(Transform tf)
    {
        ParticleSystem ps = Instantiate(deathEffect, tf.transform.position, Quaternion.identity);
        ps.transform.parent = tf.transform;
    }
    public void SpawnBloodSfx(Collider2D collision)
    {
        GameObject ps = Instantiate(bloodEffect.gameObject, collision.transform.position, Quaternion.identity);
        ps.transform.parent = collision.transform;
    }
    public void SpawnRecoverEffect(ConsumableType type,Vector3 position, Transform tf)
    {
       switch(type)
        {
            case ConsumableType.HealthPotion:
                GameObject ps1 = PoolManager.instance.Release(healthEffect.gameObject, position, Quaternion.identity);
                //ParticleSystem ps1 = Instantiate(healthEffect, tf.transform.position, Quaternion.identity);
                ps1.transform.parent = PartyController.player.transform;
                break;
            case ConsumableType.ManaPotion:
                GameObject ps2 = PoolManager.instance.Release(manaEffect.gameObject, position, Quaternion.identity);
                //ParticleSystem ps2 = Instantiate(manaEffect, tf.transform.position, Quaternion.identity);
                ps2.transform.parent = PartyController.player.transform;
                break;
            default:
                break;
        }
    }
}
