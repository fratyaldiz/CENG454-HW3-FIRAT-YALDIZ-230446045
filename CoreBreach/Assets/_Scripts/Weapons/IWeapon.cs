using UnityEngine;

// base weapon and all decorators uses same interface
public interface IWeapon
{
    void Shoot(Vector3 origin, Vector3 direction);
    float GetCooldown();}