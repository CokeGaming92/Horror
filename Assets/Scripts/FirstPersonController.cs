using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 3f;
    public float runningSpeed = 7f;
    public float gravity = 9.81f; // Earth's gravity
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

   

    Rigidbody rb;
    float rotationX = 0;

    public bool canRun = false;
    public bool isRunning = false;
    public bool enableCooldown;
    public float runTimer = 0f;
    private float minMoveSpeed = 0.1f; // Minimum movement speed to consider the player as moving
    [HideInInspector]
    public bool canMove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze rotation so we can control it manually
    }

    void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        float curSpeedX;
        float curSpeedY;

        if (!canRun)
        {
            curSpeedX = canMove ? walkingSpeed * Input.GetAxis("Vertical") : 0;
            curSpeedY = canMove ? walkingSpeed * Input.GetAxis("Horizontal") : 0;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRunning = true;
                runTimer = 0f;
            }

            if (isRunning)
            {
                runTimer += Time.deltaTime;
                if (runTimer >= 1f) // Limit running to one second
                {
                    isRunning = false;
                    canRun = false; // Prevent continuous running until explicitly enabled again
                    enableCooldown = true;
                }
            }

            curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        }


        // Check if the player is moving
        bool isMoving = rb.velocity.magnitude > minMoveSpeed;

        // Update the canMove flag based on player's movement state
        canMove = isMoving;

        Vector3 velocity = (forward * curSpeedX) + (right * curSpeedY);
        velocity.y = rb.velocity.y; // Preserve the vertical velocity

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime;

        // Apply movement
        rb.velocity = velocity;

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        

    }

 

    public void CanRun()
    {
        canRun = true;
    }
}