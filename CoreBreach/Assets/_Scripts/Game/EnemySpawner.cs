using System.Collections;
using UnityEngine;

// spawn enemies in waves from pool.
// wave only completes when all spawned enemies are gone (killed or reached core).
// uses GameEvents.OnEnemyDied as Observer signal to track active enemy count.
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemiesPerWave =3;
    [SerializeField] private float timeBetweenSpawns =1.5f;
    [SerializeField] private float timeBetweenWaves =5f;
    [SerializeField] private int totalWaves=3;
    [SerializeField] private float waveTimeoutSeconds =60f;

    private int currentWave =0;
    private int activeEnemies =0;

    private void OnEnable()
    {
        GameEvents.OnEnemyDied+= HandleEnemyDied;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDied-= HandleEnemyDied;
    }

    private void HandleEnemyDied(Vector3 position)
    {
        activeEnemies--;
        if (activeEnemies < 0) activeEnemies =0;
    }

    private void Start()
    {
        if (enemyPool == null || spawnPoints == null || spawnPoints.Length ==0)
        {
            Debug.LogError("EnemySpawner setup is broken - missing pool or spawn points");
            return;
        }

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(2f);

        while (currentWave< totalWaves)
        {
            if (GameStateManager.Instance != null &&
                GameStateManager.Instance.CurrentState !=GameStateManager.GameState.Playing)
                yield break;

            currentWave++;
            activeEnemies = 0;
            Debug.Log("=== Wave " + currentWave + " start! ===");

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnOneEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            Debug.Log("Wave " +currentWave +" enemies spawned,waiting for wave completion.");

            float elapsed =0f;
            while (activeEnemies > 0 && elapsed < waveTimeoutSeconds)
            {
                if (GameStateManager.Instance !=null &&
                    GameStateManager.Instance.CurrentState !=GameStateManager.GameState.Playing)
                    yield break;

                elapsed+= Time.deltaTime;
                yield return null;
            }

            GameEvents.RaiseWaveCompleted(currentWave);
            Debug.Log("Wave " +currentWave +" completed!");

            if (currentWave <totalWaves)
                yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("=== All waves done, declaring victory! ===");

        if (GameStateManager.Instance!= null)
        {
            GameStateManager.Instance.DeclareVictory();
        }
    }

    private void SpawnOneEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points available");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy =enemyPool.GetEnemy(spawnPoint.position);
        if (enemy !=null)
            activeEnemies++;
        else
            Debug.LogWarning("Could not get enemy from pool");
    }
}