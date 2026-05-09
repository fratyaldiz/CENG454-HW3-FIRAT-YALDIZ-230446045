using UnityEngine;

// shoot strategy: robot shoot bullet to target
public class ShootStrategy : MonoBehaviour, IAttackStrategy
{
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletForce = 30f;

    public void Attack(Transform target)
    {
        if (bulletPool ==null || firePoint ==null) return;

        // take bullet from pool
        GameObject bullet =bulletPool.GetObjectFromPool();
        if (bullet== null) return;

        // aim bullet to target
        Vector3 direction =(target.position - firePoint.position).normalized;
        bullet.transform.position= firePoint.position;
        bullet.transform.forward= direction;

        // tell bullet which pool he belongs
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript!= null)
        {
            bulletScript.SetupBullet(bulletPool,firePoint.position, bullet.transform.rotation);
        }

        // give bullet some speed
        Rigidbody rb=bullet.GetComponent<Rigidbody>();
        if (rb !=null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(direction *bulletForce,ForceMode.Impulse);
        }
    }
}