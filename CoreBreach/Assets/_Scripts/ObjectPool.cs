using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefabToPool;
    [SerializeField] private int poolSize = 20;

    private Queue<GameObject>poolQueue;

    private void Awake()
    {
        poolQueue = new Queue<GameObject>();

        // we make all bullets at start so no lag in middle of game
        for (int i = 0;i < poolSize; i++)
        {
            GameObject obj =Instantiate(prefabToPool, transform);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    // when gun shoot, we call this
    public GameObject GetObjectFromPool()
    {
        GameObject obj;

        if (poolQueue.Count> 0)
        {
            obj= poolQueue.Dequeue();
        }
        else
        {
            // pool is empty, we make new one (just in case)
            obj =Instantiate(prefabToPool, transform);
        }

        obj.SetActive(true);

        // if object have IPoolable, tell him "you are alive again"
        // so he can do his fresh setup
        IPoolable[] poolables =obj.GetComponents<IPoolable>();
        foreach (IPoolable p in poolables)
        {
            p.OnTakenFromPool();
        }

        return obj;
    }

    // bullet hit something, we put back here
    public void ReturnObjectToPool(GameObject obj)
    {
        // if we deactive first, sometimes he can not clean himself
        IPoolable[] poolables=obj.GetComponents<IPoolable>();
        foreach (IPoolable p in poolables)
        {
            p.OnReturnToPool();
        }

        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }
}