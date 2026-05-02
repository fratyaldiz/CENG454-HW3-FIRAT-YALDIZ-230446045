using UnityEngine;

public class Weapon : MonoBehaviour
{
    // we need pool reference to take bullet
    [SerializeField] private ObjectPool bulletPool; 
    [SerializeField] private Transform firePoint; // where bullet come out

    private void Update()
    {
        // fire when mouse left click
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPool == null) 
        {
            Debug.Log("we forget to put pool in inspector!");
            return;
        }

        // take bullet from pool script
        GameObject bulletObj= bulletPool.GetObjectFromPool();
        
        // get bullet script
        Bullet bulletScript =bulletObj.GetComponent<Bullet>();
        if (bulletScript !=null)
        {
            // send pool reference and position of bullet
            bulletScript.SetupBullet(bulletPool,firePoint.position,firePoint.rotation);
        }
    }
}