using UnityEngine;

/// <summary>
/// Helper class for player's <see cref="InventoryManager"/>.
/// </summary>
public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private GameEvent onInventoryChanged;

    private void Start()
    {
        onInventoryChanged.Raise();
    }
}
