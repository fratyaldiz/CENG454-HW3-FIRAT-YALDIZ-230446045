using UnityEngine;

// enemy hp when hp is zero, it go back to pool.
public class EnemyHealth : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int scoreValue = 10;

    private int currentHealth;
    private EnemyPool myPool;
    private bool isAlive;

    public void SetPool(EnemyPool pool)
    {
        myPool =pool;
    }

    // reset hp when enemy come from pool
    public void OnTakenFromPool()
    {
        currentHealth =maxHealth;
        isAlive= true;
    }

    // clean up when enemy go back to pool
    public void OnReturnToPool()
    {
        isAlive= false;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isAlive) return;

        currentHealth -=damageAmount;

        if (currentHealth<= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isAlive) return;
        isAlive = false;

        GameEvents.RaiseEnemyDied(transform.position);
        GameEvents.RaiseScoreChanged(scoreValue);

        if (myPool!= null)
        {
            myPool.ReturnEnemy(this.gameObject);
        }
        else
        {
            
            Destroy(gameObject);
        }
    }
}