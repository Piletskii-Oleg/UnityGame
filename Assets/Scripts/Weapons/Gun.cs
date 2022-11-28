using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Weapons.ScriptableObjects;

namespace Weapons
{
    /// <summary>
    /// Class used for simple guns. Inherits from <see cref="IWeapon"/>.
    /// </summary>
    public class Gun : MonoBehaviour, IWeapon
    {
        [Header("Gun Data")]
        [Tooltip("Scriptable Object with the gun data.")]
        [SerializeField] private GunData gunData;

        [Tooltip("Bullet shot.")]
        [SerializeField] private GameObject bullet;

        [Header("Sound Effects")]
        [SerializeField] private List<AudioClip> shootClips;
        [SerializeField] private AudioClip reloadClip;
        private AudioSource audioSource;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onShoot;
        [SerializeField] private UnityEvent onReloadStarted;
        [SerializeField] private UnityEvent onReloadFinished;

        private WaitForSeconds reloadWait;

        private bool reloading;

        public void Shoot()
        {
            if (gunData.currentAmmo > 0 && !reloading)
            {
                audioSource.clip = shootClips[Random.Range(0, shootClips.Count - 1)];
                audioSource.Play();
                
                var bulletAngle = Quaternion.Euler(transform.rotation.eulerAngles.x - 90,
                    transform.rotation.eulerAngles.y, 0);
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
            
            audioSource.clip = reloadClip;
            audioSource.Play();
            
            reloading = true;

            yield return reloadWait;

            gunData.currentAmmo = gunData.ammoCapacity;

            reloading = false;

            onReloadFinished.Invoke();
        }

        private void OnDisable()
            => reloading = false;

        private void Awake()
        {
            reloadWait = new WaitForSeconds(gunData.reloadTime);
            audioSource = GetComponent<AudioSource>();
        }
    }
}
