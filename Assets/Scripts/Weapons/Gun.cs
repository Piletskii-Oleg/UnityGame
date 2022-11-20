using System.Collections;
using ScriptableObjects.Guns;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private UnityEvent onReload;

        private WaitForSeconds reloadWait;

        public void Shoot()
        {
            if (gunData.currentAmmo > 0 && !gunData.reloading)
            {
                var bulletAngle = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, 0);
                Instantiate(bullet, transform.position + transform.forward, bulletAngle);

                gunData.currentAmmo--;
            }

            onShoot.Invoke();
        }

        public void StartReload()
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
            => reloadWait = new WaitForSeconds(gunData.reloadTime);
    }
}
