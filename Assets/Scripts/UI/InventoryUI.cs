using Inventory.ScriptableObjects;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// User interface for the player's <see cref="InventoryManager"/>.
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        [Header("Inventory Manager")]
        [SerializeField] private InventoryManager manager;

        [Header("On-Scene Objects")]
        [SerializeField] private GameObject itemSlotPrefab;
        [SerializeField] private Transform parentObject;
        [SerializeField] private Transform bigInventory;

        private bool isInventoryOpen;

        public void HandleInventory()
        {
            if (isInventoryOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }

        private void OpenInventory()
        {
            bigInventory.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;

            isInventoryOpen = true;
        }

        private void CloseInventory()
        {
            bigInventory.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            isInventoryOpen = false;
        }

        /// <summary>
        /// Updates the player's <see cref="InventoryManager"/> UI.
        /// </summary>
        public void UpdateInventoryUI()
        {
            foreach (Transform t in parentObject)
            {
                Destroy(t.gameObject);
            }

            DrawInventory();
        }

        private void DrawInventory()
        {
            foreach (var item in manager.Items)
            {
                var slotObject = Instantiate(itemSlotPrefab, parentObject, false);
                var slot = slotObject.GetComponent<ItemSlotUI>();
                slot.Set(item);
            }
        }
    }
}
