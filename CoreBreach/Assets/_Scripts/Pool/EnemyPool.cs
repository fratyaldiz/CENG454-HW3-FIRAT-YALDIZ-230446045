using System.Collections.Generic;
using UnityEngine;

// pool for enemies. similar to bullet pool but enemy need extra setup.
public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize =15;

    private Queue<GameObject>enemyQueue;

    private void Awake()
    {
        enemyQueue =new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy =Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);

            // tell enemy which pool he belong to
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.SetPool(this);
            }

            enemyQueue.Enqueue(enemy);
        }
    }

    public GameObject GetEnemy(Vector3 position)
    {
        GameObject enemy;

        if (enemyQueue.Count> 0)
        {
            enemy =enemyQueue.Dequeue();
        }
        else
        {
            // pool empty, make new one
            enemy = Instantiate(enemyPrefab, transform);
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health !=null)
            {
                health.SetPool(this);
            }
        }

        enemy.transform.position = position;
        enemy.SetActive(true);

        IPoolable[] poolables =enemy.GetComponents<IPoolable>();
        foreach (IPoolable p in poolables)
        {
            p.OnTakenFromPool();
        }

        return enemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        // call cleanup BEFORE deactivate, important!
        IPoolable[] poolables= enemy.GetComponents<IPoolable>();
        foreach (IPoolable p in poolables)
        {
            p.OnReturnToPool();
        }

        enemy.SetActive(false);
        enemyQueue.Enqueue(enemy);
    }
}