using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boar : Enemy
{
    float Distance;
    NavMeshAgent myagent;
    private Rigidbody2D myrigid;

    protected override void Awake()
    {
        base.Awake();

        myrigid = GetComponent<Rigidbody2D>();
        myagent = GetComponent<NavMeshAgent>();
        myagent.updateRotation = false;
        myagent.updateUpAxis = false;
        isAlert = false;
        timer = 0;
    }

    private void Update()
    {
        bool checkDistance = true ? player != null : player == null;
        if (checkDistance)
            Distance = Vector3.Distance(player.transform.position, transform.position);
        else
            Distance = 0;
        float distanceAlert = Distance;
        isAlert = true ? distanceAlert <= alertrange : distanceAlert > alertrange;
        if (!isAlert)
            AlertOff();
        else if (isAlert)
        {
            AlertOn();
            if (Distance <= range) // Attack
            {
                timer += Time.deltaTime;
                if (timer > attacktimer)
                {
                    StartCoroutine(AttackDelay());
                    timer = 0;
                }
            }
            else if (Distance > range && isAttack) // aatack
                myagent.SetDestination(player.transform.position);
        }
        FlipCharacter();
    }

    IEnumerator AttackDelay()
    {
        isAttack = true;
        yield return new WaitForSeconds(1.0f);
        myanim.SetTrigger("Attack");
        player.GetComponent<PlayerHurt>().TakeDamage(transform, damage);
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.TryGetComponent<Player>(out Player player))
            {
                player.GetComponent<PlayerHurt>().TakeDamage(null, 2);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alertrange);
    }
}
