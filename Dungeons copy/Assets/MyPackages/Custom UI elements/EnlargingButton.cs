using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnlargingButton :Selectable,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] Vector3 _bigScale=Vector3.one;
    [SerializeField] float _enlargeSpeed=1;
    [SerializeField] float _shrinkSpeed=1;
    public UnityEvent onClick;
    Vector3 _scale;
    Vector3 _orignalScale;
    private float _time;
    private Coroutine _cor;
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        StartEnlargeCor();
    }
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        StartShrinkCor();
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        StartEnlargeCor();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        StartShrinkCor();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
    protected override void Reset()
    {
        base.Reset();
        transition=Transition.None;
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null) _rectTransform = rectTransform;
    }
    protected override void Start()
    {
        _orignalScale = _rectTransform.localScale;
    }
    private void StartEnlargeCor()
    {
        if (_cor != null)
        {
            StopCoroutine(_cor);
        }
        _cor = StartCoroutine(EnlargeButton());
    }
    private void StartShrinkCor()
    {
        if (_cor != null)
        {
            StopCoroutine(_cor);
        }
        _cor = StartCoroutine(ShrinkButton());
    }
    private IEnumerator EnlargeButton()
    {
        while (_scale.x < _bigScale.x)
        {
            _time += Time.deltaTime * _enlargeSpeed;
            _time = Mathf.Clamp(_time, 0, 1);
            _scale = Vector3.Lerp(_orignalScale, _bigScale, _time);
            _rectTransform.localScale = _scale;
            yield return null;
        }
    }
    private IEnumerator ShrinkButton()
    {
        while (_scale.x > _orignalScale.x)
        {
            _time -= Time.deltaTime * _shrinkSpeed;
            _time = Mathf.Clamp(_time, 0, 1);
            _scale = Vector3.Lerp(_orignalScale, _bigScale, _time);
            _rectTransform.localScale = _scale;
            yield return null;
        }
    }


}
