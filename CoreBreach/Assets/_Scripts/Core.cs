using System;
using UnityEngine;

// we use IDamageable interface
public class Core : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    
    // OBSERVER PATTERN:
    public event Action<int,int> OnHealthChanged; 
    public event Action OnCoreDestroyed;

    private void Start()
    {
        currentHealth =maxHealth;
    }

    // interface need this take damage function
    public void TakeDamage(int damageAmount)
    {
        if (currentHealth<= 0) return; // if health zero, do nothing so no minus health
        currentHealth -=damageAmount;
        
        // damage taken! send event to UI for change health bar
        OnHealthChanged?.Invoke(currentHealth,maxHealth);
        if (currentHealth<= 0)
        {
            Die();
        }
    }
    // interface need die function too
    public void Die()
    {
        OnCoreDestroyed?.Invoke();
        Debug.Log("CORE IS EXPLODE! GAME OVER");
    }
}