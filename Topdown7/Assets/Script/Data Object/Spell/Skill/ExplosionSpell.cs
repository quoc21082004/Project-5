using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class ExplosionSpell : MonoBehaviour, ISpell
{
    private ActiveAbility activeAbility;
    public float explosionRadius;
    public LayerMask layerMask;
    public Collider2D[] colliders;
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        activeAbility = ability;
        transform.position = PartyController.player.transform.position;
        transform.parent = PartyController.player.transform;
        Explore();
    }
    private float CaculateDamage()
    {
        float totalDamage = activeAbility.skillInfo.baseDamage + PlayerPrefs.GetFloat("wandDamage");
        return totalDamage;
    }
    public void Explore()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerMask);
        if (colliders.Length == 0)
            return;
        foreach(var colli in colliders)
        {
            if (colli.gameObject.TryGetComponent<EnemyHurt>(out EnemyHurt enemy))
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
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
    
}
