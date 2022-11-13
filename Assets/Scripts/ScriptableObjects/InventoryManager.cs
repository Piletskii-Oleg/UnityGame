using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Player's inventory manager.
/// </summary>
[CreateAssetMenu]
public class InventoryManager : ScriptableObject
{
    private readonly Dictionary<InventoryItemData, InventoryItem> itemDictionary = new ();

    [SerializeField] private UnityEvent onInventoryChangedEvent;

    /// <summary>
    /// Gets list of <see cref="InventoryItem"/> stored in the inventory.
    /// </summary>
    [field:SerializeField] public List<InventoryItem> Items { get; private set; } = new ();

    /// <summary>
    /// Adds an item to the inventory using <paramref name="inventoryItemData"/>.
    /// </summary>
    /// <param name="inventoryItemData"><see cref="ScriptableObject"/> data that corresponds to the item.</param>
    public void Add(InventoryItemData inventoryItemData)
    {
        if (itemDictionary.TryGetValue(inventoryItemData, out var value))
        {
            value.AddToStack();
        }
        else
        {
            var newItem = new InventoryItem(inventoryItemData);
            Items.Add(newItem);
            itemDictionary.Add(inventoryItemData, newItem);
        }

        onInventoryChangedEvent.Invoke();
    }

    /// <summary>
    /// Removes an item to the inventory using <paramref name="inventoryItemData"/>.
    /// </summary>
    /// <param name="inventoryItemData"><see cref="ScriptableObject"/> data that corresponds to the item.</param>
    public void Remove(InventoryItemData inventoryItemData)
    {
        if (itemDictionary.TryGetValue(inventoryItemData, out var value))
        {
            value.RemoveFromStack();

            if (value.StackSize == 0)
            {
                Items.Remove(value);
                itemDictionary.Remove(inventoryItemData);
            }

            onInventoryChangedEvent.Invoke();
        }
    }

    /// <summary>
    /// Gets <see cref="InventoryItem"/> stored in the inventory based on <paramref name="inventoryItemData"/>.
    /// </summary>
    /// <param name="inventoryItemData"><see cref="ScriptableObject"/> data that corresponds to the item.</param>
    /// <returns>
    /// An <see cref="InventoryItem"/> that corresponds to the <paramref name="inventoryItemData"/>.
    /// Null if no appropriate <see cref="InventoryItem"/> was found.
    /// </returns>
    public InventoryItem GetItem(InventoryItemData inventoryItemData)
    {
        if (itemDictionary.TryGetValue(inventoryItemData, out var value))
        {
            return value;
        }

        return null;
    }
}
