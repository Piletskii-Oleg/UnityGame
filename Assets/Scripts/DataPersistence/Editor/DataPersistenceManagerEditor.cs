using UnityEditor;
using UnityEngine;

namespace DataPersistence.Editor
{
    /// <summary>
    /// Creates custom inspector for <see cref="DataPersistenceManager"/>.
    /// </summary>
    [CustomEditor(typeof(DataPersistenceManager), editorForChildClasses: false)]
    public class DataPersistenceManagerEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Adds Save and Load buttons that call the respective methods from <see cref="DataPersistenceManager"/>.
        /// </summary>
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
