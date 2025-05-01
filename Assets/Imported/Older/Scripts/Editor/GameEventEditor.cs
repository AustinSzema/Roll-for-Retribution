using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// TODO: Clean up comments in the GameEvent script.

[CustomEditor(typeof(GameEvent))]
[CanEditMultipleObjects]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameEvent gameEvent = target as GameEvent;

        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script",
            MonoScript.FromScriptableObject((ScriptableObject)target),
            GetType(), false);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Testing", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Use this button to manually raise this " +
            "event while in Play Mode.");
        if (GUILayout.Button("Raise"))
        {
            if (Application.isPlaying)
            {
                gameEvent.Raise();
            }
        }
    }
}
