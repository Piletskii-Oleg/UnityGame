using UnityEditor;
using UnityEngine;

namespace Core.DataPersistence.Editor
{
    /// <summary>
    /// Creates custom inspector for <see cref="GameDataManager"/>.
    /// </summary>
    [CustomEditor(typeof(GameDataManager), editorForChildClasses: false)]
    public class DataPersistenceManagerEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Adds Save and Load buttons that call the respective methods from <see cref="GameDataManager"/>.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var manager = target as GameDataManager;
            if (GUILayout.Button("Save"))
            {
                manager.Save();
            }

            if (GUILayout.Button("Load"))
            {
                manager.Load();
            }
        }
    }
}
