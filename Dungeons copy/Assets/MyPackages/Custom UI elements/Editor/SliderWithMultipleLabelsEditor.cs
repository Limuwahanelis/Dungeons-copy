using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

[CustomEditor(typeof(IntSliderWithMultipleLabels))]
public class SliderWithMultipleLabelsEditor: Editor
{
    SerializedProperty _slider;
    SerializedProperty _labels;
    SerializedProperty _labelPrefab;
    SerializedProperty _labelYOffset;
    SerializedProperty _handleWidth;
    bool _wholeNumbers;
    //int _maxValue;
    Slider _sliderS;
    private void OnEnable()
    {
        _labelPrefab = serializedObject.FindProperty("_labelPrefab");
        _slider = serializedObject.FindProperty("_slider");
        _labels = serializedObject.FindProperty("_labels");
        _labelYOffset = serializedObject.FindProperty("_yLabelOffset");
        _handleWidth = serializedObject.FindProperty("_handleWidth");
        _wholeNumbers = (_slider.objectReferenceValue as Slider).wholeNumbers;
        _sliderS = _slider.objectReferenceValue as Slider;
        GameObject aa;

    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        
        float width = ((_slider.objectReferenceValue as Slider).GetComponent<RectTransform>().rect.width - _handleWidth.floatValue);
        int steps = ((int)_sliderS.maxValue) - ((int)_sliderS.minValue);
        float step = width / steps;
        if (_wholeNumbers)
        {
            if(_labels.arraySize< steps+1)
            {
                for (int i = _labels.arraySize; i < steps+1; i++)
                {
                    _labels.InsertArrayElementAtIndex(i);
                    TMP_Text label = Instantiate(_labelPrefab.objectReferenceValue as GameObject, (_slider.objectReferenceValue as Slider).transform).GetComponent<TMP_Text>();
                    label.text = i.ToString();
                    _labels.GetArrayElementAtIndex(i).objectReferenceValue = label;
                }
                for(int i=1;i<_labels.arraySize;i++)
                {
                    TMP_Text label = _labels.GetArrayElementAtIndex(i).objectReferenceValue as TMP_Text;
                    label.GetComponent<RectTransform>().anchoredPosition = new Vector3(step * i - width / 2, _labelYOffset.floatValue, label.transform.position.z);
                    label.text = i.ToString();
                    _labels.GetArrayElementAtIndex(i).objectReferenceValue = label;
                }
            }
            else if (_labels.arraySize> steps + 1)
            {
                for (int i = _labels.arraySize-1; i >= steps + 1; i--)
                {
                     
                    TMP_Text label = _labels.GetArrayElementAtIndex(i).objectReferenceValue as TMP_Text;
                    DestroyImmediate(label.gameObject);
                    _labels.DeleteArrayElementAtIndex(i);
                }
                for(int i=0;i< steps + 1; i++)
                {
                    TMP_Text label = _labels.GetArrayElementAtIndex(i).objectReferenceValue as TMP_Text;
                    label.GetComponent<RectTransform>().anchoredPosition = new Vector3(step * i - width / 2, _labelYOffset.floatValue, label.transform.position.z);
                    _labels.GetArrayElementAtIndex(i).objectReferenceValue = label;
                    label.text = i.ToString();
                    _labels.GetArrayElementAtIndex(i).objectReferenceValue = label;
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

}