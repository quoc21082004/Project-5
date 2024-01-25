using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExplosionBuilet : MonoBehaviour, ISpell
{
    public float lifeTime;
    protected ActiveAbility activeAbility;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHurt>(out EnemyHurt enemy) || collision.gameObject.CompareTag("Wall")) 
            HitTarget();
    }
    protected virtual void HitTarget()
    {
        gameObject.SetActive(false);
    }
    public void KickOff(ActiveAbility ability, Vector2 direction)
    {
        activeAbility = ability;
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 dir = PartyController.player.transform.position - mousePos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180f;
        transform.position = PlayerCombat.MuzzlePoint.transform.position;
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
    }
}
