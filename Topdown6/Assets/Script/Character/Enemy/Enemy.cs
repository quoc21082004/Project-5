using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemySO enemystat;
    public GameObject Alertprefab;
    public Vector3 alertOffSet;
    [HideInInspector] public SpriteRenderer mySR;
    [HideInInspector] public TypeEnemy type;
    [HideInInspector] public Animator myanim;
    [HideInInspector] public float maxhealth, heath, damage, defense;
    [HideInInspector] public bool isDead;
    Rigidbody2D myrigid;
    protected GameObject player;
    protected Transform builetpool;
    protected float timer, attacktimer;
    protected float alertrange, range;
    protected float builetspeed;
    protected bool isBoss, isAlert, isAttack, isWithIn;
    bool turnOnAlert;
    public EnemyUIManager enemyUIManager;
    protected virtual void Awake()
    {
        maxhealth = enemystat.heath;// + (GameManager.instance.level + enemystat.growstats.healthGrow);
        heath = enemystat.heath;
        defense = enemystat.defense;// + (GameManager.instance.level + enemystat.growstats.defenseGrow);
        damage = enemystat.damage;// + (GameManager.instance.level + enemystat.growstats.damageGrow);
        attacktimer = enemystat.attackTimer;
        alertrange = enemystat.alertRange;
        range = enemystat.range;
        builetspeed = enemystat.builetSpeed;
        isDead = enemystat.isDead;
        isAlert = enemystat.isAlert;
        isAttack = enemystat.isAttack;
        player = GameObject.FindGameObjectWithTag("Player");
        myanim = GetComponent<Animator>();
        mySR = GetComponent<SpriteRenderer>();
        enemyUIManager = GetComponentInChildren<EnemyUIManager>();
        myrigid = GetComponent<Rigidbody2D>();
    }
    public virtual void FlipCharacter()
    {
        if (transform.position.x > player.transform.position.x)
            mySR.flipX = false;
        else if (transform.position.x < player.transform.position.x)
            mySR.flipX = true;
    }
    public void AlertOn()
    {
        if (!isAlert)
            return;
        if (isAlert)
        {
            if (!turnOnAlert)
            {
                GameObject alertClone = PoolManager.instance.Release(Alertprefab, transform.position + alertOffSet, Quaternion.identity,transform);
                alertClone.transform.parent = transform;
            }
            turnOnAlert = true;
            if (enemyUIManager != null)
            {
                enemyUIManager.gameObject.SetActive(true);
                enemyUIManager.enabled = true;
            }
        }
    }
    public void AlertOff()
    {
        if (isAlert)
            return;
        enemyUIManager.gameObject.SetActive(false);
        enemyUIManager.enabled = false;
    }
}
