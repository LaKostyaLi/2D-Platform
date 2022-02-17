using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private GameObject _winPanel;

    private GameObject _currentPanel;

    private void DisableCurrentPanel()
    {
        _currentPanel?.SetActive(false);
    }

    public void ShowStartScreen()
    {
        DisableCurrentPanel();
        _currentPanel = _startPanel;
        _currentPanel.SetActive(true);
    }

    public void ShowGametScreen()
    {
        DisableCurrentPanel();
        _currentPanel = _gamePanel;
        _currentPanel.SetActive(true);
    }

    public void ShowFailScreen()
    {
        DisableCurrentPanel();
        _currentPanel = _failPanel;
        _currentPanel.SetActive(true);
    }

    public void ShowWinScreen()
    {
        DisableCurrentPanel();
        _currentPanel = _winPanel;
        _currentPanel.SetActive(true);
    }
}
