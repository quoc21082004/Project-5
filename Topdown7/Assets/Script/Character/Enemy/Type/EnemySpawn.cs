using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : Enemy
{
    const int MAX_ENEMY = 15;
    public List<GameObject> monsterList;
    GameObject clone;
    [SerializeField] GameObject spawnEffect;
    public float spawnDelay;
    public float spawnRange;
    int currentMonster = 0;
    [SerializeField] LayerMask layerMask;
    protected override void Awake()
    {
        base.Awake();
    }
    public void SpawnEnemy()
    {
        StartCoroutine(SpawnCoroutine());
    }
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            bool canSpawn = false;
            canSpawn = true;
            if (currentMonster > MAX_ENEMY)
                yield break;
            if (canSpawn)
            {
                StartCoroutine(SpawnMonster());
                canSpawn = false;
            }
        }
    }
    private IEnumerator SpawnMonster()
    {
        Vector2 position = (Vector2)transform.position + Random.insideUnitCircle * spawnRange;
        if (CanSpawnAtThisPoint(position))
        {
            currentMonster++;
            GameObject delayeffect = PoolManager.instance.Release(spawnEffect, position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
            delayeffect.gameObject.SetActive(false);
            clone = PoolManager.instance.Release(monsterList[Random.Range(0, monsterList.Count)], position, Quaternion.identity);
        }
        else
            yield break;
    }
    private bool CanSpawnAtThisPoint(Vector2 spawnPos)
    {
        bool checkSpawn = !Physics2D.OverlapCircle(spawnPos, 1f, layerMask);
        return checkSpawn;
    }
}
