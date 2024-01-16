using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExplosionBuilet : MonoBehaviour, ISpell
{
    Rigidbody2D myrigid;
    public float lifeTime;

    protected ActiveAbility activeAbility;
    protected Transform builetPool;
    private void Awake()
    {
        myrigid = GetComponent<Rigidbody2D>();
        builetPool = GameObject.FindGameObjectWithTag("Pool").GetComponent<Transform>();
        transform.position = PlayerSkill.MuzzlePoint.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHurt>(out EnemyHurt enemy))
            HitTarget();
    }
    protected virtual void HitTarget()
    {

    }
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        activeAbility = ability;
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 dir = PartyController.player.transform.position - mousePos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180f;
        transform.position = PlayerSkill.MuzzlePoint.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        AudioManager.instance.PlaySfx("Fireball");
        StartCoroutine(LifeCheckCourtine());
    }
    private IEnumerator LifeCheckCourtine()
    {
        var lifeTimeInterval = 0.2f;
        Vector3 startPos = transform.position;
        var endTime = Time.time + lifeTime;
        while (Time.time <= endTime)
        {
            var distance = Vector3.Distance(transform.position, startPos);
            if (distance > activeAbility.MaxUseRange)
                break;
            yield return new WaitForSeconds(lifeTimeInterval);
        }
        Destroy(this.gameObject);
    }
}
