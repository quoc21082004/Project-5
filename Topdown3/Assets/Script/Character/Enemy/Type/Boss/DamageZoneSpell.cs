using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class DamageZoneSpell : MonoBehaviour, ISpell
{
    Rigidbody2D myrigid;
    [SerializeField] private GameObject ropeprefab;
    [SerializeField] private GameObject explosionprefab;
    [SerializeField] private GameObject indicatorprefab;
    [Header("Info Skill")]
    [SerializeField] private float movespeed;
    [SerializeField] private float changeDirectionInterval;
    [SerializeField] private float activeDelay;
    [SerializeField] private float damageRange;
    [SerializeField] private float blockTime;
    Transform target;
    public LayerMask layerMask;
    private void Awake()
    {
        myrigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void KickOff(ActiveAbility ability, Vector2 position)
    {
        transform.position = position;
        indicatorprefab.gameObject.SetActive(true);
        StartCoroutine(SpellCourtine(ability));
    }
    private IEnumerator SpellCourtine(ActiveAbility ability)
    {
        float activetime = Time.time + activeDelay;
        while (Time.time < activetime)
        {
            myrigid.velocity = (target.position - transform.position).normalized * movespeed;
            yield return new WaitForSeconds(changeDirectionInterval);
        }
        indicatorprefab.gameObject.SetActive(false);
        yield return Explosion(ability);
        Destroy(this.gameObject);
    }
    private IEnumerator Explosion(ActiveAbility ability)
    {
        explosionprefab.gameObject.SetActive(true);
        var colliders = Physics2D.OverlapCircleAll(transform.position, damageRange, layerMask);
        if (colliders.Length == 0)
            yield break;
        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<PlayerHurt>(out PlayerHurt targets))
            {
                targets.TakeDamage(null, ability.skillInfo.baseDamage);
                if (Instantiate(ropeprefab).gameObject.TryGetComponent<GameObject>(out GameObject rope))
                {
                    rope.transform.position = targets.transform.position;
                    rope.transform.parent = target.transform;
                }
            }
        }
        yield return new WaitForSeconds(blockTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRange);
    }
}
