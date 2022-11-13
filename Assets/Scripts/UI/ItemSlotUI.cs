using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private GameObject stackBox;
    [SerializeField] private TextMeshProUGUI stackNumber;

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
