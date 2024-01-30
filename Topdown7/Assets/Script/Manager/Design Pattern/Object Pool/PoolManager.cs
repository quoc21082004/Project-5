using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] ObjectPool[] enemyPools;
    [SerializeField] ObjectPool[] builetPools;
    [SerializeField] ObjectPool[] vFXPools;
    [SerializeField] ObjectPool[] spellPools;
    [SerializeField] ObjectPool[] textPools;
    [SerializeField] ObjectPool[] lootsPools;
    static Dictionary<GameObject, ObjectPool> prefab2Pool;  
    public void DeActivateAllLoots()
    {
        foreach (var pool in lootsPools)
        {
            pool.DeActivateAll();
        }
    }
    protected override void Awake()
    {
        base.Awake();
        prefab2Pool = new Dictionary<GameObject, ObjectPool>();
        Initialize(enemyPools);
        Initialize(builetPools);
        Initialize(textPools);
        Initialize(vFXPools);
        Initialize(lootsPools);
        Initialize(spellPools);
    }
    private void OnDestroy()
    {
        checkPoolSize(builetPools);
        checkPoolSize(enemyPools);
        checkPoolSize(textPools);
        checkPoolSize(vFXPools);
        checkPoolSize(lootsPools);
        checkPoolSize(spellPools);
    }
    void checkPoolSize(ObjectPool[] pools)
    {
        foreach (var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(
                    $"Pool: {pool.Prefab.name} has a runtime size {pool.RuntimeSize} bigger " +
                    $"than its initial size {pool.Size}!"
                );
            }
        }
    }

    void Initialize(ObjectPool[] pools)
    {
        foreach (var pool in pools)
        {
            if (prefab2Pool.ContainsKey(pool.Prefab))
            {
                Debug.LogError("Same prefab in multiple Pools! Prefab: " + pool.Prefab.name);
                continue; 
            }
            prefab2Pool.Add(pool.Prefab, pool);
            Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }
    public  GameObject Release(GameObject prefab)
    {
        if (!prefab2Pool.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);
            return null;
        }
        return prefab2Pool[prefab].PreparedObject();
    }

    public  GameObject Release(GameObject prefab, Vector3 position)
    {
        if (!prefab2Pool.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);
            return null;
        }
        return prefab2Pool[prefab].PreparedObject(position);
    }

    public  GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!prefab2Pool.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);
            return null;
        }
        return prefab2Pool[prefab].PreparedObject(position, rotation);
    }
    public  GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!prefab2Pool.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);
            return null;
        }
        return prefab2Pool[prefab].PreparedObject(position, rotation, parent);
    }

}
