using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ExplosionBurstZone : ExplosionBuilet
{
    public float explosionRange;
    public GameObject explosionBurstprefab;
    public LayerMask layerMask;

    protected override void HitTarget()
    {
        base.HitTarget();
        GameObject clone = PoolManager.instance.Release(explosionBurstprefab, transform.position, Quaternion.identity);
        if (clone != null)
        {
            Explosion(transform.position);
            AudioManager.instance.PlaySfx("Explosion");
            Destroy(this.gameObject);
        }
    }
    private float CaculateDamage()
    {
        float totalDamage = ((activeAbility.skillInfo.baseDamage + PlayerPrefs.GetFloat("wandDamage")) * Random.Range(240, 270)) / 115f;
        return totalDamage;
    }
    private void Explosion(Vector2 postion)
    {
        var colliders = Physics2D.OverlapCircleAll(postion, explosionRange, layerMask);
        if (colliders.Length == 0)
            return;
        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<EnemyHurt>(out EnemyHurt enemy))
            {
                bool isCrit = Random.Range(30, 40) > Random.Range(0, 101) ? true : false;
                if (isCrit)
                    enemy.TakeDamage(CaculateDamage() * PlayerPrefs.GetFloat("critDamage"), true);
                else
                    enemy.TakeDamage(CaculateDamage(), false);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
