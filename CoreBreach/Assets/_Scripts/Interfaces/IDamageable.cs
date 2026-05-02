using UnityEngine;

// if object has health, it must use
public interface IDamageable
{
    void TakeDamage(int damageAmount );
    void Die();
}