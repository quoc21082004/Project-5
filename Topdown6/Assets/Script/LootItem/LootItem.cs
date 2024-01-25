using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    [SerializeField] float minspeed = 0.3f;
    [SerializeField] float maxspeed = 0.6f;
    protected Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            PickUp();
    }
    protected virtual void PickUp()
    {
        StopAllCoroutines();
        AudioManager.instance.PlaySfx("PickUp");
        gameObject.SetActive(false);
    }
    public IEnumerator MoveCourtine()
    {
        float speedmove = Random.Range(minspeed, maxspeed);
        Vector2 dir = Vector2.left;
        while(true)
        {
            if (PartyController.player.isActiveAndEnabled)
                dir = (PartyController.player.transform.position - transform.position).normalized;
            transform.Translate(dir * speedmove * Time.deltaTime);
            yield return null;
        }
    }
}
