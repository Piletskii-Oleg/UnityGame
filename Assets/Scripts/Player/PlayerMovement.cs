using System.Collections;
using DataPersistence;
using DataPersistence.DataFiles;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Class that processes keyboard input used for moving the player (used with <see cref="InputController"/>).
    /// </summary>
    public class PlayerMovement : MonoBehaviour, IDataPersistence<GameData>
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
        private Coroutine pullToPosition;
        
        public bool CanMove { get; private set; }

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            capsule = GetComponent<CapsuleCollider>();
            
            currentSpeed = moveSpeed;
            CanMove = true;
            
            waitForFixedUpdate = new WaitForFixedUpdate();
        }

        private void FixedUpdate()
        {
            ProcessVerticalMovement();
        }

        /// <summary>
        /// Processes horizontal movement of the player character.
        /// </summary>
        /// <param name="input">WASD or other input from <see cref="InputController"/>.</param>
        public void ProcessHorizontalMovement(Vector2 input)
        {
            if (CanMove)
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
        }

        /// <summary>
        /// Checks if the player character is on ground and applies appropriate drag.
        /// </summary>
        private void ProcessVerticalMovement()
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
            if (isGrounded || !CanMove)
            {
                var velocity = rigidBody.velocity;
                velocity = new Vector3(velocity.x, 0, velocity.z);
                rigidBody.velocity = velocity;

                rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

                if (pullToPosition != null)
                {
                    StopCoroutine(pullToPosition);
                    CanMove = true;
                    rigidBody.useGravity = true;
                }
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

        public void OnSave(GameData data)
        {
            var rigidBodyTransform = rigidBody.transform;
            data.playerPosition = rigidBodyTransform.position;
            data.playerRotation = rigidBodyTransform.rotation;
            data.playerVelocity = rigidBody.velocity;
        }
        
        public void OnLoad(GameData data)
        {
            var playerTransform = transform;
            playerTransform.position = data.playerPosition;
            playerTransform.rotation = data.playerRotation;
            rigidBody.velocity = data.playerVelocity;
        }
        
        /// <summary>
        /// Makes the player character be pulled to <paramref name="pullTarget"/> with <paramref name="pullSpeed"/> speed.
        /// </summary>
        /// <param name="pullTarget">Position in the world which the player character is pulled to.</param>
        /// <param name="pullSpeed">Speed at which the player character is pulled.</param>
        public void PullTo(Vector3 pullTarget, float pullSpeed)
        {
            if (pullToPosition != null)
            {
                StopCoroutine(pullToPosition);
            }
            
            pullToPosition = StartCoroutine(PullToPosition(pullTarget, pullSpeed));
        }

        /// <summary>
        /// Stops the player from pulling to position.
        /// </summary>
        public void StopPulling()
        {
            if (pullToPosition != null)
            {
                StopCoroutine(pullToPosition);
                CanMove = true;
                rigidBody.useGravity = true;
            }
        }

        private IEnumerator PullToPosition(Vector3 pullTarget, float pullSpeed)
        {
            CanMove = false;
            rigidBody.useGravity = false;

            var source = transform.position;
            var target = new Vector3(pullTarget.x, pullTarget.y + 1, pullTarget.z);
            
            var direction = (target - source).normalized * pullSpeed;

            float timePassed = 0f;
            float pullTime = Vector3.Distance(source, target) / pullSpeed;
            while (timePassed < pullTime)
            {
                rigidBody.velocity = direction;
                timePassed += Time.fixedDeltaTime;
                yield return waitForFixedUpdate;
            }

            CanMove = true;
            rigidBody.useGravity = true;
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

            if (Physics.SphereCast(groundCheck.position, capsule.radius, Vector3.down, out var hit,
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
            if (CanMove)
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
}
