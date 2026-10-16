using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Speeds")]
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float crouchSpeed = 2.5f;

    [Header("Jump")]
    public float jumpForce = 9f;
    public float groundCheckDistance = 0.75f;

    [Header("Crouch")]
    public float crouchHeightMultiplier = 0.5f;

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private float originalHeight;
    private Vector3 originalCenter;

    private float horizontalInput;
    private bool jumpQueued;
    private bool isGrounded;
    private bool isCrouching;
    private bool isRunning;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        originalHeight = capsule.height;
        originalCenter = capsule.center;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Run: Shift on keyboard, right trigger on gamepad
        isRunning = Input.GetKey(KeyCode.LeftShift)
                 || Input.GetKey(KeyCode.RightShift)
                 || Input.GetAxis("Fire2") > 0.5f;

        // Crouch: Ctrl / S on keyboard, down on left stick for gamepad
        isCrouching = Input.GetKey(KeyCode.LeftControl)
                   || Input.GetKey(KeyCode.S)
                   || Input.GetAxisRaw("Vertical") < -0.5f;

        // Can't jump while crouched
        if (Input.GetButtonDown("Jump") && !isCrouching)
            jumpQueued = true;

        UpdateCrouchCollider();
    }

    private void UpdateCrouchCollider()
    {
        float targetHeight = isCrouching
            ? originalHeight * crouchHeightMultiplier
            : originalHeight;

        // When trying to stand up, check there's headroom first
        if (!isCrouching && capsule.height < originalHeight)
        {
            float checkDistance = originalHeight - capsule.height;
            if (Physics.Raycast(transform.position, Vector3.up, checkDistance + 0.2f))
                return; // blocked overhead, stay small
        }

        capsule.height = targetHeight;
        // Shift center down so feet stay planted
        capsule.center = originalCenter + Vector3.down * (originalHeight - targetHeight) / 2f;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance
        );

        float speed = isCrouching ? crouchSpeed
                    : isRunning   ? runSpeed
                    : walkSpeed;

        Vector3 vel = rb.linearVelocity;
        vel.x = horizontalInput * speed;

        if (jumpQueued && isGrounded)
            vel.y = jumpForce;

        jumpQueued = false;
        rb.linearVelocity = vel;
    }
}