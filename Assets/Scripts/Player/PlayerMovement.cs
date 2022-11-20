using System.Collections;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Class that processes keyboard input used for moving the player (used with <see cref="InputManager"/>).
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Ground Check")] 
        [SerializeField] private Transform groundCheck;

        [SerializeField] private float groundDistance;
        [SerializeField] private float slopeLimit;

        [Header("Movement Speed")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float runSpeed;
        private float currentSpeed;

        [Header("Ground Drag")]
        [SerializeField] private float groundDrag;

        [Header("Jumping")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float airMultiplier;

        private bool isGrounded;

        private Vector3 moveDirection;
        private Rigidbody rigidBody;
        private CapsuleCollider capsule;

        private WaitForFixedUpdate waitForFixedUpdate;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            capsule = GetComponent<CapsuleCollider>();
            currentSpeed = moveSpeed;
            waitForFixedUpdate = new WaitForFixedUpdate();
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
                var velocity = rigidBody.velocity;
                velocity = new Vector3(velocity.x, 0, velocity.z);
                rigidBody.velocity = velocity;

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

        /// <summary>
        /// Makes the player character jump to the <paramref name="targetPosition"/>
        /// with the max height of the jump being <paramref name="trajectoryHeight"/>.
        /// </summary>
        /// <param name="targetPosition">The position player character has to jump to.</param>
        /// <param name="trajectoryHeight">Maximum height of the trajectory.</param>
        public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
            => rigidBody.velocity = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);

        private Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
        {
            float gravity = Physics.gravity.y;
            float displacementY = endPoint.y - startPoint.y;
            var displacementXZ = new Vector3(endPoint.x - startPoint.x, 0, endPoint.z - startPoint.z);

            var velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
            var velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) +
                             Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

            return velocityXZ + velocityY;
        }

        private IEnumerator WaitTillLanded(float speed)
        {
            while (!isGrounded)
            {
                yield return waitForFixedUpdate;
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

            if (Physics.SphereCast(groundCheck.position, capsule.radius, Vector3.down, out RaycastHit hit,
                    groundDistance))
            {
                if (Vector3.Angle(transform.up, hit.normal) <= slopeLimit)
                {
                    isGrounded = true;
                }
            }
        }

        private void LimitVelocity()
        {
            var velocity = rigidBody.velocity;
            var flatVelocity = new Vector3(velocity.x, 0, velocity.z);

            if (flatVelocity.magnitude > currentSpeed)
            {
                var limitedVelocity = flatVelocity.normalized * currentSpeed;
                rigidBody.velocity = new Vector3(limitedVelocity.x, rigidBody.velocity.y, limitedVelocity.z);
            }
        }
    }
}
