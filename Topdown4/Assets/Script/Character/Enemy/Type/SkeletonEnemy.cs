using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonEnemy : Enemy
{
    float Distance;
    NavMeshAgent agent;
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
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
        else if (isAlert)
        {
            AlertOn();
            if (Distance <= range)
            {
                isWithIn = true;
                timer += Time.deltaTime;
                if (timer >= attacktimer)
                {
                    StartCoroutine(attackDelay());
                    timer = 0;
                }
            }
            else if (Distance > range && !isAttack)
            {
                isWithIn = false;
                agent.SetDestination(player.transform.position);
            }
        }
        if (!isWithIn)
            myanim.SetBool("Run", true);
        else
            myanim.SetBool("Run", false);
        FlipCharacter();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, alertrange);
    }
    IEnumerator attackDelay()
    {
        isAttack = true;
        yield return new WaitForSeconds(0.5f);
        myanim.SetTrigger("Attack");
        if (Distance <= range)
            player.gameObject.GetComponent<PlayerHurt>().TakeDamage(transform, damage);
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHurt>().TakeDamage(null, 2);
        }
    }
}
