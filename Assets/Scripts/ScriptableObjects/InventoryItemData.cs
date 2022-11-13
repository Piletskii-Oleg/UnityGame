using UnityEngine;

/// <summary>
/// Stores information about an inventory item.
/// </summary>
[CreateAssetMenu]
public class InventoryItemData : ScriptableObject
{
    public string displayName;

    public Vector3 position;

    public GameObject itemPrefab;

    public Sprite itemSprite;
}
