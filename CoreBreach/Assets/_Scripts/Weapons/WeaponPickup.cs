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
        transform.Rotate(0,rotateSpeed *Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = FindFirstObjectByType<Weapon>();

        if (weapon != null)
        {
            weapon.ApplyDecorator(type);
            Debug.Log("Pickup taken: " +type);
            Destroy(gameObject);
        }
    }
}