using UnityEngine;

// diferent enemy types move different ways. 
public interface IMovementStrategy
{
    // called every frame by enemy to update its movement
    void Move(Transform self,Transform target);
    
    // return true if movement should pause at this distance
    bool ShouldStopAtRange(float distance);
}