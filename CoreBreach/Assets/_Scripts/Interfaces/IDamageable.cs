using UnityEngine;

// A strategy unit we will use to differentiate between enemy attack types.
public interface IAttackStrategy
{
    void ExecuteAttack(Transform target, int damage);
}