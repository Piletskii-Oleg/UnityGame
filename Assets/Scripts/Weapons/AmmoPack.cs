using Inventory.ScriptableObjects;
using Player;
using UnityEngine;
using UnityEngine.Events;
using Weapons.ScriptableObjects;

namespace Weapons
{
    public class AmmoPack : MonoBehaviour
    {
        [Header("Inventory Info")]
        [SerializeField] private InventoryItemData itemData;
        [SerializeField] private InventoryManager inventoryManager;

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
                if (weaponManager.TryPickUpAmmo(gunData, ammoCount))
                {
                    var item = inventoryManager.GetItem(itemData);
                    
                    if (item == null)
                    {
                        inventoryManager.Add(itemData, gunData.currentTotalAmmo);
                    }
                    else
                    {
                        item.SetStackSize(gunData.currentTotalAmmo);
                        
                        inventoryManager.UpdateItemsList();
                    }

                    onPickUp.Invoke();
                    
                    Destroy(gameObject);
                }
            }
        }
    }
}