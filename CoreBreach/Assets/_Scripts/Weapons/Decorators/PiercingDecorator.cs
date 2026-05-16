using UnityEngine;

// make bullet stronger, pass through enemies

public class PiercingDecorator : WeaponDecorator
{
    public PiercingDecorator(IWeapon weapon) : base(weapon) { }

    public override void Shoot(Vector3 origin,Vector3 direction)
    {
        wrappedWeapon.Shoot(origin,direction);

        wrappedWeapon.Shoot(origin +Vector3.up *0.05f, direction);
    }
}