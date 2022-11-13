using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    [field:SerializeField] public InventoryItemData Data { get; private set; }
    [field: SerializeField] public int StackSize { get; private set; }

    public InventoryItem(InventoryItemData data)
    {
        Data = data;
        AddToStack();
    }

    public void AddToStack()
        => StackSize++;

    public void RemoveFromStack()
        => StackSize--;
}
