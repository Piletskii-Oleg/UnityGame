using System.Collections;
using System.Collections.Generic;
using Core.Weapons;
using Core.Weapons.ScriptableObjects;
using Functionality.Sound;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Functionality.Weapons
{
    /// <summary>
    /// Class used for simple guns. Inherits from <see cref="IWeapon"/>.
    /// </summary>
    public class Gun : MonoBehaviour, IWeapon, IVolume
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
            
            var bulletAngle = Quaternion.Euler(rotation.eulerAngles.x - 90,
                rotation.eulerAngles.y, 0);
            Instantiate(bullet, thisTransform.position + thisTransform.forward, bulletAngle);

            gunData.currentAmmo--;

            onShoot.Invoke();
        }

        public void StartReload()
        {
            if (!isReloading)
            {
                StartCoroutine(Reload());
            }
        }

        public void ChangeVolume(float volume)
            => audioSource.volume = volume;

        private IEnumerator Reload()
        {
            onReloadStarted.Invoke();
            
            PlayReloadSound();

            isReloading = true;

            yield return reloadWait;

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
