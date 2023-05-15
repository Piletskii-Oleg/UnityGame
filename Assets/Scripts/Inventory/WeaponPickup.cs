using Interactable;
using Inventory.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class WeaponPickup : InventoryPickup, IInteractable
    {
        [Header("Ammo")]
        [SerializeField] private int ammoCount;
        [SerializeField] private InventoryItemData ammoType;
        
        [SerializeField] private UnityEvent onPickup;
        
        public new void Interact()
        {
            int stackSize = inventoryManager.GetStackSize(inventoryItemData);

            if (stackSize < inventoryItemData.maxAmount)
            {
                inventoryManager.Add(inventoryItemData);
                inventoryManager.Add(ammoType, ammoCount);
                
                onPickup.Invoke();
                
                Destroy(gameObject);
            }
        }
    }
}