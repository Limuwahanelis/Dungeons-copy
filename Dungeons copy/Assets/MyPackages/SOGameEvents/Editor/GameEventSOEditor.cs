using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEventSO))]
public class GameEventSOEditor: Editor
{
    private void OnEnable()
    {

    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Raise event"))
        {
            (target as GameEventSO).Raise();
        }
    }
}