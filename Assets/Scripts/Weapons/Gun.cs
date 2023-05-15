using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;
using UnityEngine.Events;
using Weapons.ScriptableObjects;
using Random = UnityEngine.Random;

namespace Weapons
{
    /// <summary>
    /// Class used for simple guns. Inherits from <see cref="IWeapon"/>.
    /// </summary>
    public class Gun : MonoBehaviour, IWeapon, IVolume
    {
        [Header("Data")]
        [Tooltip("Scriptable Object with the gun data.")]
        [SerializeField] private GunData gunData;

        [SerializeField] private MusicManager musicManager;

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

        private bool isReloading;

        public void Shoot()
        {
            if (gunData.currentAmmo <= 0 || isReloading)
            {
                return;
            }
            
            PlayShootSound();

            var thisTransform = transform;
            var rotation = thisTransform.rotation;
            
            var bulletAngle = Quaternion.Euler(rotation.eulerAngles.x + 90,
                rotation.eulerAngles.y, 0);
            Instantiate(bullet, thisTransform.position + thisTransform.forward, bulletAngle);

            gunData.currentAmmo--;

            onShoot.Invoke();
        }

        public void StartReload()
        {
            if (CanReload())
            {
                StartCoroutine(Reload());
            }
        }

        private bool CanReload()
            => !isReloading
               && gunData.currentAmmo != gunData.ammoCapacity
               && gunData.currentTotalAmmo - (gunData.ammoCapacity - gunData.currentAmmo) >= 0;

        public void ChangeVolume()
            => audioSource.volume = musicManager.SoundVolume;

        private IEnumerator Reload()
        {
            onReloadStarted.Invoke();
            
            PlayReloadSound();

            isReloading = true;

            yield return reloadWait;
            
            gunData.currentTotalAmmo -= (gunData.ammoCapacity - gunData.currentAmmo);
            gunData.currentAmmo = gunData.ammoCapacity;

            isReloading = false;

            onReloadFinished.Invoke();
        }

        private void OnDisable()
            => isReloading = false;

        private void Awake()
        {
            reloadWait = new WaitForSeconds(gunData.reloadTime);
            
            audioSource = GetComponent<AudioSource>();
            
            audioSource.volume = musicManager.SoundVolume;
        }
        
        private void PlayReloadSound()
        {
            audioSource.clip = reloadClip;
            audioSource.Play();
        }
        
        private void PlayShootSound()
        {
            audioSource.clip = shootClips[Random.Range(0, shootClips.Count - 1)];
            audioSource.Play();
        }
    }
}
