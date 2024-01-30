using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilet : MonoBehaviour
{
    private Rigidbody2D myrigid;
    public float realdamage;
    private void Awake()
    {
        myrigid = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall")) 
        {
            if (collision.gameObject.TryGetComponent<Player>(out Player player))
                player.GetComponent<PlayerHurt>().TakeDamage(null, realdamage);
            gameObject.SetActive(false);
        }
    }

}
