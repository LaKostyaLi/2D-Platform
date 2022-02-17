using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private Transform _target;
    public void Initialize(Transform target)
    {
        _target = target;
        _camera.Follow = _target; //за кем следим
        _camera.LookAt = _target;
    }

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }
}
