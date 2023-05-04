using Interactable;
using Inventory.ScriptableObjects;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// An <see cref="IInteractable"/> object that goes to player's <see cref="Inventory"/> when interacted with.
    /// </summary>
    public class InventoryPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] private string promptMessage;
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private InventoryItemData inventoryItemData;

        /// <summary>
        /// Adds the object to the player's inventory and removes it from the world.
        /// </summary>
        public void Interact()
        {
            int stackSize = inventoryManager.GetStackSize(inventoryItemData);

            if (stackSize < inventoryItemData.maxAmount)
            {
                inventoryManager.Add(inventoryItemData);
                Destroy(gameObject);
            }
        }

        public string GetPromptMessage()
            => promptMessage;
    }
}