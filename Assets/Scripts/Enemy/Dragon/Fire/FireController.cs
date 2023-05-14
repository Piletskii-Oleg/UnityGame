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
        [SerializeField] private Transform groundMouth;
        [SerializeField] private Transform flyMouth;
        [SerializeField] private ActorData actorData;
        
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

        private void Awake()
        {
            fireSet.Initialize(this);
        }

        public void EruptFlamesGround()
        {
            eruptFlamesCoroutine = StartCoroutine(EruptFlamesFromMouth(groundMouth));
        }

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
    }
}