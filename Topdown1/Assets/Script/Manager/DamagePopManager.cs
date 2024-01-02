using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopManager : Singleton<DamagePopManager>
{
    [SerializeField] GameObject textprefab;

    public void CreateDamagePop(bool isCrit, float amt, Vector2 trans, Transform parent)
    {
        GameObject clone = Instantiate(textprefab, trans, Quaternion.identity);
        if (isCrit)
        {
            clone.GetComponentInChildren<TextMeshPro>().text = "-" + amt.ToString("F0");
            clone.GetComponentInChildren<TextMeshPro>().color = Color.red;
        }
        else
        {
            clone.GetComponentInChildren<TextMeshPro>().text = "-" + amt.ToString("F0");
            clone.GetComponentInChildren<TextMeshPro>().color = Color.white;
        }
        clone.transform.parent = parent.transform;
    }
    public void CreateRecoverPop(ConsumableType type, float amt, Vector2 trans, Transform parent)
    {
        GameObject clone = Instantiate(textprefab, trans, Quaternion.identity);
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
}
