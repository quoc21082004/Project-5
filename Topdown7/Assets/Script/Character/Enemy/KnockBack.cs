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
    public void GetKnockBack(Transform damageSouce, float thurstKnock, Transform parent)
    {
        Vector2 dir = (transform.position - damageSouce.position).normalized * thurstKnock * myrigid.mass;
        myrigid.AddForce(dir, ForceMode2D.Impulse);
        GameObject ps = PoolManager.instance.Release(AssetManager.instance.assetData.knockbackEffect.gameObject, parent.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        //ps.transform.parent = parent.transform;
        StartCoroutine(CourtineKnock());
    }
    IEnumerator CourtineKnock()
    {
        yield return new WaitForSeconds(knockTime);
        myrigid.velocity = Vector2.zero;
    }
}
