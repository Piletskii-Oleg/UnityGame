using System.Collections;
using Player;
using UnityEngine;
using Weapons.ScriptableObjects;

namespace Weapons
{
    public class GrappleGunJump : MonoBehaviour, IWeapon, IGrappleGun
    {
        [Header("Gun Info")]
        [SerializeField] private GunData gunData;
        private PlayerMovement playerMovement;
        
        [Header("Grappling")]
        [SerializeField] private float maxDistance;
        [SerializeField] private float grappleDelay;
        [SerializeField] private Transform gunTipTransform;
        [SerializeField] private float verticalOvershoot;
        private LineRenderer lineRenderer;

        [Header("Cooldown")]
        [SerializeField] private float grapplingCooldown;

        private Transform camTransform;
        private Vector3 grapplingPoint;
    
        private float cooldownTimer;
        private bool isGrappling;

        private WaitForSeconds grappleShootDelayWait;
        private Coroutine startGrappleCoroutine;

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

            grappleShootDelayWait = new WaitForSeconds(grappleDelay);
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
            if (startGrappleCoroutine != null)
            {
                StopCoroutine(startGrappleCoroutine);
            }
            
            startGrappleCoroutine = StartCoroutine(StartGrapple());
        }

        public void StartReload()
        {
        }

        public IEnumerator StartGrapple()
        {
            if (cooldownTimer > 0 || isGrappling)
            {
                yield break;
            }
            
            isGrappling = true;
            if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, maxDistance))
            {
                grapplingPoint = hit.point;

                yield return grappleShootDelayWait;
                
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
            yield return grappleShootDelayWait;
            
            isGrappling = false;

            cooldownTimer = grapplingCooldown;

            lineRenderer.enabled = false;

            yield return null;
        }

        public IEnumerator ExecuteGrapple()
        {
            var playerLowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
            var relativeGrappleHeight = grapplingPoint.y - playerLowestPoint.y;
            if (relativeGrappleHeight < 0)
            {
                StartCoroutine(playerMovement.JumpToPosition(grapplingPoint, verticalOvershoot));
                yield return new WaitForSeconds(1f);
            }
            else
            {
               StartCoroutine(playerMovement.JumpToPosition(grapplingPoint, relativeGrappleHeight + verticalOvershoot));
               yield return new WaitForSeconds(1f);
            }
            
            
            
            yield return StopGrapple();
        }
    }
}