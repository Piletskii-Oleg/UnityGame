using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Events;
using Weapons.ScriptableObjects;

namespace Weapons
{
    public class GrappleGunPull : MonoBehaviour, IWeapon, IGrappleGun
    {
        [Header("Gun Info")]
        [SerializeField] private GunData gunData;
        [SerializeField] private UnityEvent onShoot;
        [SerializeField] private UnityEvent onReloadStarted;
        [SerializeField] private UnityEvent onReloadFinished;
        private bool reloading;
        
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
        private WaitForSeconds reloadWait;
        private Transform cam;

        private void Start()
        {
            cam = Camera.main.transform;
            grappleDelayWait = new WaitForSeconds(grappleDelay);
            reloadWait = new WaitForSeconds(gunData.reloadTime);
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
            
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
            if (gunData.currentAmmo > 0 && !reloading)
            {
                if (startGrappleCoroutine != null)
                {
                    StopCoroutine(startGrappleCoroutine);
                }

                gunData.currentAmmo--;
                startGrappleCoroutine = StartCoroutine(StartGrapple());

                onShoot.Invoke();
            }
        }

        public void StartReload()
        {
            if (!reloading)
            {
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            onReloadStarted.Invoke();
            
            reloading = true;

            yield return reloadWait;

            gunData.currentAmmo = gunData.ammoCapacity;

            reloading = false;

            onReloadFinished.Invoke();
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