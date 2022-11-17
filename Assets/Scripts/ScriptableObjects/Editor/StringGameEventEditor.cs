using UnityEngine;
using UnityEditor;

/// <summary>
/// Changes the editor UI of <see cref="BaseGameEvent{T}"/> scriptable objects.
/// </summary>
[CustomEditor(typeof(BaseGameEvent<string>), editorForChildClasses: true)]
public class StringGameEventEditor : Editor
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