using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSpell : MonoBehaviour, ISpell
{
    private ActiveAbility activeAbility;
    public GameObject lightprefab;
    public float lightRadius;
    public float duration;
    public Vector3 spellOffSet;
    private float lightInterval = 1.5f;
    Collider2D[] colliders;
    public LayerMask layerMask;
    private float CaculateDamage()
    {
        float totalDamage = ((activeAbility.skillInfo.baseDamage + PlayerPrefs.GetFloat("wandDamage")) * Random.Range(150f, 200f)) / 100;
        return totalDamage;
    }
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        activeAbility = ability;
        transform.position = PartyController.player.transform.position + spellOffSet;
        transform.parent = PartyController.player.transform;
        StartCoroutine(AttackCourtine());
    }
    private IEnumerator AttackCourtine()
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, lightRadius, layerMask);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<EnemyHurt>(out EnemyHurt enemy))
                {
                    if (enemy != null)
                    {
                        GameObject cloneLighting = PoolManager.instance.Release(lightprefab, enemy.transform.position + spellOffSet, Quaternion.Euler(0, 0, 40f));
                        cloneLighting.gameObject.transform.position = enemy.transform.position + spellOffSet;
                        cloneLighting.gameObject.transform.rotation = Quaternion.Euler(0, 0, 40f);
                        cloneLighting.gameObject.transform.parent = enemy.transform;
                        if (cloneLighting != null)
                        {
                            bool isCrit = PlayerPrefs.GetFloat("critchance") > Random.Range(0, 101) ? true : false;
                            if (isCrit)
                                enemy.TakeDamage(CaculateDamage() * PlayerPrefs.GetFloat("critDamage"), isCrit);
                            else
                                enemy.TakeDamage(CaculateDamage(), isCrit);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(lightInterval);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lightRadius);
    }
}
