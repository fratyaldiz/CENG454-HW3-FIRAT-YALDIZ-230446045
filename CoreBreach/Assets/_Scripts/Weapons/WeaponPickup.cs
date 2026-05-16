using UnityEngine;

// pickup object in scene. when player touch, apply decorator to weapon.
public class WeaponPickup : MonoBehaviour
{
    public enum DecoratorType
    {
        DoubleShot,
        Piercing,
        FireRate
    }

    [SerializeField] private DecoratorType type;
    [SerializeField] private float rotateSpeed =90f;

    private void Update()
    {
        transform.Rotate(0,rotateSpeed *Time.deltaTime,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // try to find Weapon in this object, parent, or children
        Weapon weapon =other.GetComponent<Weapon>();
        if (weapon== null) weapon = other.GetComponentInParent<Weapon>();
        if (weapon ==null) weapon = other.GetComponentInChildren<Weapon>();

        if (weapon != null)
        {
            weapon.ApplyDecorator(type);
            Debug.Log("Pickup taken: "+type);
            Destroy(gameObject);
        }
    }
}