using Enemy.Dragon.States;
using Player.ScriptableObjects;
using Shared;
using Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Dragon
{
    public class Dragon : Actor
    {
        private BaseStateMachine stateMachine;

        private NavMeshAgent agent;

        [Header("Player")]
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        [Header("Dragon Data")]
        [SerializeField] private CircleArea area;
        [SerializeField] private HealthData health;
        
        public AttackState AttackState { get; set; }
        
        public FlyState FlyState { get; set; }
        
        public DeadState DeadState { get; set; }
        
        public PlayerRanAwayState PlayerRanAwayState { get; set; }

        public Vector3 PlayerPosition => playerScriptableObject.GetActualPlayerPosition();
        
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            stateMachine = new DragonStateMachine();
            InitializeStates();
        }

        private void InitializeStates()
        {
            AttackState = new AttackState(stateMachine, this);
            FlyState = new FlyState(stateMachine, this);
            DeadState = new DeadState(stateMachine, this);
            PlayerRanAwayState = new PlayerRanAwayState(stateMachine, this);
        }

        private void Update()
            => stateMachine.CurrentState.Tick();

        public void InitiateBattle()
        {
            health.currentHealth = health.maxHealth;
            
            stateMachine.ChangeState(FlyState);
        }

        public void StopBattle()
        {
            stateMachine.ChangeState(PlayerRanAwayState);
        }
    }
}