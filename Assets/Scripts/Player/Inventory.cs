using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private GameEvent onInventoryChanged;

    private void Start()
    {
        onInventoryChanged.Raise();
    }
}
