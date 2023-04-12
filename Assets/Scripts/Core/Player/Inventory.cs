using Core.Inventory.ScriptableObjects;
using UnityEngine;

namespace Core.Player
{
    /// <summary>
    /// Helper class for player's <see cref="InventoryManager"/>.
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private InventoryManager inventoryManager;

        private void Start()
        {
         //   inventoryManager.UpdateItemsList();
        }
    }
}
