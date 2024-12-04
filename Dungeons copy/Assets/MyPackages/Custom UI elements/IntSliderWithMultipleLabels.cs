using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof( Slider))]
public class IntSliderWithMultipleLabels : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] GameObject _labelPrefab=default;
    [SerializeField] float _yLabelOffset = 18f;
    [SerializeField] List<TMP_Text> _labels = new List<TMP_Text>();
    [SerializeField,HideInInspector] private float _handleWidth = 10f;
    // slider width, minus x for handle width
    // e.g 160 -10 for left and -10 for right is 140
    // divid it by 2
    // then divide by max value - 1 
    // e.g 140 with max 7, then separation is by 140/(6)=23.(3)
    // 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnValidate()
    {
        //if (_labels.Count == 0) return;
        //float width = (_slider.GetComponent<RectTransform>().rect.width - _handleWidth);
        //int steps = ((int)_slider.maxValue) - ((int)_slider.minValue);
        //float step = width/ steps;
        ////_labels[0].transform.position = 
        //_labels[0].GetComponent<RectTransform>().anchoredPosition= new Vector3(-width/2, _yLabelOffset, _labels[0].transform.position.z);
        //_labels[_labels.Count - 1].GetComponent<RectTransform>().anchoredPosition = new Vector3(width/2, _yLabelOffset, _labels[0].transform.position.z);
        //for (int i = 1; i < steps; i++)
        //{
        //    _labels[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(step * i -width/2, _yLabelOffset, _labels[i].transform.position.z);
        //}
        //for (int i = steps / 2 +1; i < steps; i++)
        //{
        //    _labels[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(step * (i- (steps / 2)), _yLabelOffset, _labels[i].transform.position.z);
        //}
    }
    private void Reset()
    {
        _slider=GetComponent<Slider>();
        _handleWidth = _slider.handleRect.rect.width;
        float width = (_slider.GetComponent<RectTransform>().rect.width - _handleWidth);
        GameObject label1 = Instantiate(_labelPrefab, _slider.transform);
        label1.GetComponent<TMP_Text>().text = "N";
        label1.GetComponent<RectTransform>().anchoredPosition = new Vector3(-width / 2, _yLabelOffset, label1.transform.position.z);
        GameObject label2 = Instantiate(_labelPrefab, _slider.transform);
        label2.GetComponent<TMP_Text>().text = "M";
        label2.GetComponent<RectTransform>().anchoredPosition = new Vector3(width / 2, _yLabelOffset, label2.transform.position.z);
        _labels.Add(label1.GetComponent<TMP_Text>());
        _labels.Add(label2.GetComponent<TMP_Text>());

    }
}
