using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Weapons.ScriptableObjects;

namespace Weapons
{
    /// <summary>
    /// Class used for simple guns. Inherits from <see cref="Weapon"/>.
    /// </summary>
    public class Gun : MonoBehaviour, IWeapon
    {
        [Tooltip("Scriptable Object with the gun data.")]
        [SerializeField] private GunData gunData;

        [Tooltip("Bullet shot.")]
        [SerializeField] private GameObject bullet;

        [SerializeField] private UnityEvent onShoot;
        [SerializeField] private UnityEvent onReloadStarted;
        [SerializeField] private UnityEvent onReloadFinished;

        private WaitForSeconds reloadWait;

        private bool reloading;

        public void Shoot()
        {
            if (gunData.currentAmmo > 0 && !reloading)
            {
                var bulletAngle = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, 0);
                Instantiate(bullet, transform.position + transform.forward, bulletAngle);

                gunData.currentAmmo--;
                
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

        private void OnDisable()
            => reloading = false;

        private void Awake()
            => reloadWait = new WaitForSeconds(gunData.reloadTime);
    }
}
