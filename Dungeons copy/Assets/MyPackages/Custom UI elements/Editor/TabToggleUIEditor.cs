using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(TabToggleUI))]
public class TabToggleUIEditor: ToggleEditor
{
    SerializedProperty _notSelectedGraphic;
    SerializedProperty _selectedGraphic;
    protected override void OnEnable()
    {
        _notSelectedGraphic = serializedObject.FindProperty("_notSelectedTargetGraphic");
        _selectedGraphic = serializedObject.FindProperty("_selectedTargetGraphic");
        base.OnEnable();
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_notSelectedGraphic);
        EditorGUILayout.PropertyField(_selectedGraphic);
        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
    }
}