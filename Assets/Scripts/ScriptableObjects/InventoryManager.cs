using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class InventoryManager : ScriptableObject
{
    private readonly Dictionary<InventoryItemData, InventoryItem> itemDictionary = new ();

    [SerializeField] private UnityEvent onInventoryChangedEvent;

    [field:SerializeField] public List<InventoryItem> Items { get; private set; } = new ();

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

    public InventoryItem Get(InventoryItemData inventoryItemData)
    {
        if (itemDictionary.TryGetValue(inventoryItemData, out var value))
        {
            return value;
        }

        return null;
    }

    public void AddEvent(UnityAction action)
    {
        onInventoryChangedEvent.AddListener(action);
    }
}
