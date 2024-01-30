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
    public void SpawnLoot(TypeEnemy type ,Vector2 position)
    {
        foreach (var loot in lootOnWorld)
            loot.LootSpawn(position + Random.insideUnitCircle * genrange);
        foreach (var coinsloots in coinLoots)
            coinsloots.LootSpawn(position + Random.insideUnitCircle * genrange);
        for (int i = 0; i < itemLoots.Length; i++)
            itemLoots[i].LootSpawn(position + Random.insideUnitCircle * genrange);
        switch (type)
        {
            case TypeEnemy.LittleRange:
                mobLoots[0].LootSpawn(position + Random.insideUnitCircle * genrange);
                break;
            case TypeEnemy.Boar:
                mobLoots[1].LootSpawn(position + Random.insideUnitCircle * genrange);
                break;
            case TypeEnemy.Bat:
                mobLoots[2].LootSpawn(position + Random.insideUnitCircle * genrange);
                break;
            case TypeEnemy.FlyingMelee:
                mobLoots[4].LootSpawn(position + Random.insideUnitCircle * genrange);
                break;
            case TypeEnemy.Skeleton:
                mobLoots[5].LootSpawn(position + Random.insideUnitCircle * genrange);
                break;
            case TypeEnemy.Scopoion:
                mobLoots[6].LootSpawn(position + Random.insideUnitCircle * genrange);
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
