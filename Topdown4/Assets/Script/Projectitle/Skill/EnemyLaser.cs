using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] float damage;
    bool isShooting = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!isShooting)
            {
                StartCoroutine(CoolDownHit(collision));
            }
        }
    }
    IEnumerator CoolDownHit(Collider2D collision)
    {
        isShooting = true;
        collision.gameObject.GetComponent<PlayerHurt>().TakeDamage(null,damage);
        yield return new WaitForSeconds(0.1f);
        isShooting = false;
    }
}
