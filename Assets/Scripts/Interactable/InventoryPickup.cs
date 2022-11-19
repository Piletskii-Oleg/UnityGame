using ScriptableObjects.Inventory;
using UnityEngine;

namespace Interactable
{
    /// <summary>
    /// An <see cref="IInteractable"/> object that goes to player's <see cref="Inventory"/> when interacted with.
    /// </summary>
    public class InventoryPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private InventoryItemData inventoryItemData;

        /// <summary>
        /// Adds the object to the player's inventory and removes it from the world.
        /// </summary>
        public void Interact()
        {
            var stackSize = inventoryManager.GetStackSize(inventoryItemData);

            if (stackSize < inventoryItemData.maxAmount)
            {
                inventoryManager.Add(inventoryItemData);
                Destroy(gameObject);
            }
        }
    }
}