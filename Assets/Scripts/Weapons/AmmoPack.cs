using Player;
using UnityEngine;
using UnityEngine.Events;
using Weapons.ScriptableObjects;

namespace Weapons
{
    public class AmmoPack : MonoBehaviour
    {
        [Header("Weapon Info")]
        [SerializeField] private GunData gunData;
        [SerializeField] private WeaponManager weaponManager;
        
        [Header("Ammo Pack Info")]
        [SerializeField] private int ammoCount;

        [Header("Events")]
        [SerializeField] private UnityEvent onPickUp;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out _))
            {
                if (weaponManager.TryPickUp(gunData, ammoCount))
                {
                    onPickUp.Invoke();
                    
                    Destroy(gameObject);
                }
            }
        }
    }
}