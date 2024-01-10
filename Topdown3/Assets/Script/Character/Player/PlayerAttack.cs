using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [HideInInspector] public Player player;
    [HideInInspector] public Animator myanim;
    [Header("Find Enemy")]
    public float rangeOfAim;
    Collider2D[] findEnemy;
    public LayerMask enemylayer;
    [HideInInspector] public float attacktimer = 0f;
    [HideInInspector] public float attackCD = 0.5f;
    float angle;
    public Transform aimPos, MuzzlePoint;//, builetPool;
    [HideInInspector] public bool isCanAttack, isDoubleShot;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        attacktimer = PlayerPrefs.GetFloat("attackSpeed");
    }
    public void FindEnemy()
    {
        findEnemy = Physics2D.OverlapCircleAll(transform.position, rangeOfAim, enemylayer);
        if (findEnemy.Length == 0)
        {
            MuzzlePoint.transform.rotation = Quaternion.AngleAxis(0, Vector3.right);
            return;
        }
        Collider2D randomEnemy = findEnemy[Random.Range(0, findEnemy.Length)];
        if (randomEnemy.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Vector3 dir = enemy.transform.position - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            MuzzlePoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
