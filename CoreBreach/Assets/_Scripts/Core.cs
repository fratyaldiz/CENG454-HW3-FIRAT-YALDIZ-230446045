using System;
using UnityEngine;

// the energy core. enemies want to destroy it.
public class Core : MonoBehaviour, IDamageable
{
    public static Core Instance { get;private set; }

    [SerializeField] private int maxHealth =100;
    [SerializeField] private int damagePerEnemy= 20;

    private int currentHealth;

    public bool IsAlive=> currentHealth > 0;

    // OBSERVER PATTERN
    public event Action<int,int> OnHealthChanged;
    public event Action OnCoreDestroyed;

    private void Awake()
    {
        if (Instance !=null && Instance != this)
        {
            Debug.LogWarning("Two Core in scene?Destroying second one.");
            Destroy(gameObject);

            return;
        }
        Instance = this;
    }

    private void Start()
    {
        currentHealth =maxHealth;
        OnHealthChanged?.Invoke(currentHealth,maxHealth);
    }

    private void OnDestroy()
    {
        if (Instance ==this)
        {
            Instance=null;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0) return;

        currentHealth -=damageAmount;

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnCoreDestroyed?.Invoke();
        Debug.Log("CORE EXPLODE! GAME OVER");
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy =other.GetComponent<EnemyHealth>();
        if (enemy!= null)
        {
            TakeDamage(damagePerEnemy);

            enemy.OnReachedCore();
        }
    }
}