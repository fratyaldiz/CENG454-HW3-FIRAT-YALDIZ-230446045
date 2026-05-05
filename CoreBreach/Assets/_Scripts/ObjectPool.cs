using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // NEW: we make this static so weapon can find pool easy
    public static ObjectPool instance;

    // we put this script to empty game object in unity
    [SerializeField] private GameObject prefabToPool;
    [SerializeField] private int poolSize = 20; 

    // we use queue
    private Queue<GameObject> poolQueue;
    
    private void Awake()
    {
        // setup instance
        instance =this;

        poolQueue= new Queue<GameObject>();

        // we make objects before game start for no lag
        for (int i= 0; i< poolSize;i++)
        {
            GameObject obj =Instantiate(prefabToPool,transform);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    // when gun shoot, we call this to take bullet
    public GameObject GetObjectFromPool()
    {
        if (poolQueue.Count >0)
        {
            GameObject obj= poolQueue.Dequeue(); 
            obj.SetActive(true); 
            return obj;
        }
        else
        {
            // if pool is empty
            GameObject obj= Instantiate(prefabToPool,transform);
            obj.SetActive(true);
            return obj;
        }
    }
    
    // when bullet hit to wall, we call this to put back
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false); //hide bullet
        poolQueue.Enqueue(obj); //put back in line
    }
}