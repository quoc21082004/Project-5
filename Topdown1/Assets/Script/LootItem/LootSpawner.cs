using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [Header("Exp Loots")]
    [SerializeField] LootOnWorld[] lootOnWorld;
    [Header("Item Loots")]
    [SerializeField] LootOnWorld[] itemLoots;
    [Header("Coin Loots")]
    [SerializeField] LootOnWorld[] coinLoots;
    [SerializeField] float genrange;
    public void SpawnLoot(Vector2 position,Transform pool)
    {
        foreach (var loot in lootOnWorld)
        {
            loot.LootSpawn(position + Random.insideUnitCircle * genrange, pool);
        }
        foreach (var coinsloots in coinLoots)
        {
            coinsloots.LootSpawn(position + Random.insideUnitCircle * genrange, pool);
        }
        for (int i = 0; i < itemLoots.Length; i++)
        {
            itemLoots[i].LootSpawn(position + Random.insideUnitCircle * genrange, pool);
        }
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
    }
}
