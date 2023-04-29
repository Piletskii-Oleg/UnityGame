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

        [Header("Fire Data")]
        [SerializeField] private GameObject firePrefab;
        [SerializeField] private Transform groundMouth;
        [SerializeField] private Transform flyMouth;

        [Header("Fire Stats")]
        [SerializeField] private float fireSpeed;
        [SerializeField] private float fireDelay;

        [Header("Dragon Data")]
        [SerializeField] private CircleArea area;
        [SerializeField] private HealthData health;
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
        
        public RamState RamState { get; private set; }

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
            RamState = new RamState(stateMachine, this);
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
            
            stateMachine.ChangeState(PlayerRanAwayState);
            hasBattleStarted = false;
        }

        public void EruptFlamesGround()
        {
            StartCoroutine(EruptFlamesFromMouth(groundMouth,1.7f, 1.6f));
        }

        private IEnumerator EruptFlamesFromMouth(Transform mouth, float animationTime, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            
            float time = 0;
            
            var waitForSeconds = new WaitForSeconds(fireDelay);
            
            while (time < animationTime)
            {
                time += fireDelay;
                var mouthTransform = mouth.transform;
                var fire = Instantiate(firePrefab, mouthTransform.position, mouthTransform.localRotation);
                StartCoroutine(MoveFire(fire));
                yield return waitForSeconds;
            }
        }

        private IEnumerator MoveFire(GameObject fire)
        {
            float groundLevel = 50;
            while (fire.transform.position.y - groundLevel > Mathf.Epsilon)
            {
                fire.transform.Translate(fire.transform.rotation * Vector3.right * (Time.deltaTime * fireSpeed));
                yield return null;
            }
        }
        
        public void EruptFlamesFlying()
        {
            StartCoroutine(EruptFlamesFlyingCoroutine());
        }

        private IEnumerator EruptFlamesFlyingCoroutine()
        {
            while (stateMachine.CurrentState is FlyAroundState)
            {
                yield return StartCoroutine(EruptFlamesFromMouth(flyMouth, 3f, 1f));
                yield return new WaitForSeconds(1.67f);
            }
        }

        public float DistanceTo(Vector3 point)
            => (transform.position - point).magnitude;

        public void SmashGround()
        {
            
        }

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