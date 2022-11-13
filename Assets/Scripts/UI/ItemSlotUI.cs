using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Item slot that stores a single <see cref="InventoryItem"/> in <see cref="InventoryUI"/>.
/// </summary>
public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private GameObject stackBox;
    [SerializeField] private TextMeshProUGUI stackNumber;

    /// <summary>
    /// Sets information about the <see cref="InventoryItem"/> to the <see cref="ItemSlotUI"/>.
    /// </summary>
    /// <param name="item">An <see cref="InventoryItem"/> to set information about.</param>
    public void Set(InventoryItem item)
    {
        itemSprite.sprite = item.Data.itemSprite;
        label.text = item.Data.displayName;
        if (item.StackSize <= 1)
        {
            stackBox.SetActive(false);
            return;
        }

        stackNumber.text = item.StackSize.ToString();
    }
}
