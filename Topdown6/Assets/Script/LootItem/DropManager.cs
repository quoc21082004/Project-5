using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : Singleton<DropManager>
{
    [Header("                       Exp Loots")]
    [SerializeField] LootOnWorld[] lootOnWorld;
    [Header("                       Item Loots")]
    [SerializeField] LootOnWorld[] itemLoots;
    [Header("                       Mob Loots")]
    [SerializeField] LootOnWorld[] mobLoots;
    [Header("                       Coin Loots")]
    [SerializeField] LootOnWorld[] coinLoots;
    [SerializeField] float genrange;
    public void SpawnLoot(TypeEnemy type ,Vector2 position,Transform pool)
    {
        foreach (var loot in lootOnWorld)
            loot.LootSpawn(position + Random.insideUnitCircle * genrange, pool);
        foreach (var coinsloots in coinLoots)
            coinsloots.LootSpawn(position + Random.insideUnitCircle * genrange, pool);
        for (int i = 0; i < itemLoots.Length; i++)
            itemLoots[i].LootSpawn(position + Random.insideUnitCircle * genrange, pool);
        switch (type)
        {
            case TypeEnemy.LittleRange:
                mobLoots[0].LootSpawn(position + Random.insideUnitCircle * genrange, pool);
                break;
            case TypeEnemy.Boar:
                mobLoots[1].LootSpawn(position + Random.insideUnitCircle * genrange, pool);
                break;
            case TypeEnemy.Bat:
                mobLoots[2].LootSpawn(position + Random.insideUnitCircle * genrange, pool);
                break;
            case TypeEnemy.FireTotem:
                mobLoots[3].LootSpawn(position + Random.insideUnitCircle * genrange, pool);
                break;
            case TypeEnemy.FlyingMelee:
                mobLoots[4].LootSpawn(position + Random.insideUnitCircle * genrange, pool);
                break;
            case TypeEnemy.Skeleton:
                mobLoots[5].LootSpawn(position + Random.insideUnitCircle * genrange, pool);
                break;
            case TypeEnemy.Scopoion:
                mobLoots[6].LootSpawn(position + Random.insideUnitCircle * genrange, pool);
                break;
            default:
                break;
        }
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
    }
}
