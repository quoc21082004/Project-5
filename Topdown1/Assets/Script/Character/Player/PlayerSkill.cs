using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : ActiveAbility
{
    Player player;
    public static Transform MuzzlePoint;
    public GameObject fireballprefab;
    float attackCD = 0.5f;
    bool isAttack = false;
    Transform builetPool;
    public static MouseFollow mouseFollow;
    private void Awake()
    {
        MuzzlePoint = GameObject.FindGameObjectWithTag("Muzzle").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Player>();
        builetPool = GameObject.FindGameObjectWithTag("Pool").GetComponent<Transform>();
        mouseFollow = GetComponentInChildren<MouseFollow>();
    }
    public void CastFireBall()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAttack)
        {
            isAttack = true;
            GameObject clone = Instantiate(fireballprefab, MuzzlePoint.transform.position, mouseFollow.transform.rotation, builetPool.transform);
            StartCoroutine(waitCD(attackCD));
        }
    }
    public void CastSpell()
    {

    }
    IEnumerator waitCD(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = false;
    }

}
