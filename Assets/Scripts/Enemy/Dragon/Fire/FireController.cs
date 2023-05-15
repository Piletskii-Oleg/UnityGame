using System;
using System.Collections;
using Shared;
using Shared.ScriptableObjects;
using UnityEngine;

namespace Enemy.Dragon.Fire
{
    public class FireController : MonoBehaviour
    {
        [Header("Fire Data")]
        [SerializeField] private GameObject firePrefab;
        
        [Header("Dragon Data")]
        [Tooltip("Object from which fire will be erupted when the dragon is on the ground")]
        [SerializeField] private Transform groundMouth;
        
        [Tooltip("Object from which fire will be erupted when the dragon is flying")]
        [SerializeField] private Transform flyMouth;
        [SerializeField] private ActorData actorData;
        
        [Header("Fire Stats")]
        [SerializeField] private DragonFireSet fireSet;
        
        [Tooltip("Time in seconds between fire instantiation")]
        [SerializeField] private float fireDelay;
        
        [Tooltip("Time in seconds that will pass each time after fire damaging the player")]
        [SerializeField] private float fireDamageDelay;
        [SerializeField] private float fireDamage;
        
        [Tooltip("Time for which the player should not touch fire for it to stop dealing damage")]
        [SerializeField] private float fireNotTouchTime;
        
        private bool hasPlayerTouchedFire;

        private Coroutine eruptFlamesCoroutine;
        private Coroutine dealFireDamageCoroutine;
        private Coroutine tryStopDealingFireDamageCoroutine;

        private void Awake()
        {
            fireSet.Initialize(this);
        }

        /// <summary>
        /// Makes the dragon erupt flames when it is on the ground.
        /// </summary>
        public void EruptFlamesGround()
        {
            eruptFlamesCoroutine = StartCoroutine(EruptFlamesFromMouth(groundMouth));
        }

        /// <summary>
        /// Makes the dragon stop erupting flames.
        /// </summary>
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

        /// <summary>
        /// Makes the dragon erupt flames when it is flying.
        /// </summary>
        public void EruptFlamesFlying()
            => eruptFlamesCoroutine = StartCoroutine(EruptFlamesFromMouth(flyMouth));
        
        /// <summary>
        /// Makes the fire deal damage to the actor, with <see cref="fireDamageDelay"/> seconds
        /// passing before dealing damage again.
        /// </summary>
        /// <param name="actor">Actor which the damage will be dealt to.</param>
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

        /// <summary>
        /// Tries to stop dealing fire damage to the actor.
        /// </summary>
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
    }
}