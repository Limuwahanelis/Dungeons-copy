using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TabToggleUILogic : MonoBehaviour
{
    public UnityEvent OnTabSelected;
    public UnityEvent OnTabDeSelected;

    public void TabStateChange(bool isSelected)
    {
        if(isSelected) OnTabSelected?.Invoke();
        else OnTabDeSelected?.Invoke();
    }
}
