using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    [SerializeField] float minspeed = 0.3f;
    [SerializeField] float maxspeed = 0.6f;

    protected Player player;
    protected Enemy enemy;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }
    protected virtual void PickUp()
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }
    public IEnumerator MoveCourtine()
    {
        float speedmove = Random.Range(minspeed, maxspeed);
        Vector2 dir = Vector2.left;
        while(true)
        {
            if (player.isActiveAndEnabled)
            {
                dir = (player.transform.position - transform.position).normalized;
            }
            transform.Translate(dir * speedmove * Time.deltaTime);
            yield return null;
        }
    }
    
}
