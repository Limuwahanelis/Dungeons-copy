using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomizablePlatform))]
public class CustomizablePlatformEditor : Editor
{
    CustomizablePlatform platform;
    SerializedProperty platformX;
    private void OnEnable()
    {
        platform = target as CustomizablePlatform;
        platformX = serializedObject.FindProperty("platformX");
    }

    public override void OnInspectorGUI()
    {
        int previousValue = platformX.intValue;
        base.OnInspectorGUI();
        serializedObject.Update();
        if(previousValue != platformX.intValue)
        {
            platform.UpdateSprite();
        }
    }
}
