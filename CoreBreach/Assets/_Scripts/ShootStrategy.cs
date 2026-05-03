using UnityEngine;

// this is shoot strategy for robot.
public class ShootStrategy : MonoBehaviour, IAttackStrategy
{
    public void Attack(Transform target)
    {
        // we will use bullet pool here later
        Debug.Log("PEW PEW! ROBOT SHOOT TARGET: " +target.name);
    }
}