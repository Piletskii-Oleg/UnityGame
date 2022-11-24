using UnityEditor;
using UnityEngine;

namespace DataPersistence.Editor
{
    [CustomEditor(typeof(DataPersistenceManager), editorForChildClasses: false)]
    public class DataPersistenceManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var manager = target as DataPersistenceManager;
            if (GUILayout.Button("Save"))
            {
                manager.SaveGame();
            }

            if (GUILayout.Button("Load"))
            {
                manager.LoadGame();
            }
        }
    }
}
