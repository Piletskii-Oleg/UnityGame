using Core.EventListeners.GameEvents;
using UnityEditor;
using UnityEngine;

namespace Functionality.EventListeners.Editor
{
    /// <summary>
    /// Changes the editor UI of <see cref="BaseGameEvent{T}"/> scriptable objects.
    /// </summary>
    [CustomEditor(typeof(BaseGameEvent<string>), editorForChildClasses: true)]
    public class StringGameEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var gameEvent = target as GameEvent;
            if (GUILayout.Button("Raise"))
            {
                gameEvent.Raise();
            }
        }
    }
}