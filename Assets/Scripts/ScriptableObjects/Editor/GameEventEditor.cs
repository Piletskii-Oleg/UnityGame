using UnityEngine;
using UnityEditor;

/// <summary>
/// Changes the editor UI of <see cref="GameEvent"/> scriptable objects.
/// </summary>
[CustomEditor(typeof(GameEvent), editorForChildClasses: true)]
public class GameEventEditor : Editor
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
