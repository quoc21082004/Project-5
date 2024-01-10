using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyToSpawn;
    public GameObject enemyspawned = null;
    public float timespawn;
    public bool isSpawning = false;
    public LayerMask blockingLayer;
    public RaycastHit2D hitleftToRight, hittopToBot;
    Transform builetPool;
    private void Start()
    {
        SpawnAfterDelay(0f);
        builetPool = GameObject.Find("BuiletPool").GetComponent<Transform>();
    }
    private void Update()
    {
        if (enemyspawned == null && !isSpawning)
        {
            StartCoroutine(SpawnAfterDelay(timespawn));
        }
    }
    IEnumerator SpawnAfterDelay(float delay)
    {
        isSpawning = true;
        yield return new WaitForSeconds(delay);
        while (!CheckIfSpawnLocationClear())
        {
            yield return new WaitForSeconds(1f);
        }
        enemyspawned = Instantiate(enemyToSpawn[Random.Range(0, enemyToSpawn.Length)], transform.position, Quaternion.identity, builetPool);
        isSpawning = false;
    }

    private bool CheckIfSpawnLocationClear()
    {
        Vector3 leftEdge = transform.position + new Vector3(-0.5f, 0f);
        Vector3 rightEdge = transform.position + new Vector3(0f, 0.5f);
        Vector3 topEdge = transform.position + new Vector3(0, 0.5f);
        Vector3 botEdge = transform.position + new Vector3(0, -0.5f);

        // Create linecast from left edge to right edge and from top edge to btm edge of the intended spawn point like a "+".
        hitleftToRight = Physics2D.Linecast(leftEdge, rightEdge, blockingLayer);
        hittopToBot = Physics2D.Linecast(topEdge, botEdge, blockingLayer);
        Debug.Log("See player");
        return hitleftToRight.transform == null && hittopToBot.transform == null;
    }
}
