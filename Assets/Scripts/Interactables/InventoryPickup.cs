using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickup : Interactable
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private InventoryItemData inventoryItemData;

    protected override void Interact()
    {
        base.Interact();

        inventoryManager.Add(inventoryItemData);
    }
}
