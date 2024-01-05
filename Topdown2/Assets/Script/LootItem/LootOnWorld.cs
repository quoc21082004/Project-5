using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootOnWorld", menuName = "Loots/LootWorld")]
public class LootOnWorld : ScriptableObject
{
    [SerializeField] GameObject lootprefab;
    [SerializeField] [Range(0, 100)] public float dropRate;
    GameObject loot;
    float dropForce = 10f;
    public void LootSpawn(Vector2 position,Transform pool)
    {
        float random = Random.Range(0, 101);
        if (random <= dropRate)
        {
            loot = Instantiate(lootprefab, position, Quaternion.identity, pool.transform);
            loot.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * dropForce, ForceMode2D.Impulse);
        }     
    }
}
