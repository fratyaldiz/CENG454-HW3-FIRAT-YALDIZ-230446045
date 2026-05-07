using UnityEngine;

// any object that can take damage uses this
public interface IDamageable
{
    void TakeDamage(int damageAmount);
}