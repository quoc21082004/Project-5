using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopManager : Singleton<DamagePopManager>
{
    [SerializeField] GameObject textprefab;
    public void CreateDamagePop(bool isCrit, float amt, Vector3 position, Transform parent)
    {
        GameObject clone = PoolManager.instance.Release(textprefab, position, Quaternion.identity);
        if (isCrit)
        {
            clone.GetComponentInChildren<TextMeshPro>().text = "-" + amt.ToString("F0");
            clone.GetComponentInChildren<TextMeshPro>().color = Color.red;
            //clone.transform.parent = parent.transform;
        }
        else
        {
            clone.GetComponentInChildren<TextMeshPro>().text = "-" + amt.ToString("F0");
            clone.GetComponentInChildren<TextMeshPro>().color = Color.white;
            //clone.transform.parent = parent.transform;
        }
    }
    public void CreateRecoverPop(ConsumableType type, float amt, Vector2 trans, Transform parent)
    {
        GameObject clone = PoolManager.instance.Release(textprefab, trans, Quaternion.identity);
        switch (type)
        {
            case ConsumableType.HealthPotion:
                clone.GetComponentInChildren<TextMeshPro>().text = "+" + amt.ToString("F0");
                clone.GetComponentInChildren<TextMeshPro>().color = Color.green;
                clone.transform.parent = parent.transform;
                break;
            case ConsumableType.ManaPotion:
                clone.GetComponentInChildren<TextMeshPro>().text = "+" + amt.ToString("F0");
                clone.GetComponentInChildren<TextMeshPro>().color = Color.blue;
                clone.transform.parent = parent.transform;
                break;
            default:
                break;
        }
    }
    public void BlockPop(Vector2 trans, Quaternion angle,Transform parent)
    {
        GameObject clone = PoolManager.instance.Release(textprefab, trans, angle, parent);
        if (clone != null)
        {
            clone.GetComponentInChildren<TextMeshPro>().text = "" + "Block!";
            clone.GetComponentInChildren<TextMeshPro>().color = Color.red;
            clone.transform.parent = parent.transform;
        }
    }
}
