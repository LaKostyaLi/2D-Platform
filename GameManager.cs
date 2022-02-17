using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIController _uiController;
    private SaveSystem _saveSystem;
    private LevelManager _levelManager;
    private CameraController _cameraController;

    private GameData gameData;

    public int Coins => gameData.Coins;
    public int Level => gameData.Level;
    public static event System.Action<int> OnCoinsCountChanged = null;

    private void Awake() 
    {
        _uiController = FindObjectOfType<UIController>();

        _saveSystem = GetComponent<SaveSystem>();

        _levelManager = GetComponent<LevelManager>();
        _cameraController = FindObjectOfType<CameraController>();

        gameData = _saveSystem.LoadData();
        _uiController.ShowStartScreen(); 
    }

    private void OnApplicationQuit()
    {
        _saveSystem.SaveData(gameData);
    }

    public void StartGame()
    {
        _uiController.ShowGametScreen();
        _levelManager.InstantiateLevel(Level);
        OnGameStarted();
    }

    public void FailGame()
    {
        _uiController.ShowFailScreen();
        OnGameEnded();
    }

    public void WinGame()
    {
        _uiController.ShowWinScreen();
    }

    private void OnGameStarted()
    {
        _cameraController.Initialize(_levelManager.Player.transform); 
        _levelManager.Player.OnWin += WinGame;
        _levelManager.Player.OnLost += FailGame;
        _levelManager.Player.OnCoinsCollected += OnCoinsCollected;
        
    }

    private void OnGameEnded()
    {
        _levelManager.Player.OnWin -= WinGame;
        _levelManager.Player.OnLost -= FailGame;
        _levelManager.Player.OnCoinsCollected -= OnCoinsCollected;
        _saveSystem.SaveData(gameData);
    }

    private void OnCoinsCollected()
    {
        gameData.Coins++;
        OnCoinsCountChanged?.Invoke(Coins);
    }
}
