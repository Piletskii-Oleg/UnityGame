using ScriptableObjects.Inventory;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// User interface for the player's <see cref="InventoryManager"/>.
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private InventoryManager manager;

        [SerializeField] private GameObject itemSlotPrefab;
        [SerializeField] private Transform parentObject;
        [SerializeField] private Transform bigInventory;

        private bool isInventoryClosed;

        private void Start()
        {
            isInventoryClosed = true;
        }

        public void HandleInventory()
        {
            if (isInventoryClosed)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }

        private void OpenInventory()
        {
            bigInventory.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;

            isInventoryClosed = false;
        }

        private void CloseInventory()
        {
            bigInventory.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            isInventoryClosed = true;
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
