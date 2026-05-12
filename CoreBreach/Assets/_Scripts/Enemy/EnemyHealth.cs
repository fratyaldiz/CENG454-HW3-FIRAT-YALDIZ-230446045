using UnityEngine;

// enemy hp. when hp is zero or it reach core, it go back to pool.
// every spawn, animator switch between run and fast run.
public class EnemyHealth : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int scoreValue = 10;

    private int currentHealth;
    private EnemyPool myPool;
    private bool isAlive;

    private Animator anim;
    
    // static counter, every new spawn use next animation
    private static int spawnCounter = 0;

    public void SetPool(EnemyPool pool)
    {
        myPool = pool;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // called when enemy come from pool
    public void OnTakenFromPool()
    {
        currentHealth = maxHealth;
        isAlive = true;

        // every other spawn use fast run for variety
        if (anim != null)
        {
            bool useFastRun = (spawnCounter % 2 == 1);
            anim.SetBool("UseFastRun", useFastRun);
            anim.ResetTrigger("Die"); // reset die trigger from old life
        }

        spawnCounter++;
    }

    // called when enemy go back to pool
    public void OnReturnToPool()
    {
        isAlive = false;
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
        isAlive =false;

        // play death animation
        if (anim!= null)
        {
            anim.SetTrigger("Die");
        }

        GameEvents.RaiseEnemyDied(transform.position);
        GameEvents.RaiseScoreChanged(scoreValue);

        // wait a bit so death anim can play, then return to pool
        Invoke(nameof(GoBackToPool), 1.5f);
    }

    // called when enemy touch the core
    public void OnReachedCore()
    {
        if (!isAlive) return;
        isAlive =false;

        GameEvents.RaiseEnemyDied(transform.position);
        // no score, player did not kill him

        // no death anim here, just disappear
        GoBackToPool();
    }

    private void GoBackToPool()
    {
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