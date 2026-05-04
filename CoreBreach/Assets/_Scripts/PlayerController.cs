using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float turnSpeed = 300f; // how much mouse fast
    
    private CharacterController controller; //physics body for hit walls
    private Animator anim;

    private void Start()
    {
        // mouse hide and lock center screen
        Cursor.lockState =CursorLockMode.Locked;
        Cursor.visible = false;
        
        controller =GetComponent<CharacterController>();
        anim= GetComponent<Animator>();
    }

    private void Update()
    {
        RotatePlayer();
        MovePlayer();
    }

    private void RotatePlayer()
    {
        // get mouse move
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX* turnSpeed *Time.deltaTime);
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical= Input.GetAxisRaw("Vertical");

        Vector3 moveDir = (transform.forward * vertical +transform.right* horizontal).normalized;

        // BIG FIX: use controller for move, so we don't go inside walls!
        controller.Move(moveDir* moveSpeed * Time.deltaTime);
        
        // ANIMATION MAGIC: if we walk, play walk anim. if stop, play idle
        if (moveDir.magnitude >0.1f)
        {
            anim.SetFloat("Speed",1f);
        }
        else
        {
            anim.SetFloat("Speed",0f);
        }
    }
}