using System.Collections.Generic;
using Core.DataPersistence;
using Core.DataPersistence.DataFiles;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Inventory.ScriptableObjects
{
    /// <summary>
    /// Player's inventory manager.
    /// </summary>
    [CreateAssetMenu(fileName = "Inventory Manager", menuName = "Managers/Inventory Manager")]
    public class InventoryManager : DataManager<GameData>
    {
        private readonly Dictionary<InventoryItemData, InventoryItem> itemDictionary = new ();

        [SerializeField] private UnityEvent onInventoryChangedEvent;
        [SerializeField] private UnityEvent<string> onItemChosenEvent;
        
        [SerializeField] private List<InventoryItem> items = new ();

        [SerializeField] private InventoryItemDataList allItemsList;
        
        /// <summary>
        /// Gets read-only copy of the list list of <see cref="InventoryItem"/> stored in the inventory.
        /// </summary>
        public IEnumerable<InventoryItem> Items => items;

        /// <summary>
        /// Updates item dictionary so that its contents are same as <see cref="Items"/>.
        /// </summary>
        public void UpdateItemsList()
        {
            itemDictionary.Clear();
            foreach (var item in Items)
            {
                itemDictionary.Add(item.Data, item);
            }

            onInventoryChangedEvent.Invoke();
        }

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
                items.Add(newItem);
                itemDictionary.Add(inventoryItemData, newItem);

                if (inventoryItemData.canHandle)
                {
                    onItemChosenEvent.Invoke(inventoryItemData.displayName);
                }
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
                    items.Remove(value);
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
            if (itemDictionary.TryGetValue(inventoryItemData, out var inventoryItem))
            {
                return inventoryItem;
            }

            return null;
        }

        /// <summary>
        /// Gets stack size of <see cref="InventoryItem"/> stored in the inventory based on <paramref name="inventoryItemData"/>.
        /// </summary>
        /// <param name="inventoryItemData"><see cref="ScriptableObject"/> data that corresponds to the item.</param>
        /// <returns>Amount of said item in the inventory.</returns>
        public int GetStackSize(InventoryItemData inventoryItemData)
        {
            if (itemDictionary.TryGetValue(inventoryItemData, out var inventoryItem))
            {
                return inventoryItem.StackSize;
            }

            return 0;
        }

        public override void SaveData(GameData data)
            => data.storedItems = items;

        public override void LoadData(GameData data)
        {
            items = data.storedItems;
            foreach (var item in items)
            {
                var foundData = allItemsList.itemsList.Find(value => value.displayName == item.Name);
                item.SetData(foundData);
            }
            
            this.UpdateItemsList();
        }
    }
}
