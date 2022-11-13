using UnityEngine;

[CreateAssetMenu]
public class InventoryItemData : ScriptableObject
{
    public string displayName;

    public Vector3 position;

    public GameObject itemPrefab;

    public Sprite itemSprite;
}
