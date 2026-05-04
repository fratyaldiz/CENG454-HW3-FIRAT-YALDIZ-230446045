using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          
    // pubg fortnite style offset (right, up, back)
    public Vector3 offset = new Vector3(1.2f, 1.8f, -3f); 
    public float smoothSpeed = 0.125f;

    // mouse y speed for look up down
    public float lookSpeed = 300f;
    private float xRotation = 0f; // neck angle

    void LateUpdate()
    {
        // if no target, stop
        if (target == null) return;

        // find shoulder position
        Vector3 desiredPosition = target.TransformPoint(offset);
        
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // get mouse up and down from user
        float mouseY = Input.GetAxis("Mouse Y");
        
        // calculate neck move with time
        xRotation -=mouseY *lookSpeed * Time.deltaTime;
        
        xRotation = Mathf.Clamp(xRotation,-60f, 60f);

        // body look left/right, camera look up/down! 
        transform.rotation = Quaternion.Euler(xRotation, target.eulerAngles.y, 0f);
    }
}