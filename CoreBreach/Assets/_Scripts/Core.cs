using System;
using UnityEngine;

// the energy core. enemies want to destroy it.
public class Core : MonoBehaviour, IDamageable
{
    // enemies and other things use Core
    public static Core Instance { get; private set; }

    [SerializeField] private int maxHealth =100;
    private int currentHealth;

    // OBSERVER PATTERN
    // when core take damage or die, we send event to subscribers.
    public event Action<int, int>OnHealthChanged;
    public event Action OnCoreDestroyed;

    private void Awake()
    {
        // setup instance. if there is already a core, that is wrong.
        if (Instance!= null && Instance !=this)
        {
            Debug.LogWarning("Two Core in scene? destroying second one.");
            Destroy(gameObject);
            return;
        }
        Instance =this;
    }

    private void Start()
    {
        currentHealth =maxHealth;
        // tell ui my starting health right away
        OnHealthChanged?.Invoke(currentHealth,maxHealth);
    }

    private void OnDestroy()
    {
        // clean instance when core gone, so no broken reference left
        if (Instance ==this)
        {
            Instance= null;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <=0) return; // already dead, no more damage

        currentHealth-= damageAmount;

        // tell everyone health changed (ui bar, screen shake, etc)
        OnHealthChanged?.Invoke(currentHealth,maxHealth);

        if (currentHealth<= 0)
        {
            Die();
        }
    }

    // private now, dying is internal decision
    private void Die()
    {
        OnCoreDestroyed?.Invoke();
        Debug.Log("CORE EXPLODE!GAME OVER");
    }
}