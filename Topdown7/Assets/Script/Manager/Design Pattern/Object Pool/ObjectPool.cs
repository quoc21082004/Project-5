using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    public GameObject Prefab => prefab; 
    public int Size => size;
    public int RuntimeSize => queue.Count; 
    [SerializeField] GameObject prefab;
    [SerializeField] int size = 1;  
    Queue<GameObject> queue;
    Transform parent;
    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();
        this.parent = parent;

        for (var i = 0; i < size; i++)
        {
            queue.Enqueue(Get());
        }
    }
    GameObject Get()
    {
        var copy = GameObject.Instantiate(prefab, parent);  
        copy.SetActive(false);
        return copy;
    }
    GameObject AvailableObject()
    {
        GameObject availableObject = null;
        availableObject = (queue.Count > 0 && !queue.Peek().activeSelf) ? queue.Dequeue() : Get();
        queue.Enqueue(availableObject); 
        return availableObject;
    }
    public GameObject PreparedObject()
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        return preparedObject;
    }
    public GameObject PreparedObject(Vector3 position)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        return preparedObject;
    }
    public GameObject PreparedObject(Vector3 position, Quaternion rotation)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        return preparedObject;
    }
    public GameObject PreparedObject(Vector3 position, Quaternion rotation, Transform parent)
    { 
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.parent = parent.transform;
        return preparedObject;
    }

    public void DeActivateAll()
    {
        foreach (var obj in queue)
        {
            obj.SetActive(false);
        }
    }

}
