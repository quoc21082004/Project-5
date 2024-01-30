using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingMelee : Enemy
{
    float Distance;
    bool isDMG;
    NavMeshAgent myagent;
    private BoxCollider2D mybox;
    private CircleCollider2D mycircle;

    protected override void Awake()
    {
        base.Awake();
        isAttack = false;
        isAlert = false;
        isDMG = false;

        mybox = GetComponent<BoxCollider2D>();
        mycircle = GetComponent<CircleCollider2D>();
        myagent = GetComponent<NavMeshAgent>();
        myagent.updateRotation = false;
        myagent.updateUpAxis = false;

        timer = 0;
    }

    private void Update()
    {
        Distance = Vector2.Distance(player.transform.position, transform.position);
        float distanceAlert = Distance;
        isAlert = true ? distanceAlert <= alertrange : distanceAlert > alertrange;

        if (!isAlert)
            AlertOff();
        else if (isAlert)
        {
            AlertOn();
            if (Distance < range) // Attack
            {
                timer += Time.deltaTime;
                if (timer >= attacktimer)  // Attack
                {
                    StartCoroutine(attackdelay());
                    timer = 0;
                }
            }
            else if (Distance > range && !isAttack) // move
                myagent.SetDestination(player.transform.position);
        }
        FlipCharacter();
    }
    IEnumerator attackdelay()
    {
        isAttack = true;
        yield return new WaitForSeconds(1.0f);
        myanim.SetTrigger("Attack");
        yield return new WaitForSeconds(1.3f);
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.TryGetComponent<Player>(out Player player))
                if (isAttack)
                    if (!isDMG)
                    {
                        isDMG = true;
                        player.GetComponent<PlayerHurt>().TakeDamage(transform, damage);
                        StartCoroutine(dmgCD());
                    }
        }
    }
    IEnumerator dmgCD()
    {
        yield return new WaitForSeconds(0.3f);
        isDMG = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alertrange);
    }
}
