using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class HealthBarUI : HealthBar
{
    [SerializeField] RectTransform _fill;
    [SerializeField] RectTransform _bar;
    [SerializeField] int _maxValue;
    [SerializeField] int _currentValue;
    Vector2 tmp;
    Vector2 tmp2;
    public override void Initialize()
    {
        
    }
    public override void ReduceHP(int value)
    {
        _currentValue -= value;
        UpdateDisplay();
    }

    public override void SetHealth(int hp)
    {
        _currentValue = math.clamp(hp, 0, _maxValue);
        UpdateDisplay();
    }

    public override void SetMaxHealth(int value)
    {
        _maxValue = value;
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        
        float pct = _currentValue / (float)_maxValue;
        float value = math.remap(0, 1, -_bar.sizeDelta.x / 2, 0, pct);
        tmp = _fill.anchoredPosition;
        tmp2 = _fill.sizeDelta;
        tmp.x = value;
        tmp2.x = value * 2;
        _fill.anchoredPosition = tmp;
        _fill.sizeDelta = tmp2;
    }
    private void OnValidate()
    {
        _currentValue = math.clamp(_currentValue, 0, _maxValue);
    }
}
