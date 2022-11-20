using System.Collections;
using Player;
using ScriptableObjects.Guns;
using UnityEngine;

namespace Weapons
{
    public class GrappleGunPull : MonoBehaviour, IWeapon, IGrappleGun
    {
        [Header("Gun Info")]
        [SerializeField] private GunData gunData;
        
        private PlayerMovement playerMovement;
        
        [Header("Grappling")]
        [SerializeField] private float grappleDelay;
        [SerializeField] private Transform gunTip;
        private LineRenderer lineRenderer;
        private Vector3 grapplingPoint;

        [Header("Pulling")]
        [SerializeField] private float pullSpeed;

        private Coroutine startGrappleCoroutine;
        private WaitForSeconds grappleDelayWait;
        private Transform cam;

        private void Start()
        {
            cam = Camera.main.transform;
            grappleDelayWait = new WaitForSeconds(grappleDelay);
            lineRenderer = GetComponent<LineRenderer>();
            
            playerMovement = GetComponentInParent<PlayerMovement>();
        }
        
        private void LateUpdate()
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.SetPosition(0, gunTip.position);
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
            if (Physics.Raycast(cam.position, cam.forward, out var hit, gunData.maxDistance))
            {
                grapplingPoint = hit.point;
                
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(1, grapplingPoint);

                yield return grappleDelayWait;

                yield return StartCoroutine(ExecuteGrapple());
            }
            else
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(1, cam.position + cam.forward * gunData.maxDistance);

                yield return grappleDelayWait;

                yield return StartCoroutine(StopGrapple());
            }
        }

        public IEnumerator StopGrapple()
        {
            lineRenderer.enabled = false;

            yield break;
        }

        public IEnumerator ExecuteGrapple()
        {
            playerMovement.PullTo(grapplingPoint, pullSpeed);

            yield return new WaitWhile(() => !playerMovement.CanMove);
            
            yield return StopGrapple();
        }
    }
}