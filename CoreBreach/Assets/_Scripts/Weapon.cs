using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ObjectPool bulletPool; 
    public Transform firePoint; 
    public float bulletForce = 40f; 
    
    public Camera mainCamera; 

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet =bulletPool.GetObjectFromPool();

        if (bullet !=null)
        {
            // teleport to gun tip
            bullet.transform.position = firePoint.position;

            // find exact center of screen
            Ray ray =mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f,0)); 
            RaycastHit hit;
            Vector3 targetPoint;

            // if camera see a wall or enemy, target is there
            if (Physics.Raycast(ray,out hit))
            {
                targetPoint= hit.point;
            }
            else
            {
                // if we look sky, just go very far away
                targetPoint= ray.GetPoint(1000); 
            }

            // calculate direction from gun hole to crosshair target
            Vector3 direction = (targetPoint - firePoint.position).normalized;
            
            // make bullet look to crosshair
            bullet.transform.forward = direction;

            Rigidbody rb= bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero; 
                
                // push bullet to crosshair
                rb.AddForce(direction* bulletForce, ForceMode.Impulse); 
            }
        }
    }
}