using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStormSpell : MonoBehaviour, ISpell
{
    const string Animation_Hash = "Storm";
    private Animator myanim;
    private Rigidbody2D myrigid;
    public float startDelay;
    public float lifeTime;
    public float speed;
    public float damagePerInterval;
    public Vector2 damageZoneSize;
    Collider2D[] colliders;
    public LayerMask layerMask;
    private ActiveAbility activeAbility;
    private float endTime;
    private Transform target;
    private void Awake()
    {
        myanim = GetComponent<Animator>();
        myrigid = GetComponent<Rigidbody2D>();
    }
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        activeAbility = ability;
        target = PartyController.player.transform;
        transform.position = direction;
        StartCoroutine(StormCouritne(ability));
    }
    private IEnumerator StormCouritne(ActiveAbility ability)
    {
        yield return new WaitForSeconds(startDelay);
        myanim.Play(Animation_Hash);
        endTime = Time.time + lifeTime;
        StartCoroutine(DamageCourtine(ability));
        while (Time.time < endTime)
        {
            var direction = (target.position - transform.position).normalized;
            myrigid.velocity = direction * speed;
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(this.gameObject);
    }
    private IEnumerator DamageCourtine(ActiveAbility ability)
    {
        while (Time.time < endTime)
        {
            colliders = Physics2D.OverlapBoxAll(transform.position, damageZoneSize, 0, layerMask);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<PlayerHurt>(out PlayerHurt target))
                {
                    target.TakeDamage(null, ability.skillInfo.baseDamage);
                }
            }
            yield return new WaitForSeconds(damagePerInterval);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, damageZoneSize);
    }
}
