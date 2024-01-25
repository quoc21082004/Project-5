using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTotem : Enemy
{
    float Distance;
    public Transform[] BuiletPos;
    GameObject builet;
    public GameObject builetprefab;
    protected override void Awake()
    {
        base.Awake();
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
        timer += Time.deltaTime;
        if (!isAlert)
            AlertOff();
        else
        {
            AlertOn();
            if (Distance <= range) // attack
            {
                if (timer >= attacktimer)
                {
                    GetRotation();
                    StartCoroutine(Attackdelay());
                    timer = 0;
                }
            }
        }
        FlipCharacter();
    }
    private void GetRotation()
    {
        Vector3 direct = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg - 90;

        foreach (var builetpos in BuiletPos)
        {
            builetpos.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    IEnumerator Attackdelay()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (var item in BuiletPos)
        {
            builet = Instantiate(builetprefab, item.position, item.rotation,builetpool.transform);
            Rigidbody2D myrigid = builet.GetComponent<Rigidbody2D>();
            myrigid.AddForce(item.up * builetspeed, ForceMode2D.Impulse);
            myrigid.velocity = Vector2.ClampMagnitude(myrigid.velocity, builetspeed);
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
