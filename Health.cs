using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue = 3f;
    [SerializeField] private float _startValue = 3f;

    private float _currentValue;
    public event System.Action OnDie;

    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            _currentValue = Mathf.Clamp(value, min: 0, _maxValue);
            if (_currentValue == 0) OnDie?.Invoke();
        }
    }
    private void Start()
    {
        _currentValue = _startValue;
    }
    [ContextMenu(itemName:"Set dead")]
    public void SetDead()
    {
        CurrentValue = 0;
    }

    [ContextMenu(itemName: "Set dead")]
    public void SetMaxValue()
    {
        CurrentValue = _maxValue;
    }
}
