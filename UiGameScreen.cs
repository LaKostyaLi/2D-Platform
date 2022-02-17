using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGameScreen : MonoBehaviour
{
    private UiCoinsText _coinsText;
    private GameManager _gameManager;

    private void Awake()
    {
        _coinsText = GetComponentInChildren<UiCoinsText>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _coinsText.OnCoinsChange(_gameManager.Coins);
    }
}
