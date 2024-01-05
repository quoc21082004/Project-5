using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public EnemySO enemystat;
    public GameObject Alertprefab;
    public Vector3 alertOffSet;
    [HideInInspector] public SpriteRenderer mySR;
    [HideInInspector] public TypeEnemy type;
    [HideInInspector] public Animator myanim;
    [HideInInspector] public float maxhealth, heath, damage;
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
        maxhealth = enemystat.heath;
        heath = enemystat.heath;
        damage = enemystat.damage;
        attacktimer = enemystat.attackTimer;
        alertrange = enemystat.alertRange;
        range = enemystat.range;
        builetspeed = enemystat.builetSpeed;
        isDead = enemystat.isDead;
        isAlert = enemystat.isAlert;
        isAttack = enemystat.isAttack;
        PlayerPrefs.SetFloat("enemybuilet", damage);

        builetpool = GameObject.FindGameObjectWithTag("Pool").GetComponent<Transform>();
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
                GameObject alertClone = Instantiate(Alertprefab, transform.position + alertOffSet, Quaternion.identity);
                alertClone.transform.parent = transform;
            }
            turnOnAlert = true;
            enemyUIManager.gameObject.SetActive(true);
            enemyUIManager.enabled = true;
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
