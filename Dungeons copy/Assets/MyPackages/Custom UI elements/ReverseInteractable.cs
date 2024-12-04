using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class ReverseInteractable : MonoBehaviour
{
    [SerializeField] Selectable _selectable;

    public void SetInteractable(bool value)
    {
        _selectable.interactable = !value;
    }

    private void OnValidate()
    {
        if (_selectable == null) _selectable = GetComponent<Selectable>();
    }
    private void Reset()
    {
        _selectable = GetComponent<Selectable>();
    }
}
