using System.Collections;
using Core.DataPersistence.DataFiles;
using Core.Weapons;
using Core.Weapons.ScriptableObjects;
using UnityEngine;

namespace Core.Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private Transform weaponHolder;

        private IWeapon currentWeapon;
        
        private Coroutine shootRepeatedlyCoroutine;
        private bool isShootingRepeatedly;
        private WaitForSeconds fireDelay;

        private void Start()
        {
            this.ChangeWeapon(weaponManager.CurrentIndex);
        }

        /// <summary>
        /// Makes the player shoot (or start shooting if the weapon is automatic) with the currently held weapon.
        /// </summary>
        public void StartShooting()
        {
            if (currentWeapon == null)
            {
                return;
            }

            if (weaponManager.CurrentGunData.canAutoShoot)
            {
                isShootingRepeatedly = true;
                
                shootRepeatedlyCoroutine = StartCoroutine(ShootRepeatedly());
            }
            else
            {
                if (!weaponManager.CurrentGunItem.HasShot)
                {
                    StartCoroutine(ShootOnce());
                }
            }
        }

        private IEnumerator ShootOnce()
        {
            var item = weaponManager.CurrentGunItem;
            
            item.HasShot = true;

            currentWeapon.Shoot();
            
            yield return fireDelay;

            item.HasShot = false;
        }

        /// <summary>
        /// Makes the player stop shooting if the weapon is automatic with the currently held weapon.
        /// </summary>
        public void StopShooting()
        {
            if (shootRepeatedlyCoroutine != null)
            {
                StopCoroutine(shootRepeatedlyCoroutine);
                isShootingRepeatedly = false;
            }
        }

        /// <summary>
        /// Makes the player start reloading the currently held weapon.
        /// </summary>
        public void StartReload()
        {
            if (currentWeapon != null)
            {
                currentWeapon.StartReload();
            }
        }

        /// <summary>
        /// Used to change the currently held weapon.
        /// </summary>
        /// <param name="index">Index of the weapon in the list.</param>
        public void ChangeWeapon(int index)
        {
            index--; // index parameter is a key from 1 to 9, hence we need to decrease it by 1 to get an actual index in the list.

            if (weaponManager.ChangeIndex(index))
            {
                InstantiateWeapon();
            }
        }

        public void ChangeWeapon(string weaponName)
        {
            if (weaponManager.ChangeTo(weaponName))
            {
                InstantiateWeapon();
            }
        }

        /// <summary>
        /// Used to take the next weapon.
        /// </summary>
        public void IncrementWeaponIndex() // change to ChangeWeapon()
        {
            if (weaponManager.CurrentIndex == weaponManager.WeaponCount - 1)
            {
                weaponManager.ChangeIndex(0);
            }
            else
            {
                weaponManager.ChangeIndex(weaponManager.CurrentIndex + 1);
            }
        
            fireDelay = new WaitForSeconds(60f / weaponManager.CurrentGunData.fireRate);
        }

        /// <summary>
        /// Used to take the previous weapon.
        /// </summary>
        public void DecrementWeaponIndex() // change to ChangeWeapon()
        {
            if (weaponManager.CurrentIndex == 0)
            {
                weaponManager.ChangeIndex(weaponManager.WeaponCount - 1);
            }
            else
            {
                weaponManager.ChangeIndex(weaponManager.CurrentIndex - 1);
            }
            
            fireDelay = new WaitForSeconds(60f / weaponManager.CurrentGunData.fireRate);
        }

        public void OnLoad(GameData data)
            => this.ChangeWeapon(data.currentWeaponIndex + 1);
        
        private IEnumerator ShootRepeatedly()
        {
            while (isShootingRepeatedly)
            {
                currentWeapon.Shoot();
                
                yield return fireDelay;
            }
        }
        
        private void InstantiateWeapon()
        {
            foreach (Transform t in weaponHolder)
            {
                Destroy(t.gameObject);
            }

            var weapon = Instantiate(weaponManager.CurrentWeaponPrefab, weaponHolder, false);
            currentWeapon = weapon.GetComponent<IWeapon>();

            fireDelay = new WaitForSeconds(60f / weaponManager.CurrentGunData.fireRate);

            isShootingRepeatedly = false;
        }
    }
}
