using Inventory.ScriptableObjects;
using UnityEngine;
using UnityEditor;

namespace Inventory.Editor
{
    [CustomEditor(typeof(InventoryManager))]
    public class InventoryManagerEditor : UnityEditor.Editor
    {
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