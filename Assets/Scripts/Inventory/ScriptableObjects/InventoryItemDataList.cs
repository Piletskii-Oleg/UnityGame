using System.Collections.Generic;
using UnityEngine;

namespace Inventory.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data List/Inventory Item Data List")]
    public class InventoryItemDataList : ScriptableObject
    {
        public List<InventoryItemData> itemsList;
    }
}