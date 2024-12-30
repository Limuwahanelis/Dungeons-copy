using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DungeonBlockPlacer))]
public class DungeonBlockPlacerEditor : Editor
{
    SerializedProperty _blocks;
    List<ListOfGameObjects> _copyOfBlocks;
    int _previousBlocksSize;
    private void OnEnable()
    {
        _blocks = serializedObject.FindProperty("_blocks");
        _copyOfBlocks = new List<ListOfGameObjects>();
        for (int i = 0; i < _blocks.arraySize; i++)
        {
            _copyOfBlocks.Add(_blocks.GetArrayElementAtIndex(i).managedReferenceValue as ListOfGameObjects);
        }
        // _blocks.managedReferenceValue as List<ListOfGameObjects>;
    }
    public override void OnInspectorGUI()
    {

        _previousBlocksSize = _blocks.arraySize;
        base.OnInspectorGUI();
        serializedObject.Update();
        if (_blocks.arraySize == 0)
        {
            for (int i = _previousBlocksSize - 1; i >= 0; i--)
            {
                ListOfGameObjects list = _copyOfBlocks[i];
                for (int j = list.gameObjects.Count - 1; j >= 0; j--)
                {
                    DestroyImmediate(list.gameObjects[j]);
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}