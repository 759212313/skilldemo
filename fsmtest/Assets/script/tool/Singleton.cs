using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;

public abstract class Singleton<T> where T : new()
{
    private static T _instance;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                object lockObject = Singleton<T>._lock;
                Monitor.Enter(lockObject);
                try
                {
                    if (_instance == null)
                        _instance = Activator.CreateInstance<T>();
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
            }
            return _instance;
        }
    }

    public virtual void Init()
    {

    }
}

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance = null;
    private static object lockObject = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                object obj = MonoSingleton<T>.lockObject;
                Monitor.Enter(obj);
                instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>();
                }
                Monitor.Exit(obj);
            }
            return instance;
        }
    }

    public virtual void SetDontDestroyOnLoad(Transform parent)
    {
        transform.parent = parent;
    }
}
