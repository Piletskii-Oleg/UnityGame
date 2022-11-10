using System.Collections;
using UnityEngine;

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

    private WaitForSeconds waitTillLanded = new WaitForSeconds(0.02f);

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        currentSpeed = moveSpeed;
    }

    public void ProcessHorizontalMovement(Vector2 input)
    {
        moveDirection = new Vector3(input.x, 0, input.y) * currentSpeed;

        if (isGrounded)
        {
            rigidBody.AddForce(transform.TransformDirection(moveDirection), ForceMode.VelocityChange);
        }
        else
        {
            rigidBody.AddForce(transform.TransformDirection(moveDirection * airMultiplier), ForceMode.VelocityChange);
        }
    }

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

    public void Jump()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);

        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void StartRunning()
        => StartCoroutine(WaitTillLanded(runSpeed));

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
