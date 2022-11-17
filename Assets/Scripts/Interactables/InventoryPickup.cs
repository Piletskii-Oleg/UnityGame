using UnityEngine;

/// <summary>
/// An <see cref="Interactable"/> object that goes to player's <see cref="Inventory"/> when interacted with.
/// </summary>
public class InventoryPickup : Interactable
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private InventoryItemData inventoryItemData;

    /// <summary>
    /// Adds the object to the player's inventory and removes it from the world.
    /// </summary>
    protected override void Interact()
    {
        var stackSize = inventoryManager.GetItem(inventoryItemData).StackSize;

        if (stackSize < inventoryItemData.maxAmount)
        {
            inventoryManager.Add(inventoryItemData);

            Destroy(gameObject);
        }
    }
}
