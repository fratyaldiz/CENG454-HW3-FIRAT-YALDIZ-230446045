using System.Collections;
using UnityEngine;

// spawn enemies in waves from pool.
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemiesPerWave =5;
    [SerializeField] private float timeBetweenSpawns = 1.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int totalWaves =3;

    private int currentWave= 0;

    private void Start()
    {
        if (enemyPool == null|| spawnPoints== null|| spawnPoints.Length== 0)
        {
            Debug.LogError("EnemySpawner setup is broken");
            return;
        }

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        // wait a bit so player can prepare
        yield return new WaitForSeconds(2f);

        while (currentWave < totalWaves)
        {
            currentWave++;
            Debug.Log("Wave " +currentWave + "start!");

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnOneEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            GameEvents.RaiseWaveCompleted(currentWave);
            Debug.Log("Wave " + currentWave +" done!");

            yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("All waves done, player win!");
    }

    private void SpawnOneEnemy()
    {
        Transform spawnPoint =spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy= enemyPool.GetEnemy(spawnPoint.position);

        if (enemy== null)
        {
            Debug.LogWarning("Could not get enemy from pool");
        }
    }
}