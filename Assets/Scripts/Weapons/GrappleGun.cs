using System;
using System.Collections;
using Player;
using ScriptableObjects.Guns;
using UnityEngine;

namespace Weapons
{
    public class GrappleGun : MonoBehaviour, IWeapon
    {
        [Header("Gun Info")]
        [SerializeField] private GunData gunData;
        private PlayerMovement playerMovement;
        
        [Header("Grappling")]
        [SerializeField] private float maxDistance;
        [SerializeField] private float grappleDelay;
        [SerializeField] private Transform gunTipTransform;
        private LineRenderer lineRenderer;

        [Header("Cooldown")]
        [SerializeField] private float grapplingCooldown;

        private Transform camTransform;
        private Vector3 grapplingPoint;
    
        private float cooldownTimer;
        private bool isGrappling;

        private WaitForSeconds grappleDelayWait;
        private Coroutine startGrappleCoroutine;
        private Coroutine stopGrappleCoroutine;
        private Coroutine executeGrappleCoroutine;

        private void Start()
        {
            playerMovement = GetComponentInParent<PlayerMovement>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
            var cam = Camera.main;
            if (cam != null)
            {
                camTransform = cam.transform;
            }

            grappleDelayWait = new WaitForSeconds(grappleDelay);
        }

        private void FixedUpdate()
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.fixedDeltaTime;
            }
        }

        private void LateUpdate()
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.SetPosition(0, gunTipTransform.position);
            }
        }

        public void Shoot()
        {
            startGrappleCoroutine = StartCoroutine(StartGrapple());
        }

        public void StartReload()
        {
            throw new NotImplementedException();
        }

        public IEnumerator StartGrapple()
        {
            if (cooldownTimer > 0)
            {
                yield break;
            }
            
            isGrappling = true;
            if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, maxDistance))
            {
                grapplingPoint = hit.point;

                yield return grappleDelayWait;
                
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(1, grapplingPoint);
                
                yield return StartCoroutine(ExecuteGrapple());
            }
            else
            {
                grapplingPoint = camTransform.position + camTransform.forward * maxDistance;

                lineRenderer.enabled = true;
                lineRenderer.SetPosition(1, grapplingPoint);
                
                yield return StartCoroutine(StopGrapple());
            }
        }

        public IEnumerator StopGrapple()
        {
            yield return grappleDelayWait;
            
            isGrappling = false;

            cooldownTimer = grapplingCooldown;

            lineRenderer.enabled = false;

            yield return null;
        }

        public IEnumerator ExecuteGrapple()
        {
            playerMovement.JumpToPosition(grapplingPoint, 10f);

            yield return StopGrapple();
        }
    }
}