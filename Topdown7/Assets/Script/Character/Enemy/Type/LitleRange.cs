using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LitleRange : Enemy
{
    public GameObject builetprefab;
    public Transform MuzzlePoint;
    [HideInInspector] public float Distance;
    NavMeshAgent myagent;
    private CircleCollider2D mycollider;
    protected override void Awake()
    {
        base.Awake();
        timer = 0;
        attacktimer = 2f;
        mycollider = GetComponent<CircleCollider2D>();
        myagent = GetComponent<NavMeshAgent>();
        myagent.updateRotation = false;
        myagent.updateUpAxis = false;
        isAlert = false;
    }

    private void Update()
    {
        bool checkDistance = true ? player != null : player == null;
        if (checkDistance)
            Distance = Vector3.Distance(player.transform.position, transform.position);
        else
            Distance = 0;

        float alertDis = Distance;
        isAlert = true ? alertDis <= alertrange : alertDis > alertrange;
        if (!isAlert)
            AlertOff();
        else if (isAlert == true)
        {
            AlertOn();
            if (Distance <= range)
            {
                timer += Time.deltaTime;
                if (timer >= attacktimer)
                {
                    Vector3 dir = player.transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                    MuzzlePoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    Attack();
                    myagent.SetDestination(-player.transform.position);
                    timer = 0;
                }
            }
            else if (Distance > range)
                myagent.SetDestination(player.transform.position);
        }
        FlipCharacter(); // rotate
    }

    void Attack()
    {
        timer = 0;
        builetprefab.gameObject.GetComponent<EnemyBuilet>().realdamage = enemystat.damage;
        GameObject clone = PoolManager.instance.Release(builetprefab, MuzzlePoint.position, MuzzlePoint.rotation);
        Rigidbody2D crb = clone.GetComponent<Rigidbody2D>();
        crb.AddForce(MuzzlePoint.up * builetspeed, ForceMode2D.Impulse);
        crb.velocity = Vector2.ClampMagnitude(crb.velocity, builetspeed);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alertrange);
    }
}


