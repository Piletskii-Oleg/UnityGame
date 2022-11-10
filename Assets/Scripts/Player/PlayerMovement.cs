using System.Collections;
using UnityEngine;

/// <summary>
/// Class that processes keyboard input used for moving the player (used with <see cref="InputManager"/>)
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private float slopeLimit;

    [Header("Movement speed")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    private float currentSpeed;

    [Header("Ground Drag")]
    [SerializeField] private float groundDrag;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;

    private bool isGrounded;

    private Vector3 moveDirection;
    private Rigidbody rigidBody;
    private CapsuleCollider capsule;

    private readonly WaitForSeconds waitTillLanded = new (0.02f);

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        currentSpeed = moveSpeed;
    }

    /// <summary>
    /// Processes horizontal movement of the player character.
    /// </summary>
    /// <param name="input">WASD or other input from <see cref="InputManager"/>.</param>
    public void ProcessHorizontalMovement(Vector2 input)
    {
        moveDirection = new Vector3(input.x, 0, input.y) * currentSpeed;
        moveDirection = transform.TransformDirection(moveDirection);

        if (isGrounded)
        {
            rigidBody.AddForce(moveDirection, ForceMode.VelocityChange);
        }
        else
        {
            rigidBody.AddForce(moveDirection * airMultiplier, ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// Checks if the player character is on ground and applies appropriate drag.
    /// </summary>
    public void ProcessVerticalMovement()
    {
        IsGroundedCheck();
        
        if (isGrounded)
        {
            rigidBody.drag = groundDrag;
        }
        else
        {
            rigidBody.drag = 0;
        }
    }

    /// <summary>
    /// Makes the player character jump if it is on proper ground.
    /// </summary>
    public void Jump()
    {
        if (isGrounded)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);

            rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Makes player character run if it is on the ground.
    /// </summary>
    public void StartRunning()
        => StartCoroutine(WaitTillLanded(runSpeed));

    /// <summary>
    /// Makes player character stop running if it is on the ground.
    /// </summary>
    public void StopRunning()
        => StartCoroutine(WaitTillLanded(moveSpeed));

    private IEnumerator WaitTillLanded(float speed)
    {
        while (!isGrounded)
        {
            yield return waitTillLanded;
        }

        currentSpeed = speed;
    }

    private void LateUpdate()
    {
        LimitVelocity();
    }

    private void IsGroundedCheck()
    {
        isGrounded = false;

        if (Physics.SphereCast(groundCheck.position, capsule.radius, Vector3.down, out RaycastHit hit, groundDistance))
        {
            if (Vector3.Angle(transform.up, hit.normal) <= slopeLimit)
            {
                isGrounded = true;
            }
        }
    }

    private void LimitVelocity()
    {
        var flatVelocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);

        if (flatVelocity.magnitude > currentSpeed)
        {
            var limitedVelocity = flatVelocity.normalized * currentSpeed;
            rigidBody.velocity = new Vector3(limitedVelocity.x, rigidBody.velocity.y, limitedVelocity.z);
        }
    }
}
