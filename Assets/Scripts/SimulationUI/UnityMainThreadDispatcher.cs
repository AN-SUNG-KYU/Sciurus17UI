using UnityEngine;
using System.Collections.Generic;
using System;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static UnityMainThreadDispatcher _instance = null;
    private readonly Queue<Action> executionQueue = new Queue<Action>();

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static UnityMainThreadDispatcher Instance()
    {
        if (_instance == null)
        {
            Debug.LogError("UnityMainThreadDispatcherがシーンに存在しません！Hierarchyに追加してください。");
        }
        return _instance;
    }

    public void Enqueue(Action action)
    {
        lock (executionQueue)
        {
            executionQueue.Enqueue(action);
        }
    }

    void Update()
    {
        lock (executionQueue)
        {
            while (executionQueue.Count > 0)
            {
                executionQueue.Dequeue()?.Invoke();
            }
        }
    }
}
