using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparateSelector : MonoBehaviour
{
    public Action<Vector2, Vector2> OnSelectionChangedWorld;
    public Action<Vector2, Vector2> OnSelectionChangedViewport;
    [SerializeField]
    private Camera _cam;
    private Vector2 _lastMouseScreenPos;
    private Vector2 _mouseHoldStartScreenPos;
    private Vector3 _selectionStart;
    private Vector3 _selectionEnd;
    private Vector2 _mouseScreenPos;
    private Vector3 _minScreenPosToConvert;
    private Vector3 _maxScreenPosToConvert;
    private bool _isHoldingMouse = false;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _lastMouseScreenPos.x = 0;
        _lastMouseScreenPos.y = 0;
    }
    // TODO: include screen shift after starting selection
    // Update is called once per frame
    void Update()
    {
        if (!_isHoldingMouse) return;
        MakeSSelection();
    }
    private void MakeSSelection()
    {
        if (_lastMouseScreenPos.x != _mouseScreenPos.x || _mouseScreenPos.y != _lastMouseScreenPos.y)
        {

            float minX;
            float minY;
            float maxX;
            float maxY;
            HelperClass.SetMinMaxValues(_mouseScreenPos.x, _mouseHoldStartScreenPos.x, out minX, out maxX);
            HelperClass.SetMinMaxValues(_mouseScreenPos.y, _mouseHoldStartScreenPos.y, out minY, out maxY);
            float index = minX;
            float index2 = minY;
            _minScreenPosToConvert.x = minX;
            _minScreenPosToConvert.y = minY;
            _minScreenPosToConvert.z = _cam.nearClipPlane;
            _maxScreenPosToConvert.x = maxX;
            _maxScreenPosToConvert.y = maxY;
            _maxScreenPosToConvert.z = _cam.nearClipPlane;
            _selectionStart = _cam.ScreenToWorldPoint(_minScreenPosToConvert);
            _selectionEnd = _cam.ScreenToWorldPoint(_maxScreenPosToConvert);
            Logger.Log($"Range x: {minX} {maxX} y: {minY} {maxY}");
            Logger.Log($"World pos min point: {_selectionStart} max: {_selectionEnd}");
            OnSelectionChangedWorld?.Invoke(_selectionStart, _selectionEnd);
            _selectionStart = _cam.ScreenToViewportPoint(_minScreenPosToConvert);
            _selectionEnd= _cam.ScreenToViewportPoint( _maxScreenPosToConvert);
            Logger.Log($"Viewport pos min:{_selectionStart} max: {_selectionEnd}");
            _lastMouseScreenPos = _mouseScreenPos;
            OnSelectionChangedViewport?.Invoke(_selectionStart, _selectionEnd);
        }
    }
    public void StartSelection()
    {
        _isHoldingMouse = true;
        _mouseHoldStartScreenPos = _mouseScreenPos;
    }
    public void EndSelection()
    {
        _isHoldingMouse = false;
    }
    public void SetMousePos(Vector2 pos)
    {
        _mouseScreenPos = pos;

    }
}
