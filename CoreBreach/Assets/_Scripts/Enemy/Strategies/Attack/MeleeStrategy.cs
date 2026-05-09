using UnityEngine;

// melee strategy: enemy hit target with hand when he is very close.
public class MeleeStrategy : MonoBehaviour, IAttackStrategy
{
    [SerializeField] private int damage =15;
    [SerializeField] private float meleeRange= 2.5f;

    public void Attack(Transform target)
    {
        if (target ==null) return;

        // check distance, melee only work when very close
        float distance = Vector3.Distance(transform.position,target.position);
        if (distance > meleeRange) return;

        // try get IDamageable from target and hit
        IDamageable victim = target.GetComponent<IDamageable>();
        if (victim !=null)
        {
            victim.TakeDamage(damage);
        }
    }
}