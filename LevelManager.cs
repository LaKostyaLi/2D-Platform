using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    private GameObject _currentLevel;

    private PlayerController _playerController;

    public PlayerController Player => _playerController;

    public void InstantiateLevel( int index)
    {
        if(_currentLevel!=null)
        {
            Destroy(_currentLevel);
        }

        index = index / levels.Length >= 1 ? index % levels.Length : index;
        _currentLevel = Instantiate(levels[index], transform);
        _playerController = _currentLevel.GetComponentInChildren<PlayerController>();
    }
}
