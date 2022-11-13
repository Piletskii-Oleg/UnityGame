using UnityEngine;

/// <summary>
/// Stores information about an inventory item.
/// </summary>
[CreateAssetMenu]
public class InventoryItemData : ScriptableObject
{
    [Tooltip("Name displayed on the inventory")]
    public string displayName;
    
    public Vector3 position;

    public GameObject itemPrefab;

    [Tooltip("Sprite of the object in the inventory")]
    public Sprite itemSprite;

    [Tooltip("Can player character hold this in hands and use it?")]
    public bool canHandle;
}
