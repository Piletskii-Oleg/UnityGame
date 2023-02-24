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
        public InventoryItem(InventoryItemData data)
        {
            Data = data;
            Name = data.name;
            AddToStack();
        }
        
        public void SetData(InventoryItemData data)
        {
            this.Data = data;
        }

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
    }
}
