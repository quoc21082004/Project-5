using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Scorpion : Enemy
{
    private NavMeshAgent myagent;
    public Transform[] builetPos;
    public GameObject builetprefab;
    float distance;
    protected override void Awake()
    {
        base.Awake();
        myagent = GetComponent<NavMeshAgent>();
        myagent.updateRotation = false;
        myagent.updateUpAxis = false;
        isAlert = false;
    }

    private void Update()
    {
        bool checkDistance = true ? player != null : player == null;
        if (checkDistance)
            distance = Vector3.Distance(player.transform.position, transform.position);
        else
            distance = 0;

        float AlertDistance = distance;
        timer += Time.deltaTime;
        isAlert = true ? AlertDistance <= alertrange : AlertDistance > alertrange;
        if (!isAlert)
            AlertOff();
        else if (isAlert)
        {
            AlertOn();
            if (distance <= range) // attack
            {
                if (timer >= attacktimer)
                {
                    AttackDirec();
                    StartCoroutine(AttackDelay());
                    myagent.SetDestination(-player.transform.position);
                    timer = 0;
                }
            }
            else if (distance > range) // move
            {
                myagent.SetDestination(player.transform.position);
            }
        }
        FlipCharacter();
    }
    private void AttackDirec()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        builetPos[0].rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        builetPos[1].rotation = Quaternion.AngleAxis(angle + 35, Vector3.forward);
        builetPos[2].rotation = Quaternion.AngleAxis(angle - 35, Vector3.forward);
    }
    private void Attack()
    {
        foreach (var item in builetPos)
        {
            GameObject clone = Instantiate(builetprefab, item.transform.position, item.transform.rotation,builetpool.transform);
            Rigidbody2D crb = clone.GetComponent<Rigidbody2D>();
            crb.AddForce(item.up * builetspeed, ForceMode2D.Impulse);
            crb.velocity = Vector2.ClampMagnitude(crb.velocity, builetspeed);
        }
    }
    private IEnumerator AttackDelay()
    {
        Attack();
        yield return new WaitForSeconds(0.2f);
        Attack();
        yield return new WaitForSeconds(0.2f);
        Attack();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alertrange);
    }
}
