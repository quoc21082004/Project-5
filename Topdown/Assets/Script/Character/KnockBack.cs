using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D myrigid;
    [SerializeField] float knockTime;
    private void Awake()
    {
        myrigid = GetComponent<Rigidbody2D>();
    }
    public void GetKnockBack(Transform damageSouce, float thurstKnock)
    {
        Vector2 dir = (transform.position - damageSouce.position).normalized * thurstKnock * myrigid.mass;
        myrigid.AddForce(dir, ForceMode2D.Impulse);
        ParticleSystem ps = Instantiate(AssetManager.instance.assetData.knockbackEffect, damageSouce.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        ps.transform.parent = damageSouce.transform;
        ps.Play();
        StartCoroutine(CourtineKnock());
    }
    IEnumerator CourtineKnock()
    {
        yield return new WaitForSeconds(knockTime);
        myrigid.velocity = Vector2.zero;
    }
}
