using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    private const string SPEED_KEY = "SpeedInt";
    private const string JUMP_KEY = "Jump";
    private static readonly int SpeedInt = Animator.StringToHash(name: SPEED_KEY);
    private static readonly int Jump = Animator.StringToHash(name: JUMP_KEY);
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(int value)
    {
        _animator.SetInteger(name: "SpeeedInt", value);
    }
    public void SetJump()
    {
        _animator.SetTrigger(name: "Jump");
    }
}
