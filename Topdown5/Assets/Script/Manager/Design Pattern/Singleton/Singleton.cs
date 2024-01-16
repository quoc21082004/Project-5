using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T instance;
    protected virtual void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this as T;
        }
        else
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
