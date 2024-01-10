using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Bat : Enemy
{
    public GameObject builetprefab;
    private PolygonCollider2D mycollider;
    NavMeshAgent myagent;
    float Distance;
    GameObject clone;
    public Transform[] builetPos;

    protected override void Awake()
    {
        base.Awake();

        isAlert = false;
        myagent = GetComponent<NavMeshAgent>();
        myagent.updateRotation = false;
        myagent.updateUpAxis = false;

        timer = 0;

        mycollider = GetComponent<PolygonCollider2D>();
        mySR = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        bool checkDistance = true ? player != null : player == null;
        if (checkDistance)
            Distance = Vector3.Distance(player.transform.position, transform.position);
        else
            Distance = 0;
        float AlertDistance = Distance;
        timer += Time.deltaTime;
        isAlert = true ? AlertDistance <= alertrange : AlertDistance > alertrange;
        if (!isAlert)
            AlertOff();
        else if (isAlert)
        {
            AlertOn();
            if (Distance <= range) // attack
                if (timer >= attacktimer)
                {
                    GetRotation();
                    StartCoroutine(DelayAttack());
                    myagent.SetDestination(-player.transform.position);
                    timer = 0;
                }
                else if (Distance > range) // move
                {
                    myagent.SetDestination(player.transform.position);
                }
        }
        FlipCharacter();
    }
    private void Attack()
    {
        foreach (var direct in builetPos)
        {
            clone = Instantiate(builetprefab, direct.position, direct.rotation, builetpool.transform);
            Rigidbody2D crb = clone.GetComponent<Rigidbody2D>();
            crb.AddForce(direct.up * builetspeed, ForceMode2D.Impulse);
            crb.velocity = Vector2.ClampMagnitude(crb.velocity, builetspeed);
        }
    }
    private void GetRotation()
    {
        Vector3 direction = player.transform.position - transform.position; // 5-3
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        foreach (var item in builetPos)
        {
            angle -= 45;
            item.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // angle 45 - 90 - 135 - 180 - 225 - 270 - 315 - 360
        }
    }
    IEnumerator DelayAttack()
    {
        myagent.enabled = false;
        yield return new WaitForSeconds(1.0f);
        Attack();
        yield return new WaitForSeconds(0.2f);
        Attack();
        yield return new WaitForSeconds(0.2f);
        Attack();
        yield return new WaitForSeconds(0.2f);
        Attack();
        yield return new WaitForSeconds(1.0f);
        myagent.enabled = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alertrange);
    }
}
