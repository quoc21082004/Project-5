using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer mySR;
    Rigidbody2D myrigid;
    Animator myanim;
    Collider2D mycollider;
    TrailRenderer mytrail;
    [HideInInspector] public PlayerHurt playerhurt;
    [HideInInspector] public PlayerCombat playercombat;
    [HideInInspector] public float maxhealth, maxmana, health, mana, speed;
    [HideInInspector] public bool isAlve = true;
    public DataScriptObject playerdata;
    float move_x, move_y, startSpeed;
    public static bool isFace, canAction;
    public float dashSpeed, dashCD;
    bool isDash;
    [Header("Pick Up")]
    Collider2D[] pickup;
    [HideInInspector] public float rangePickup = 1.2f;
    public LayerMask pickupMask;
    private void Awake()
    {
        startSpeed = speed;
        mySR = GetComponent<SpriteRenderer>();
        myrigid = GetComponent<Rigidbody2D>();
        mycollider = GetComponent<Collider2D>();
        myanim = GetComponent<Animator>();
        playerhurt = GetComponentInParent<PlayerHurt>();
        playercombat = GetComponent<PlayerCombat>();
        mytrail = GetComponentInChildren<TrailRenderer>();
    }
    private void Start()
    {
        canAction = true;
        health = playerdata.basicStats.health;
        mana = playerdata.basicStats.mana;
    }
    private void Update()
    {
        maxhealth = playerdata.basicStats.health;
        maxmana = playerdata.basicStats.mana;
        speed = PlayerPrefs.GetFloat("speed") + (PlayerPrefs.GetFloat("speed") * PlayerPrefs.GetFloat("bounsSpeed")) / 100;
        if (isAlve && canAction)
        {
            PickUp();
            Dash();
            playerhurt.RegenRecover();
            playercombat.CastFireBall();
            playercombat.FindEnemy();
        }
    }
    private void FixedUpdate()
    {
        Move();
        FlipCharacter();
        if (health >= playerdata.basicStats.health)
            health = playerdata.basicStats.health;
        if (mana >= playerdata.basicStats.mana)
            mana = playerdata.basicStats.mana;
    }
    #region move & flip
    private void Move()
    {
        move_x = Input.GetAxis("Horizontal");
        move_y = Input.GetAxis("Vertical");
        Vector2 newvelocity = new Vector2(move_x, move_y).normalized;
        if ((move_x != 0 || move_y != 0) && canAction) 
        {
            myanim.SetFloat("MoveX", move_x);
            myanim.SetFloat("MoveY", move_y);
            myanim.SetBool("Walking", true);
            myrigid.velocity = (newvelocity * speed * Time.deltaTime);
        }
        else if ((move_x == 0 && move_y == 0)) 
        {
            myanim.SetBool("Walking", false);
            myrigid.velocity = Vector2.zero;
        }
    }
    private void FlipCharacter()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenShot = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < playerScreenShot.x)
        {
            mySR.flipX = true;
            isFace = true;
        }
        else
        {
            mySR.flipX = false;
            isFace = false;
        }

    }
    public void SetPosition(Vector3 cords, Vector2 direction)
    {
        Vector2 currentMove = new Vector2(move_x, move_y);
        this.transform.position = cords;
        currentMove = direction;
    }
    #endregion

    #region Dash
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDash)
        {
            isDash = true;
            speed *= dashSpeed;
            mytrail.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }
    private IEnumerator EndDashRoutine()
    {
        float dashTime = 0.2f;
        yield return new WaitForSeconds(dashTime);
        speed = startSpeed;
        yield return new WaitForSeconds(dashCD);
        mytrail.emitting = false;
        isDash = false;
    }
    #endregion

    #region PickUp
    void PickUp()
    {
        pickup = Physics2D.OverlapCircleAll(transform.position, rangePickup, pickupMask);
        if (pickup.Length == 0)
            return;
        if (pickup.Length > 0)
            foreach (var collider in pickup)
            {
                if (collider.gameObject.TryGetComponent<LootItem>(out LootItem loot))
                    loot.StartCoroutine(loot.MoveCourtine());
            }
    }
    #endregion
}

