using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer mySR;
    Rigidbody2D myrigid;
    Animator myanim;
    [HideInInspector] public PlayerHurt playerhurt;
    [HideInInspector] public PlayerAttack playerAttack;
    [HideInInspector] public PlayerSkill playerSkill;
    [HideInInspector] public float maxhealth, maxmana, health, mana, speed;
    [HideInInspector] public bool isAlve = true;

    Collider2D mycollider;
    public TrailRenderer mytrail;
    public DataScriptObject playerdata;
    float move_x, move_y, startSpeed;
    public static bool isFace;
    public float dashSpeed, dashCD;
    bool isDash;
    public static bool near = false;
    [Header("Pick Up")]
    Collider2D[] pickup;
    public Collider2D[] interactCollider;
    [HideInInspector] public float rangePickup = 1.2f;
    public LayerMask pickupMask, interactLayer;
    private void Awake()
    {
        startSpeed = speed;
        mySR = GetComponent<SpriteRenderer>();
        myrigid = GetComponent<Rigidbody2D>();
        mycollider = GetComponent<Collider2D>();
        myanim = GetComponent<Animator>();
        playerAttack = GetComponentInParent<PlayerAttack>();
        playerhurt = GetComponentInParent<PlayerHurt>();
        playerSkill = GetComponent<PlayerSkill>();
    }
    private void Start()
    {
        health = PlayerPrefs.GetFloat("heath");
        PlayerPrefs.SetFloat("currenthealth", health);
        mana = PlayerPrefs.GetFloat("mana");
        PlayerPrefs.SetFloat("currentmana", mana);
    }
    private void Update()
    {
        maxhealth = playerdata.basicStats.health;
        maxmana = playerdata.basicStats.mana;
        PlayerPrefs.SetFloat("currentmana", mana);
        PlayerPrefs.SetFloat("currenthealth", health);
        speed = PlayerPrefs.GetFloat("speed") + (PlayerPrefs.GetFloat("speed") * PlayerPrefs.GetFloat("bounsSpeed")) / 100;
        if (isAlve)
        {
            playerAttack.FindEnemy();
            playerhurt.RegenRecover();
            PickUp();
            InteractablePlayer();
            Dash();
            playerSkill.CastFireBall();
            playerSkill.CastSpell();
        }
    }
    private void FixedUpdate()
    {
        Move();
        FlipCharacter();
        if (health >= PlayerPrefs.GetFloat("heath"))
            health = PlayerPrefs.GetFloat("heath");
        if (mana >= PlayerPrefs.GetFloat("mana"))
            mana = PlayerPrefs.GetFloat("mana");

    }
    #region move & flip
    private void Move()
    {
        move_x = Input.GetAxis("Horizontal");
        move_y = Input.GetAxis("Vertical");
        Vector2 newvelocity = new Vector2(move_x, move_y).normalized;
        if (move_x != 0 || move_y != 0)
        {
            myanim.SetFloat("MoveX", move_x);
            myanim.SetFloat("MoveY", move_y);
            myanim.SetBool("Walking", true);
            myrigid.velocity = (newvelocity * speed * Time.deltaTime);
        }
        else if (move_x == 0 && move_y == 0)
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
    void InteractablePlayer()
    {
        interactCollider = Physics2D.OverlapCircleAll(transform.position, 1.4f, interactLayer);
        if (interactCollider.Length == 0)
        {
            near = false;
            return;
        }
        near = true;
        if (Input.GetKeyDown(KeyCode.Z) && near && interactCollider.Length > 0 && !DialogueManager.isDialogueOpen && !DialogueManager.instance.dialogueBox.activeSelf)
            foreach (var collider in interactCollider)
            {
                if (collider.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
                    interactable.Interact();
            }
    }
    #endregion
}

