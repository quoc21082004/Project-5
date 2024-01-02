using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilet : MonoBehaviour
{
    private Rigidbody2D myrigid;
    Enemy enemy;
    float realdamage;

    private void Awake()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        myrigid = GetComponent<Rigidbody2D>();
        realdamage = enemy.damage;
        Destroy(this.gameObject, 5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.CompareTag("Wall")) 
        {
            if (collision.gameObject.TryGetComponent<Player>(out Player player))
            {
                player.GetComponent<PlayerHurt>().TakeDamage(null,realdamage);
            }
            Destroy(this.gameObject);
        }
    }

}
