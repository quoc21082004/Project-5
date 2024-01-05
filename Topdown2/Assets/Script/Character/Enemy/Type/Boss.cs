using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private BoxCollider2D mycollider;
    public Animator bodyanim;
    // Math
    float distance;
    Vector3 dir;
    float angle;

    [Header("Shooting Rot Builte")]
    public Transform[] builetPos;
    public GameObject builetprefab;
    public Transform builetRot;
    GameObject shootclone;
    public float builetCount;
    public float fireRate;
    public float rotSpeed;
    bool isRotate = false;
    bool isReadyAttack;

    [Space]
    [Header("Hand Attack")]
    public GameObject LeftHand;
    public GameObject RightHand;
    public Transform LeftHandMuzzle;
    public Transform RightHandMuzzle;
    public GameObject Laserprefab;
    public Animator LeftLaser;
    public Animator RightLaser;
    bool moveleft = false;
    bool moveright = false;
    Vector3 playerPos;
    GameObject LaserClone;

    [Space]
    [Header("follow Spike Arrow")]
    public GameObject spikeprefab;
    public Transform[] spikePos;
    public float speedSpike;
    Quaternion SpikeRot;
    GameObject spikeclone;

    protected override void Awake()
    {
        base.Awake();
        mycollider = GetComponent<BoxCollider2D>();
        LeftLaser = GetComponentInChildren<Animator>();
        RightLaser = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isRotate)
        {
            builetRot.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
        }

        if (!isReadyAttack)
        {
            isReadyAttack = true;
            StartCoroutine(randomAttack()); // random 0-3 type
        }

        if (moveright) // Lerp : move from start to end point
        {
            RightHand.transform.position = Vector3.Lerp(RightHand.transform.position, new Vector2(RightHand.transform.position.x, playerPos.y), 5f * Time.deltaTime);
        }
        if (moveleft)
        {
            LeftHand.transform.position = Vector3.Lerp(LeftHand.transform.position, new Vector2(LeftHand.transform.position.x, playerPos.y), 5f * Time.deltaTime);
        }
    }
    IEnumerator rotateAttack()
    {
        isRotate = true;
        int i = 0;
        while (i < builetCount)
        {
            RotateShooting();
            i++;
            yield return new WaitForSeconds(fireRate);
        }
        isRotate = false;
        isReadyAttack = false;
    }
    private void RotateShooting()
    {
        foreach (var item in builetPos)
        {
            shootclone = Instantiate(builetprefab, item.position, item.rotation, builetpool);
            Rigidbody2D myrigid = shootclone.GetComponent<Rigidbody2D>();
            myrigid.AddForce(item.up * builetspeed, ForceMode2D.Impulse);
            myrigid.velocity = Vector2.ClampMagnitude(myrigid.velocity, builetspeed);
        }
    }

    IEnumerator LasetAttack()
    {
        RightLaser.SetTrigger("Attack");
        LeftLaser.SetTrigger("Attack");

        moveleft = true;
        playerPos = player.transform.position;
        yield return new WaitForSeconds(1f);
        LaserShoot(false);
        yield return new WaitForSeconds(1f);
        moveleft = false;

        moveright = true;
        playerPos = player.transform.position;
        yield return new WaitForSeconds(1f);
        LaserShoot(true);
        yield return new WaitForSeconds(1f);
        moveright = false;

        moveleft = true;
        playerPos = player.transform.position;
        yield return new WaitForSeconds(1f);
        LaserShoot(false);
        yield return new WaitForSeconds(1f);
        moveleft = false;

        moveright = true;
        playerPos = player.transform.position;
        yield return new WaitForSeconds(1f);
        LaserShoot(true);
        yield return new WaitForSeconds(1f);
        moveright = false;

        yield return new WaitForSeconds(3f);
        LeftLaser.ResetTrigger("Attack");
        RightLaser.ResetTrigger("Attack");
        isReadyAttack = false;
    }

    private void LaserShoot(bool isRight)
    {
        if (isRight)
        {
            LaserClone = Instantiate(Laserprefab, RightHandMuzzle.transform.position, Quaternion.identity, builetpool);
            Destroy(LaserClone, 3f);
        }
        else if (!isRight)
        {
            LaserClone = Instantiate(Laserprefab, LeftHandMuzzle.transform.position, Quaternion.identity, builetpool);
            LaserClone.transform.localScale = new Vector2(-1, 1);
            Destroy(LaserClone, 3f);
        }
    }

    IEnumerator SpikeAttack()
    {
        bodyanim.Play("SpikeAttack");

        foreach (var item in spikePos)
        {
            spikeclone = Instantiate(spikeprefab, item.position, Quaternion.identity,builetpool);
            yield return new WaitForSeconds(1f);
            dir = player.transform.position - item.transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            SpikeRot = Quaternion.AngleAxis(angle - 180, Vector3.forward);
            item.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Rigidbody2D crb = spikeclone.GetComponent<Rigidbody2D>();
            crb.AddForce(item.up * speedSpike, ForceMode2D.Impulse);
            crb.velocity = Vector3.ClampMagnitude(crb.velocity, speedSpike);
            spikeclone.GetComponent<Transform>().rotation = SpikeRot;
            yield return new WaitForSeconds(0.8f);
        }
        bodyanim.Play("bossIdle");
        isReadyAttack = false;
    }

    private void SpikeFollow(int i)
    {
        dir = player.transform.position - spikePos[i].transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        SpikeRot = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    }
    IEnumerator randomAttack()
    {
        int randomtype = Random.Range(1, 3);
        yield return new WaitForSeconds(4f);
        switch (randomtype)
        {
            case 1:
                StartCoroutine(rotateAttack());
                break;
            case 2:
                StartCoroutine(LasetAttack());
                break;
            case 3:
                StartCoroutine(SpikeAttack());
                break;
        }
    }
}

