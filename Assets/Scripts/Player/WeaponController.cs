using System.Collections;
using UnityEngine;
using Weapons;
using Weapons.ScriptableObjects;

namespace Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private Transform weaponHolder;

        private IWeapon currentWeapon;
        
        private Coroutine shootRepeatedlyCoroutine;
        private bool isShooting;
        private WaitForSeconds fireDelay;

        private void Start()
        {
            if (currentWeapon != null)
            {
                this.ChangeWeapon(1);
            }
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
                isShooting = true;

                shootRepeatedlyCoroutine = StartCoroutine(ShootRepeatedly());
            }
            else
            {
                currentWeapon.Shoot();
            }
        }

        /// <summary>
        /// Makes the player stop shooting if the weapon is automatic with the currently held weapon.
        /// </summary>
        public void StopShooting()
        {
            isShooting = false;
            if (shootRepeatedlyCoroutine != null)
            {
                StopCoroutine(shootRepeatedlyCoroutine);
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
        public void IncrementWeaponIndex()
        {
            if (weaponManager.CurrentIndex == weaponManager.WeaponCount - 1)
            {
                weaponManager.ChangeIndex(0);
            }
            else
            {
                weaponManager.ChangeIndex(weaponManager.CurrentIndex + 1);
            }
        
            fireDelay = new WaitForSeconds(1f / weaponManager.CurrentGunData.fireRate);
        }

        /// <summary>
        /// Used to take the previous weapon.
        /// </summary>
        public void DecrementWeaponIndex()
        {
            if (weaponManager.CurrentIndex == 0)
            {
                weaponManager.ChangeIndex(weaponManager.WeaponCount - 1);
            }
            else
            {
                weaponManager.ChangeIndex(weaponManager.CurrentIndex - 1);
            }
            
            fireDelay = new WaitForSeconds(1f / weaponManager.CurrentGunData.fireRate);
        }
        
        private IEnumerator ShootRepeatedly()
        {
            while (isShooting)
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

            fireDelay = new WaitForSeconds(1f / weaponManager.CurrentGunData.fireRate);
        }
    }
}
