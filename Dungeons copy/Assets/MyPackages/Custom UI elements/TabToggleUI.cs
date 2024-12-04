using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabToggleUI : Toggle
{
    [SerializeField]private Graphic _notSelectedTargetGraphic;
    [SerializeField] private Graphic _selectedTargetGraphic;
    private Navigation _startingNavSettings;
    protected override void Awake()
    {
        base.Awake();
        targetGraphic = _notSelectedTargetGraphic;
        if (isOn) SwapTargetGraphic(isOn);
        _startingNavSettings = navigation;
    }
    protected override void Start()
    {
        base.Start();
        if (group != null)
        {
            foreach (Toggle tog in group.ActiveToggles())
            {
                if (tog == this) continue;
                tog.onValueChanged.AddListener(UnSelectTab);
            }
        }
    }
    #region Interafces
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (isOn) return;
        else base.OnPointerDown(eventData);
    }
    public override void OnSubmit(BaseEventData eventData)
    {
        if (isOn) return;
        base.OnSubmit(eventData);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (isOn) return;
        base.OnPointerClick(eventData);
    }
    #endregion
    #region Nav
    public void SetNavigationMode(bool isExplicit)
    {
        Navigation nav = navigation;
        nav.mode=isExplicit?Navigation.Mode.Explicit:Navigation.Mode.Automatic;
        navigation = nav;
    }
    public void SetSelectableOnTop(Selectable selectable)
    {
        Navigation nav = navigation;
        nav.selectOnUp=selectable;
        navigation = nav;
    }
    public void SetSelectableOnDown(Selectable selectable)
    {
        Navigation nav = navigation;
        nav.selectOnDown = selectable;
        navigation = nav;
    }
    public void SetSelectableOnLeft(Selectable selectable)
    {
        Navigation nav = navigation;
        nav.selectOnLeft = selectable;
        navigation = nav;
    }
    public void SetSelectableOnRight(Selectable selectable)
    {
        Navigation nav = navigation;
        nav.selectOnRight = selectable;
        navigation = nav;
    }
    public void ResetNavigation()
    {
        navigation = _startingNavSettings;
    }
    #endregion

    public void UnSelectTab(bool value)
    {
        if (value) targetGraphic = _notSelectedTargetGraphic;
    }
    public void SwapTargetGraphic(bool isOn)
    {
        if (isOn) targetGraphic = _selectedTargetGraphic;
        else targetGraphic = _notSelectedTargetGraphic;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (group != null)
        {
            foreach (Toggle tog in group.ActiveToggles())
            {
                if (tog == this) continue;
                tog.onValueChanged.RemoveListener(UnSelectTab);
            }
        }
    }
}
