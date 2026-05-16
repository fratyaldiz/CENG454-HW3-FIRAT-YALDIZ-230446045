using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public enum DecoratorType
    {
        DoubleShot,
        Piercing,
        FireRate
    }

    [SerializeField] private DecoratorType type;
    [SerializeField] private float rotateSpeed = 90f;

    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // walk up parent hierarchy from bone to find weapon
        Transform current = other.transform;
        Weapon weapon = null;

        while (current != null && weapon == null)
        {
            weapon = current.GetComponent<Weapon>();
            current = current.parent;
        }

        if (weapon != null)
        {
            weapon.ApplyDecorator(type);
            Debug.Log("Pickup taken: " + type);
            Destroy(gameObject);
        }
    }
}