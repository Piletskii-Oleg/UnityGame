using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player.ScriptableObjects;
using Shared;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class Enemy : Actor
    {
        private static readonly int doStep = Animator.StringToHash("DoStep");
        
        protected BaseStateMachine stateMachine;
        
        protected LayerMask playerMask;
        protected Collider[] playerInRange;
        
        protected NavMeshAgent agent;

        private LayerMask groundMask;

        [Header("Data")]
        [SerializeField] protected PlayerScriptableObject playerScriptableObject;
        
        [Tooltip("An object around which actor can roam freely")]
        [SerializeField] protected MobArea area;
        
        [Header("Attack State")]
        [Tooltip("Radius of a circle in which enemy will look for the player")]
        [SerializeField] protected float lookRadius;

        [Tooltip("Angle of a segment of a circle in which enemy can see the player")]
        [SerializeField] protected float lookAngle;

        [Tooltip("Time in seconds that should pass until enemy looks for the player again")]
        [SerializeField] protected float followTimeTact;

        [Tooltip("Amount of times that enemy will try to look for a player")]
        [SerializeField] protected int timesPlayerIsSearched;

        [Header("Damaged State")]
        [Tooltip("Amount of time which actor stays in damaged state for")]
        [SerializeField] [Range(0.0f, 3.0f)]
        protected float waitingTime;

        [Header("Dead State")]
        [Tooltip("Time in seconds after which the enemy disappears (applies after death)")]
        [SerializeField] private float timeUntilDisappearing;

        [Header("Spawned Objects")]
        [SerializeField] private List<GameObject> spawnedObjects;

        [SerializeField] private int spawnedObjectsCount;

        /// <summary>
        /// Position of the player calculated using <see cref="LookForPlayer"/> method.
        /// </summary>
        public Vector3 PlayerPosition { get; private set; }

        protected virtual void Awake()
        {
            groundMask = 1 << LayerMask.NameToLayer("Ground");
        }

        private void Update()
            => stateMachine.CurrentState.Tick();

        /// <summary>
        /// Activates the walk animation and makes actor move towards its next destination.
        /// </summary>
        public void WalkToDestination(Vector3 destination)
        {
            SetAnimationValue(doStep, true);
            agent.SetDestination(destination);
            agent.isStopped = false;
        }

        /// <summary>
        /// Stops the movement of the actor.
        /// </summary>
        public void Stop()
        {
            SetAnimationValue(doStep, false);
            agent.isStopped = true;
        }

        /// <summary>
        /// Searches for a player in a sphere area around it. Returns true if player is found and false otherwise.
        /// </summary>
        /// <returns>True if player is found and false otherwise.</returns>
        public bool LookForPlayer()
        {
            PlayerPosition = playerScriptableObject.GetActualPlayerPosition();
            
            int found = Physics.OverlapSphereNonAlloc(transform.position, lookRadius, playerInRange, playerMask);
            
            return found == 1;
        }

        /// <summary>
        /// Searches for a player in a sphere around it and limits its view to a segment with angle <see cref="lookAngle"/>.
        /// Returns true if player is found and false otherwise.
        /// </summary>
        /// <returns>True if player is found and false otherwise.</returns>
        public bool LookForPlayerInSegment()
        {
            if (!LookForPlayer())
            {
                return false;
            }

            var enemyTransform = transform;
            var position = enemyTransform.position;
            var directionToTarget = (PlayerPosition - position).normalized;

            float angle = Vector3.Angle(enemyTransform.forward, directionToTarget);
            return angle < lookAngle / 2;
        }

        public override void OnKill()
        {
            if (!isKilled)
            {
                base.OnKill();

                SpawnObjects();
            }
        }

        private void SpawnObjects()
        {
            for (int i = 0; i < spawnedObjectsCount; i++)
            {
                var obj = spawnedObjects[Random.Range(0, spawnedObjects.Count)];

                var position = transform.position;
                var spawnPosition = position;
                spawnPosition.y = position.y;
                
                var resultVector = GetResultPosition(spawnPosition);
                
                var gameObj = Instantiate(obj, spawnPosition, obj.transform.rotation);
                gameObj.transform.DOJump(resultVector, 2, 1, 1f);
            }
        }

        private Vector3 GetResultPosition(Vector3 spawnPosition)
        {
            var randomVector = GenerateRandomVector(-5f, 5f);
            randomVector.y = 0f;

            var resultVector = spawnPosition + randomVector;
            
            var lowPoint = resultVector;
            lowPoint.y -= 30;
            
            var highPoint = resultVector;
            highPoint.y += 30;

            if (Physics.Linecast(lowPoint, highPoint, out var info, groundMask))
            {
                resultVector.y = info.transform.position.y;
            }

            return resultVector;
        }

        /// <summary>
        /// Sets the <see cref="area"/> variable
        /// (if enemy is spawned, area cannot be initialized in the editor as it is scene-specific).
        /// </summary>
        /// <param name="newArea">Area which the spider belongs to.</param>
        public void SetArea(MobArea newArea)
            => area = newArea;
        
        /// <summary>
        /// Changes enemy's state to the <paramref name="idleState"/> for a <paramref name="period"/> seconds
        /// and then changes it to <paramref name="state"/>.
        /// </summary>
        /// <param name="period">Time in seconds for which enemy should idle.</param>
        /// <param name="idleState">Enemy's idle state.</param>
        /// <param name="state">New state, activated after <paramref name="period"/> seconds.</param>
        public void IdleForPeriod(float period, BaseState idleState, BaseState state)
            => StartCoroutine(IdleForPeriodCoroutine(period, idleState, state));
        
        private IEnumerator IdleForPeriodCoroutine(float period, BaseState idleState, BaseState state)
        {
            stateMachine.ChangeState(idleState);
            
            yield return new WaitForSeconds(period);
            
            if (stateMachine.CurrentState == idleState)
            {
                stateMachine.ChangeState(state);
            }
        }

        protected IEnumerator Disappear()
        {
            yield return new WaitForSeconds(timeUntilDisappearing);
            
            gameObject.SetActive(false);
        }
    }
}