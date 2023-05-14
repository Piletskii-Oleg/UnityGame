using Enemy.FlyingDragon.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.FlyingDragon
{
    public class FlyingDragon : MonoBehaviour
    {
        private Animator animator;
        private FlyingStateMachine stateMachine;

        [SerializeField] private float speed;
        [Tooltip("Points at which dragon will land")]
        [SerializeField] private Transform[] pointsTo;
        [Tooltip("Points around which dragon will float, high above the ground")]
        [SerializeField] private Transform[] pointsAbove;

        public FlyToPointState FlyToPointState { get; private set; }
        
        public StandState StandState { get; private set; }
        
        public FlyAroundState FlyAroundState { get; private set; }
        
        public TakeOffState TakeOffState { get; private set; }
        
        public LandingState LandingState { get; private set; }
        
        public HiddenState HiddenState { get; private set; }

        public float Speed => speed;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            stateMachine = new FlyingStateMachine();

            InitializeStates();

            stateMachine.Initialize(StandState);
        }

        private void InitializeStates()
        {
            FlyToPointState = new FlyToPointState(this, stateMachine, pointsTo);
            StandState = new StandState(this, stateMachine);
            FlyAroundState = new FlyAroundState(this, stateMachine, pointsAbove, pointsTo);
            TakeOffState = new TakeOffState(this, stateMachine);
            LandingState = new LandingState(this, stateMachine);
            HiddenState = new HiddenState(this, stateMachine);
        }

        private void Update()
            => stateMachine.CurrentState.Tick();

        /// <summary>
        /// Triggers an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        public void TriggerAnimation(int animationHash)
            => animator.SetTrigger(animationHash);

        public void Kill()
            => Destroy(gameObject);

        public void Respawn()
        {
            transform.position = pointsTo[Random.Range(0, pointsTo.Length)].position;
            
            gameObject.SetActive(true);
            
            stateMachine.ChangeState(StandState);
        }

        public void Stop()
        {
            stateMachine.KillSequence();
            
            stateMachine.ChangeState(HiddenState);
            
            transform.position = Vector3.zero;
        }
    }
}