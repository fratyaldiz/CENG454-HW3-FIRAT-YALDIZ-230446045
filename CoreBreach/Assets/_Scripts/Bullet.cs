using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 3f; // if no hit, go back after 3 sec
    [SerializeField] private int damage = 10;

    private ObjectPool myPool;
    private float timer;
    private bool isAlive; // small flag to stop double return to pool

    // weapon call this when shooting
    public void SetupBullet(ObjectPool pool, Vector3 startPos, Quaternion startRot)
    {
        myPool = pool;
        transform.position = startPos;
        transform.rotation = startRot;
    }

    public void OnTakenFromPool()
    {
        timer = 0f;
        isAlive = true;

        // reset rigidbody too, old speed should not stay on new bullet
        Rigidbody rb =GetComponent<Rigidbody>();
        if (rb!= null)
        {
            rb.linearVelocity= Vector3.zero;
            rb.angularVelocity =Vector3.zero;
        }
    }

    // we clean events here. no events for now but place is ready
    public void OnReturnToPool()
    {
        isAlive =false;
        // future: if bullet listen any event, unsubscribe here
    }

    private void Update()
    {
        if (!isAlive) return; // dont move if not alive

        // move bullet forward
        transform.Translate(Vector3.forward* speed*Time.deltaTime);

        // count time, if too much go to pool
        timer +=Time.deltaTime;
        if (timer>= lifeTime)
        {
            DieAndGoPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAlive) return; // already going back, ignore this hit

        // if we hit something with health, give damage
        IDamageable hitTarget =other.GetComponent<IDamageable>();
        if (hitTarget!= null)
        {
            hitTarget.TakeDamage(damage);
        }

        DieAndGoPool();
    }

    private void DieAndGoPool()
    {
        if (!isAlive) return; // stop double return
        isAlive= false;

        if (myPool!= null)
        {
            myPool.ReturnObjectToPool(this.gameObject);
        }
        else
        {
            // safety, if pool reference broken
            gameObject.SetActive(false);
        }
    }
}