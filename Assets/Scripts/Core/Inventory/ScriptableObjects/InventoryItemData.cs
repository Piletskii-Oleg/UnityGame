using UnityEngine;

namespace Core.Inventory.ScriptableObjects
{
    /// <summary>
    /// Stores information about an inventory item.
    /// </summary>
    [CreateAssetMenu(menuName = "Data/Inventory Item Data")]
    public class InventoryItemData : ScriptableObject
    {
        [Tooltip("Name displayed on the inventory")]
        public string displayName;

        [Tooltip("Sprite of the object in the inventory")]
        public Sprite itemSprite;

        [Tooltip("Can player character hold this in hands and use it?")]
        public bool canHandle;

        [Tooltip("Maximum amount of this object that player can have in inventory")]
        public int maxAmount;
    }
}
