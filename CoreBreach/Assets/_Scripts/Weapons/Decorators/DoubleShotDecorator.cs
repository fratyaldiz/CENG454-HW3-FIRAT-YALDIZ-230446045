using UnityEngine;

// shoot 2 bullets side by side
public class DoubleShotDecorator : WeaponDecorator
{
    private float spread=0.3f;

    public DoubleShotDecorator(IWeapon weapon) : base(weapon) { }

    public override void Shoot(Vector3 origin,Vector3 direction)
    {
        // one bullet to left, one to right
        Vector3 right = Vector3.Cross(Vector3.up,direction).normalized;

        wrappedWeapon.Shoot(origin + right * spread,direction);
        wrappedWeapon.Shoot(origin - right * spread,direction);
    }
}