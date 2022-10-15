using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpSpeed = 10.0f;
    [SerializeField] private float slideDownSpeed = 0.5f;
    private readonly float terminalVelocity = -50.0f;
    private Vector3 velocity;

    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private Transform groundCheck;
    public bool isGrounded;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void ProcessHorizontalMovement(Vector2 input)
    {
        var moveDirection = new Vector3(input.x, 0, input.y);
        controller.Move(speed * Time.deltaTime * transform.TransformDirection(moveDirection));
    }

    public void ProcessVerticalMovement()
    {
        TerminalVelocityCheck();
        OnGroundCheck();
        JumpCheckAndSlide();

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpSpeed * -2 * gravity);
        }
    }

    private void JumpCheckAndSlide()
    {
        isGrounded = false;

        if (Physics.SphereCast(groundCheck.position, controller.radius, Vector3.down, out RaycastHit hit, groundDistance))
        {
            var angle = Vector3.Angle(transform.up, hit.normal);
            if (angle <= controller.slopeLimit)
            {
                isGrounded = true;
            }
            else
            {
                var slideDown = hit.normal * slideDownSpeed;
                controller.Move(slideDown);
            }
        }
    }

    private void TerminalVelocityCheck()
        => velocity.y = (velocity.y < terminalVelocity) ? terminalVelocity : velocity.y;

    private void OnGroundCheck()
        => velocity.y = (velocity.y < -2.0f && isGrounded) ? -2.0f : velocity.y;
}
