using Core.Inventory;
using Core.Inventory.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Functionality.UI
{
    /// <summary>
    /// Item slot that stores a single <see cref="InventoryItem"/> in <see cref="InventoryUI"/>.
    /// </summary>
    public class ItemSlotUI : MonoBehaviour
    {
        [Header("UI information")]
        [SerializeField] private Image itemSprite;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private GameObject stackBox;
        [SerializeField] private TextMeshProUGUI stackNumber;

        [Header("Item data")]
        [SerializeField] private InventoryItemData data;

        [Header("Events")]
        [SerializeField] private UnityEvent<string> onItemChosen;

        /// <summary>
        /// Sets information about the <see cref="InventoryItem"/> to the <see cref="ItemSlotUI"/>.
        /// </summary>
        /// <param name="item">An <see cref="InventoryItem"/> to set information about.</param>
        public void Set(InventoryItem item)
        {
            data = item.Data;
            itemSprite.sprite = data.itemSprite;
            label.text = data.displayName;
            if (item.StackSize <= 1)
            {
                stackBox.SetActive(false);
                return;
            }

            stackNumber.text = item.StackSize.ToString();
        }

        public void OnClick()
        {
            if (data.canHandle)
            {
                onItemChosen.Invoke(data.displayName);
            }
        }
    }
}
