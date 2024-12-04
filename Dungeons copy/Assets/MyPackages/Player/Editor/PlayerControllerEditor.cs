using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor: Editor
{
     SerializedProperty _initialStateType;
     [SerializeField]int _selected = 0;
     List<string> _options=new List<string>();
    List<Type> states;
    private void OnEnable()
    {
        states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(domainAssembly => domainAssembly.GetTypes())
    .Where(type => typeof(PlayerState).IsAssignableFrom(type) && !type.IsAbstract).ToArray().ToList();
        foreach (var state in states)
        {
            _options.Add(state.ToString());
        }
        _initialStateType = serializedObject.FindProperty("_initialStateType");
        _selected = _options.IndexOf(_initialStateType.stringValue);
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        _selected = EditorGUILayout.Popup("Player initial state", _selected, _options.ToArray());

        if (EditorGUI.EndChangeCheck())
        {
            Debug.Log(_options[_selected]);
            _initialStateType.stringValue = states[_selected].ToString();
        }
        serializedObject.ApplyModifiedProperties();
    }
}