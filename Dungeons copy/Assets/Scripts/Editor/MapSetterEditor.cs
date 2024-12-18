using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMapSetter))]
public class MapSetterEditor: Editor
{
    TileMapSetter _mapSetter;
    SerializedProperty _materialBlock;
    SerializedProperty _renderer;
    SerializedProperty _horizontalSize;
    SerializedProperty _verticalSize;
    int _previousHorizontalSize;
    int _previousVerticalSize;
    private void OnEnable()
    {
        _mapSetter = target as TileMapSetter;
        _materialBlock = serializedObject.FindProperty("_materialBlock");
        _renderer = serializedObject.FindProperty("_renderer");
        _horizontalSize = serializedObject.FindProperty("_horizontalSize");
        _verticalSize = serializedObject.FindProperty("_verticalSize");
    }
    public override void OnInspectorGUI()
    {
        _previousHorizontalSize = _horizontalSize.intValue;
        _previousVerticalSize = _verticalSize.intValue;
        base.OnInspectorGUI();
        serializedObject.Update();
        if (_previousHorizontalSize != _horizontalSize.intValue)
        {
            _mapSetter.UpdatGridHorizontally(_horizontalSize.intValue);
        }
        if(_previousVerticalSize != _verticalSize.intValue) 
        {
            _mapSetter.UpdatGridVertically(_verticalSize.intValue);
        }
        serializedObject.ApplyModifiedProperties();
    }
}