using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryManager manager;
    [SerializeField] private GameObject itemSlotPrefab;

    public void UpdateInventory()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    private void DrawInventory()
    {
        foreach (var item in manager.Items)
        {
            AddInventorySlot(item);
        }
    }

    private void AddInventorySlot(InventoryItem item)
    {
        var slotObject = Instantiate(itemSlotPrefab);
        slotObject.transform.SetParent(transform, false);

        var slot = slotObject.GetComponent<ItemSlotUI>();
        slot.Set(item);
    }
}
