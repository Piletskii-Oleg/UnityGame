﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        [SerializeField] private DragonFireSet fireSet;
        [SerializeField] private float fireDelay;
        [SerializeField] private float fireDamageDelay;
        [SerializeField] private float fireDamage;
        [SerializeField] private float fireNotTouchTime;

        private bool hasPlayerTouchedFire;
        
        private Coroutine eruptFlamesCoroutine;
        private Coroutine dealFireDamageCoroutine;
        private Coroutine tryStopDealingFireDamageCoroutine;

        [Header("Dragon Data")]
        [SerializeField] private CircleArea area;
        [SerializeField] private HealthData health;
        [SerializeField] private int circlePointsCount;
        [SerializeField] private Transform areaPlane;

        private Vector3[] nextPoints;

        [Tooltip("Time which player can stay outside of the area for until the dragon goes away")]
        [SerializeField] private float stayTime;
        
        private Coroutine stopBattleCoroutine;
        private bool hasBattleStarted;

        [Header("Dragon Stats")]
        [SerializeField] private float ramSpeed;
        [SerializeField] private float peekSpeed;
        [SerializeField] private float flyAroundSpeed;
        [SerializeField] private float smashDamage;
        [SerializeField] private float ramDamage;

        public Transform Center => area.transform;
        
        public float RamSpeed => ramSpeed;

        public float PeekSpeed => peekSpeed;

        public float FlySpeed => flyAroundSpeed;

        public FlyState FlyState { get; private set; }
        
        public DeadState DeadState { get; private set; }
        
        public PlayerRanAwayState PlayerRanAwayState { get; private set; }
        
        public SitOnGroundState SitOnGroundState { get; private set; }
        
        public PeekState PeekState { get; private set; }
        
        public FlyAroundState FlyAroundState { get; private set; }
        
        public RamState RamState { get; private set; }
        
        public NotInFightState NotInFightState { get; private set; }

        private List<DragonBaseState> states;

        public Vector3 GetPlayerPosition()
            => playerScriptableObject.GetActualPlayerPosition();
        
        private void Awake()
        {
            animator = GetComponent<Animator>();

            stateMachine = new DragonStateMachine(this);

            nextPoints = new Vector3[circlePointsCount];
            
            InitializeStates();
            
            stateMachine.Initialize(SitOnGroundState);

            fireSet.Initialize(this);
        }

        private void InitializeStates()
        {
            states = new();
            FlyState = new FlyState(stateMachine, this);
            DeadState = new DeadState(stateMachine, this);
            PlayerRanAwayState = new PlayerRanAwayState(stateMachine, this);
            SitOnGroundState = new SitOnGroundState(stateMachine, this);
            PeekState = new PeekState(stateMachine, this);
            FlyAroundState = new FlyAroundState(stateMachine, this);
            RamState = new RamState(stateMachine, this);
            NotInFightState = new NotInFightState(stateMachine, this);
        }

        private void Update()
            => stateMachine.CurrentState?.Tick();

        public void InitiateBattle()
        {
            if (hasBattleStarted || stateMachine.CurrentState is DeadState)
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
            => eruptFlamesCoroutine = StartCoroutine(EruptFlamesFromMouth(groundMouth));

        public void StopEruptingFlames()
        {
            if (eruptFlamesCoroutine != null)
            {
                StopCoroutine(eruptFlamesCoroutine);
            }
        }

        private IEnumerator EruptFlamesFromMouth(Transform mouth)
        {
            var waitForSeconds = new WaitForSeconds(fireDelay);
            
            while (true)
            {
                Instantiate(firePrefab, mouth.position, mouth.rotation);
                yield return waitForSeconds;
            }
        }

        public void EruptFlamesFlying()
            => eruptFlamesCoroutine = StartCoroutine(EruptFlamesFromMouth(flyMouth));

        public float DistanceTo(Vector3 point)
            => (transform.position - point).magnitude;

        public void SmashGround()
        {
            var explosionPoint = transform.position;
            explosionPoint.y = BaseHeight;
            playerScriptableObject.PlayerRigidbody.AddExplosionForce(1000f, explosionPoint, 30f, 6.0f);

            playerScriptableObject.PlayerActor.OnTakeDamage(smashDamage, actorData.affiliation);
        }

        public Vector3[] GetPointsAround()
        {
            float offset = Random.Range(0f, 360f);
            for (int i = 0; i < nextPoints.Length; i++)
            {
                float angle = offset + nextPoints.Length * (i + 1); // 12 -> one full circle
                nextPoints[i] = area.GetPositionOnCircle(angle);
            }

            return nextPoints;
        }

        public Vector3 GetPointInArea()
            => area.GetNewPosition();

        public void StartDealingFireDamage(Actor actor)
        {
            if (!hasPlayerTouchedFire)
            {
                dealFireDamageCoroutine = StartCoroutine(DealFireDamage(actor));
            }

            if (tryStopDealingFireDamageCoroutine != null)
            {
                StopCoroutine(tryStopDealingFireDamageCoroutine);
            }
        }

        public void StopDealingFireDamage()
        {
            tryStopDealingFireDamageCoroutine = StartCoroutine(TryStopFireDamage());
        }

        private IEnumerator DealFireDamage(Actor actor)
        {
            hasPlayerTouchedFire = true;
            
            var wait = new WaitForSeconds(fireDamageDelay);

            while (true)
            {
                actor.OnTakeDamage(fireDamage, actorData.affiliation);

                yield return wait;
            }
        }

        private IEnumerator TryStopFireDamage()
        {
            var wait = new WaitForSeconds(fireNotTouchTime);
            yield return wait;

            hasPlayerTouchedFire = false;
            StopCoroutine(dealFireDamageCoroutine);
        }

        public float BaseHeight => areaPlane.position.y;

        public override void OnKill()
        {
            StopAllCoroutines();

            DOTween.Kill(transform);
            StopAllSequences();

            stateMachine.ChangeState(DeadState);
        }

        public void AddState(DragonBaseState state)
            => states.Add(state);

        private void StopAllSequences()
            => states.ForEach(state => state.KillSequences());
    }
}