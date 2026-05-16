using UnityEngine;

// reduce cooldown so weapon shoot faster
public class FireRateDecorator : WeaponDecorator
{
    private float multiplier =0.5f; // half the cooldown

    public FireRateDecorator(IWeapon weapon) : base(weapon) { }

    public override float GetCooldown()
    {
        return wrappedWeapon.GetCooldown()*multiplier;
    }
}