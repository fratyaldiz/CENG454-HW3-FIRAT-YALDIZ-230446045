using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;    //how fast we go
    private Camera mainCam;

    private void Start()
    {
        //get main camera to calculate mouse position later
        mainCam =Camera.main;
    }

    private void Update()
    {
        MovePlayer();
        LookAtMouse();
    }

    private void MovePlayer()
    {
        // get WASD input
        float horizontal= Input.GetAxisRaw("Horizontal");
        float vertical =Input.GetAxisRaw("Vertical");

        // calculate direction
        Vector3 moveDir =new Vector3(horizontal, 0f, vertical).normalized;

        // move the character
        transform.Translate(moveDir *moveSpeed* Time.deltaTime,Space.World);
    }

    private void LookAtMouse()
    {
        Plane groundPlane =new Plane(Vector3.up,transform.position);
        Ray ray= mainCam.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (groundPlane.Raycast(ray,out distance))
        {
            // find exact point mouse looking at
            Vector3 point= ray.GetPoint(distance);
            
            // look at that point but keep Y same so we don't look up/down
            Vector3 lookPoint =new Vector3(point.x, transform.position.y, point.z);
            transform.LookAt(lookPoint);
        }
    }
}