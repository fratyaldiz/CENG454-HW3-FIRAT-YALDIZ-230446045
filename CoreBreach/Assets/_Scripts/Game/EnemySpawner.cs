using System.Collections;
using UnityEngine;

// spawn enemies in waves from pool
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemiesPerWave= 3;
    [SerializeField] private float timeBetweenSpawns =1.5f;
    [SerializeField] private float timeBetweenWaves= 5f;
    [SerializeField] private int totalWaves =3;

    private int currentWave = 0;

    private void Start()
    {
        if (enemyPool == null || spawnPoints == null || spawnPoints.Length ==0)
        {
            Debug.LogError("EnemySpawner setup is broken -missing pool or spawn points");
            return;
        }

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(2f);

        while (currentWave < totalWaves)
        {
            currentWave++;
            Debug.Log("=== Wave "+ currentWave +" start! ===");

            // spawn enemies for this wave
            for (int i =0; i < enemiesPerWave; i++)
            {
                SpawnOneEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            Debug.Log("Wave "+ currentWave +" enemies spawned, waiting for wave completion...");
            
            yield return new WaitForSeconds(timeBetweenWaves);

            // wave is complete
            GameEvents.RaiseWaveCompleted(currentWave);
            Debug.Log("Wave " + currentWave+ " completed!");
        }

        Debug.Log("=== All waves done, declaring victory! ===");

        // all waves finished, player won
        if (GameStateManager.Instance!= null)
        {
            GameStateManager.Instance.DeclareVictory();
        }
    }

    private void SpawnOneEnemy()
    {
        if (spawnPoints.Length== 0)
        {
            Debug.LogWarning("No spawn points available");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy =enemyPool.GetEnemy(spawnPoint.position);

        if (enemy ==null)
        {
            Debug.LogWarning("Could not get enemy from pool");
        }
    }
}