using Inventory.ScriptableObjects;
using UnityEngine;
using Weapons.ScriptableObjects;

namespace Player
{
    /// <summary>
    /// Helper class for player's <see cref="InventoryManager"/>.
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private WeaponManager weaponManager;

        private void Start()
        {
            inventoryManager.UpdateItemsList();
        }

        public void UpdateAmmoCount()
        {
            foreach (var inventoryItem in inventoryManager.Items)
            {
                if (inventoryItem.Name.Contains("Ammo"))
                {
                    var weaponItem = weaponManager.GetWeaponItem(inventoryItem.Name.Replace("Ammo", ""));
                    
                    inventoryItem.SetStackSize(weaponItem.Data.currentTotalAmmo);
                    
                    inventoryManager.UpdateItemsList();
                }
            }
        }
    }
}
