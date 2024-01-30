using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : ActiveAbility 
{
    Player player;
    public static Transform MuzzlePoint;
    public GameObject fireballprefab;
    bool isAttack = false;
    public static MouseFollow mouseFollow;

    public float rangeOfAim;
    Collider2D[] findEnemy;
    public LayerMask enemylayer;
    float angle;
    public Transform aimPos, muzzleFind;
    private void Awake()
    {
        MuzzlePoint = GameObject.FindGameObjectWithTag("Muzzle").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Player>();
        mouseFollow = GetComponentInChildren<MouseFollow>();
    }
    public void FindEnemy()
    {
        findEnemy = Physics2D.OverlapCircleAll(transform.position, rangeOfAim, enemylayer);
        if (findEnemy.Length == 0)
        {
            muzzleFind.transform.rotation = Quaternion.AngleAxis(0, Vector3.right);
            return;
        }
        Collider2D randomEnemy = findEnemy[Random.Range(0, findEnemy.Length)];
        if (randomEnemy.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Vector3 dir = enemy.transform.position - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            muzzleFind.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    public void CastFireBall()
    {
        float attackCD = 0.5f;
        if (Input.GetKeyDown(KeyCode.F) && !isAttack)
        {
            isAttack = true;
            GameObject clone = PoolManager.instance.Release(fireballprefab, MuzzlePoint.transform.position, mouseFollow.transform.rotation);
            StartCoroutine(waitCD(attackCD));
        }
    }
    IEnumerator waitCD(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = false;
    }
}
