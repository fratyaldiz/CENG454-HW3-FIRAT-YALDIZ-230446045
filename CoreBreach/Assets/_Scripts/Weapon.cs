using UnityEngine;

// player weapon. it use IWeapon system so decorators can change behavior.
public class Weapon : MonoBehaviour
{
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float bulletForce =40f;
    [SerializeField] private float baseCooldown=0.4f;

    private IWeapon currentWeapon;
    private float nextShootTime;

    private void Start()
    {
        // start with base weapon, no decorators yet
        currentWeapon = new BaseWeapon(bulletPool, bulletForce, baseCooldown);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextShootTime)
        {
            ShootCurrentWeapon();
            nextShootTime = Time.time+currentWeapon.GetCooldown();
        }
    }

    private void ShootCurrentWeapon()
    {
        // find where camera look at
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray,out hit))
        {
            targetPoint =hit.point;
        }
        else
        {
            targetPoint= ray.GetPoint(1000);
        }

        Vector3 direction= (targetPoint -firePoint.position).normalized;

        currentWeapon.Shoot(firePoint.position, direction);
    }

    // called by pickup, wraps current weapon with new decorator
    public void ApplyDecorator(WeaponPickup.DecoratorType type)
    {
        switch (type)
        {
            case WeaponPickup.DecoratorType.DoubleShot:
                currentWeapon = new DoubleShotDecorator(currentWeapon);
                Debug.Log("Picked up DoubleShot!");
                break;
            case WeaponPickup.DecoratorType.Piercing:
                currentWeapon =new PiercingDecorator(currentWeapon);
                Debug.Log("Picked up Piercing!");
                break;
            case WeaponPickup.DecoratorType.FireRate:
                currentWeapon= new FireRateDecorator(currentWeapon);
                Debug.Log("Picked up FireRate!");
                break;
        }
    }
}