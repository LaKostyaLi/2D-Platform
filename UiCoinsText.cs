using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiCoinsText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }
   

    public void OnCoinsChange(int value)
    {
        _text.text = value.ToString();
    }
}
