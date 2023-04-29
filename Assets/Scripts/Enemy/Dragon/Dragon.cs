using System.Collections;
using Enemy.Dragon.States;
using Player.ScriptableObjects;
using Shared;
using Shared.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Dragon
{
    public class Dragon : Actor
    {
        private BaseStateMachine stateMachine;

        [Header("Player")]
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        [Header("Dragon Data")]
        [SerializeField] private CircleArea area;
        [SerializeField] private HealthData health;
        [SerializeField] private Transform[] points;
        [SerializeField] private int circlePointsCount;

        private Vector3[] nextPoints;

        [Tooltip("Time which player can stay outside of the area for until the dragon goes away")]
        [SerializeField] private float stayTime;
        
        private Coroutine stopBattleCoroutine;
        private bool hasBattleStarted;
        
        [Header("Dragon Stats")]
        [SerializeField] private float ramSpeed;
        [SerializeField] private float peekSpeed;
        [SerializeField] private float flyAroundSpeed;

        public Transform Center => area.transform;
        
        public float RamSpeed => ramSpeed;

        public float PeekSpeed => peekSpeed;

        public float FlySpeed => flyAroundSpeed;
        
        public AttackState AttackState { get; private set; }
        
        public FlyState FlyState { get; private set; }
        
        public DeadState DeadState { get; private set; }
        
        public PlayerRanAwayState PlayerRanAwayState { get; private set; }
        
        public SitOnGroundState SitOnGroundState { get; private set; }
        
        public PeekState PeekState { get; private set; }
        
        public FlyAroundState FlyAroundState { get; private set; }

        public Vector3 PlayerPosition => playerScriptableObject.GetActualPlayerPosition();
        
        private void Awake()
        {
            animator = GetComponent<Animator>();

            stateMachine = new DragonStateMachine(this);

            nextPoints = new Vector3[circlePointsCount];
            
            InitializeStates();
            
            stateMachine.Initialize(SitOnGroundState);
        }

        private void InitializeStates()
        {
            AttackState = new AttackState(stateMachine, this);
            FlyState = new FlyState(stateMachine, this);
            DeadState = new DeadState(stateMachine, this);
            PlayerRanAwayState = new PlayerRanAwayState(stateMachine, this);
            SitOnGroundState = new SitOnGroundState(stateMachine, this);
            PeekState = new PeekState(stateMachine, this);
            FlyAroundState = new FlyAroundState(stateMachine, this);
        }

        private void Update()
            => stateMachine.CurrentState?.Tick();

        public void InitiateBattle()
        {
            if (hasBattleStarted)
            {
                return;
            }
            
            hasBattleStarted = true;
                
            health.currentHealth = health.maxHealth;

            stateMachine.ChangeState(FlyState);
        }

        public void EnterArea()
        {
            if (stopBattleCoroutine != null)
            {
                Debug.Log("Enter Area");
                StopCoroutine(stopBattleCoroutine);
            }
        } 
        
        /// <summary>
        /// Attempts to stop the battle if the player runs away.
        /// </summary>
        public void TryStopBattle()
        {
            Debug.Log("stop");
            stopBattleCoroutine = StartCoroutine(StopBattle());
        }
        
        private IEnumerator StopBattle()
        {
            float timePassed = 0;
            while (timePassed < stayTime)
            {
                timePassed += Time.deltaTime;
                yield return null;
            }
            
            Debug.Log("Changing staet");
            stateMachine.ChangeState(PlayerRanAwayState);
            hasBattleStarted = false;
        }

        public void EruptFlamesGround()
        {
            
        }
        
        public void EruptFlamesFlying()
        {
            
        }

        public void Scream()
        {
            
        }

        public float DistanceTo(Vector3 point)
            => (transform.position - point).magnitude;

        public void SmashGround()
        {
            
        }

        public DragonBaseState ChooseNextState()
            => Random.Range(0, 5) switch
            {
                _ => AttackState,
            };

        public Vector3[] GetPointsAround()
        {
            float offset = Random.Range(0f, 360f);
            for (int i = 0; i < nextPoints.Length; i++)
            {
                float angle = offset + nextPoints.Length * (i + 1);
                nextPoints[i] = area.GetPositionOnCircle(angle);
            }

            return nextPoints;
        }
    }
}