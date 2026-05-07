using UnityEngine;

// any object that goes inside object pool should reset itself.

public interface IPoolable
{
    // called when object goes back to pool. 
    void OnReturnToPool();
    
    // called when object is taken from pool.
    void OnTakenFromPool();
}