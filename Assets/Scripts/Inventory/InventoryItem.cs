using System;
using Inventory.ScriptableObjects;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// Wrap around <see cref="InventoryItemData"/>, used in <see cref="InventoryManager"/>.
    /// </summary>
    [Serializable]
    public class InventoryItem
    {
        [field:SerializeField] public string Name { get; private set; }
        
        [field:SerializeField] public InventoryItemData Data { get; private set; }

        [field: SerializeField] public int StackSize { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItem"/> class.
        /// </summary>
        /// <param name="data"><see cref="InventoryItemData"/> to wrap around.</param>
        /// <param name="count">Initial count of the item.</param>
        public InventoryItem(InventoryItemData data, int count = 1)
        {
            Data = data;
            Name = data.displayName;
            StackSize = count;
        }
        
        public void SetData(InventoryItemData data)
            => Data = data;

        /// <summary>
        /// Increases amount by 1.
        /// </summary>
        public void AddToStack()
            => StackSize++;

        /// <summary>
        /// Decreases amount by 1.
        /// </summary>
        public void RemoveFromStack()
            => StackSize--;

        public void AddToStack(int amount)
            => StackSize += amount;

        public void SetStackSize(int size)
            => StackSize = size;
    }
}
