using System.Collections;
using ScriptableObjects.Guns;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons
{
    /// <summary>
    /// Class used for simple guns. Inherits from <see cref="Weapon"/>.
    /// </summary>
    public class Gun : Weapon
    {
        [Tooltip("Scriptable Object with the gun data.")]
        [SerializeField] private GunData gunData;

        [Tooltip("Bullet shot.")]
        [SerializeField] private GameObject bullet;

        [SerializeField] private UnityEvent onShoot;
        [SerializeField] private UnityEvent onReload;

        private float timeSinceLastShot;

        private WaitForSeconds reloadWait;

        public override void Shoot()
        {
            if (gunData.currentAmmo > 0 && CanShoot())
            {
                var bulletAngle = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, 0);
                Instantiate(bullet, transform.position + transform.forward, bulletAngle);

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
            }

            onShoot.Invoke();
        }

        public override void StartReload()
        {
            if (!gunData.reloading)
            {
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            gunData.reloading = true;

            yield return reloadWait;

            gunData.currentAmmo = gunData.ammoCapacity;

            gunData.reloading = false;

            onReload.Invoke();
        }

        private void OnDisable()
            => gunData.reloading = false;

        private void Awake()
        {
            timeSinceLastShot = 0f;

            reloadWait = new WaitForSeconds(gunData.reloadTime);
        }

        private void FixedUpdate()
            => timeSinceLastShot += Time.fixedDeltaTime;

        private bool CanShoot()
            => !gunData.reloading && timeSinceLastShot > (1f / gunData.fireRate);
    }
}
