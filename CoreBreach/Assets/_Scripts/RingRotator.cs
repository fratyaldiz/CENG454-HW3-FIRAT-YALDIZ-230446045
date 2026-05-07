using UnityEngine;

public class RingRotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(20f, 30f, 0f);
    
    void Update()
    {
        transform.Rotate(rotationSpeed*Time.deltaTime);
    }
}