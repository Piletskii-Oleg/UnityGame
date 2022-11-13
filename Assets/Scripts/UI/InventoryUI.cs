using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// User interface for the player's <see cref="InventoryManager"/>.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryManager manager;

    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Transform parentObject;

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
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;

        isInventoryClosed = false;
    }

    private void CloseInventory()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

        isInventoryClosed = true;
    }

    /// <summary>
    /// Updates the player's <see cref="InventoryManager"/> UI.
    /// </summary>
    public void UpdateInventory()
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
            AddInventorySlot(item);
        }
    }

    private void AddInventorySlot(InventoryItem item)
    {
        var slotObject = Instantiate(itemSlotPrefab);
        slotObject.transform.SetParent(parentObject, false);

        var slot = slotObject.GetComponent<ItemSlotUI>();
        slot.Set(item);
    }
}
