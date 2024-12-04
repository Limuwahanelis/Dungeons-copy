using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHidePanel : MonoBehaviour
{
    [SerializeField] GameObject _currentPanel;
    public void ShowPanel(GameObject panelToShow)
    {
        _currentPanel.SetActive(false);
        _currentPanel = panelToShow;
        _currentPanel.SetActive(true);
    }
}
