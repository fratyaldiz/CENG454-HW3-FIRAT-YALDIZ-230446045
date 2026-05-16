using UnityEngine;

// basic weapon, shoot one bullet to direction
public class BaseWeapon : IWeapon
{
    private ObjectPool bulletPool;
    private float bulletForce;
    private float cooldown;

    public BaseWeapon(ObjectPool pool,float force,float cd)
    {
        bulletPool =pool;
        bulletForce= force;
        cooldown= cd;
    }

    public virtual void Shoot(Vector3 origin, Vector3 direction)
    {
        GameObject bullet =bulletPool.GetObjectFromPool();
        if (bullet ==null) return;

        bullet.transform.position = origin;
        bullet.transform.forward= direction;

        Bullet bScript =bullet.GetComponent<Bullet>();
        if (bScript!= null)
        {
            bScript.SetupBullet(bulletPool, origin,bullet.transform.rotation);
        }

        Rigidbody rb= bullet.GetComponent<Rigidbody>();
        if (rb!= null)
        {
            rb.linearVelocity= Vector3.zero;
            rb.AddForce(direction* bulletForce, ForceMode.Impulse);
        }
    }

    public virtual float GetCooldown()
    {
        return cooldown;
    }
}