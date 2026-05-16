using UnityEngine;

// abstract base for all decorator
public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon wrappedWeapon;

    public WeaponDecorator(IWeapon weapon)
    {
        wrappedWeapon=weapon;
    }

    public virtual void Shoot(Vector3 origin, Vector3 direction)
    {
        wrappedWeapon.Shoot(origin,direction);
    }

    public virtual float GetCooldown()
    {
        return wrappedWeapon.GetCooldown();
    }
}