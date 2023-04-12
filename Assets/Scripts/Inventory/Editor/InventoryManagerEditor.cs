using Inventory.ScriptableObjects;
using UnityEngine;
using UnityEditor;

namespace Inventory.Editor
{
    /// <summary>
    /// Creates custom inspector for <see cref="InventoryManager"/>.
    /// </summary>
    [CustomEditor(typeof(InventoryManager))]
    public class InventoryManagerEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Adds Update Item List button that updates the items visible on the inventory if its contents
        /// were changed using Unity Inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUI.enabled = Application.isPlaying;

            var manager = target as InventoryManager;
            if (GUILayout.Button("Update Item List"))
            {
                manager.UpdateItemsList();
            }
        }
    }
}