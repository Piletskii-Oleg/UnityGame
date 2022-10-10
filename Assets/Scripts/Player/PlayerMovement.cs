using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Input")]
public class PlayerMovement : MonoBehaviour
{
    public bool drawGizmos = true;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpSpeed = 10.0f;
    [SerializeField] private float slideDownSpeed = 0.5f;
    private Vector3 velocity;

    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private Transform groundCheck;
    public bool isGrounded;
    private LayerMask groundMask;

    private CharacterController controller;

    private void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");

        var movement = transform.right * deltaX + transform.forward * deltaZ;
        movement = Vector3.ClampMagnitude(movement, speed);
        movement = transform.TransformDirection(movement);
        controller.Move(speed * Time.deltaTime * movement);

        SetVerticalVelocity();
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
    }

    private void SetVerticalVelocity()
    {
        if (velocity.y < -50.0f)
        {
            velocity.y = -50.0f;
        }

        GroundCheck();

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = 0.0f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpSpeed * -2 * gravity);
        }

        Debug.Log(velocity);
    }

    private void GroundCheck()
    {
        isGrounded = false;

        if (Physics.SphereCast(groundCheck.position, controller.radius, Vector3.down, out RaycastHit hit, groundDistance))
        {
            if (Vector3.Angle(transform.up, hit.normal) <= controller.slopeLimit)
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

    // Physics.CapsuleCast(GetCapsuleBottomHemisphere(), GetCapsuleTopHemisphere(m_Controller.height),
    // m_Controller.radius, Vector3.down, out RaycastHit hit, chosenGroundCheckDistance, GroundCheckLayers,
    // QueryTriggerInteraction.Ignore)
}
