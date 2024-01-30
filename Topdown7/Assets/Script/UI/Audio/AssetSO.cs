using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class AssetSO : ScriptableObject
{
    [Header("Recover Regen Effect")]
    public ParticleSystem healthEffect;
    public ParticleSystem manaEffect;

    [Header("Death and blood")]
    public ParticleSystem bloodEffect;

    [Header("KnockBack Effect")]
    public ParticleSystem knockbackEffect;
    public GameObject blockEffect;
    public void SpawnBloodSfx(Transform bloodPos)
    {
        GameObject ps = PoolManager.instance.Release(bloodEffect.gameObject, bloodPos.position, Quaternion.identity);
        //ps.transform.parent = collision.transform;
    }
    public void SpawnRecoverEffect(ConsumableType type,Vector3 position, Transform tf)
    {
       switch(type)
        {
            case ConsumableType.HealthPotion:
                GameObject ps1 = PoolManager.instance.Release(healthEffect.gameObject, position, Quaternion.identity);
                ps1.transform.parent = PartyController.player.transform;
                break;
            case ConsumableType.ManaPotion:
                GameObject ps2 = PoolManager.instance.Release(manaEffect.gameObject, position, Quaternion.identity);
                ps2.transform.parent = PartyController.player.transform;
                break;
            default:
                break;
        }
    }
}
