using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static Unity.Burst.Intrinsics.X86.Avx;
using Unity.Mathematics;

[CustomEditor(typeof(HealthBarUI))]
public class HealthBarUIEditor: Editor
{
    SerializedProperty _currentValue;
    SerializedProperty _maxValue;
    SerializedProperty _bar;
    SerializedProperty _fill;
    private Vector2 tmp;
    private Vector2 tmp2;
    private RectTransform _barRectTransform;
    private RectTransform _fillRectTransform;
    private void OnEnable()
    {
        _currentValue = serializedObject.FindProperty("_currentValue");
        _maxValue = serializedObject.FindProperty("_maxValue");
        _bar = serializedObject.FindProperty("_bar");
        _fill = serializedObject.FindProperty("_fill");
        _barRectTransform = ((RectTransform)(_bar.objectReferenceValue));
        _fillRectTransform = ((RectTransform)(_fill.objectReferenceValue));
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        if(GUI.changed)
        {
            UpdateFill();
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void UpdateFill()
    {
        _currentValue.intValue = math.clamp(_currentValue.intValue, 0, _maxValue.intValue);
        float pct = _currentValue.intValue / (float)_maxValue.intValue;
        float value = math.remap(0, 1, -_barRectTransform.sizeDelta.x / 2, 0, pct);
        tmp = _fillRectTransform.anchoredPosition;
        tmp2 = _fillRectTransform.sizeDelta;
        tmp.x = value;
        tmp2.x = value * 2;
        _fillRectTransform.anchoredPosition = tmp;
        _fillRectTransform.sizeDelta = tmp2;
    }
}