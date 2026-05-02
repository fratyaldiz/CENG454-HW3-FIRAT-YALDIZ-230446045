using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 3f; // if don't hit, delete after 3 sec
    
    private ObjectPool myPool;
    private float timer;

    // weapon will call this when shooting
    public void SetupBullet(ObjectPool pool,Vector3 startPos,Quaternion startRot)
    {
        myPool =pool;
        transform.position = startPos;
        transform.rotation= startRot;
        
        timer = 0f; 
    }

    private void Update()
    {
        // move bullet forward in world
        transform.Translate(Vector3.forward *speed *Time.deltaTime);

        // timer for kill bullet if miss
        timer+= Time.deltaTime;
        if (timer >=lifeTime)
        {
            DieAndGoPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if we hit something, maybe it is enemy. 
        IDamageable hitTarget = other.GetComponent<IDamageable>();
        if (hitTarget!= null)
        {
            hitTarget.TakeDamage(10);   //give 10 damage
        }

        // hit wall or enemy, so go back to pool
        DieAndGoPool();
    }

    private void DieAndGoPool()
    {
        if (myPool != null)
        {
            myPool.ReturnObjectToPool(this.gameObject);
        }
        else
        {
            gameObject.SetActive(false); // second plan if pool missing
        }
    }
}