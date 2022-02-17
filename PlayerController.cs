using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed = 5f;
    [SerializeField] private float _jumpPower = 5f;

    private Rigidbody2D _rigidBody;
    private Health _health;
    private PlayerAnimationController _animatorController;

    private bool _isActive=true;
    private bool _isCanJump=true;
    private float _maxVelocityMagnitude; //максимальная скорость движения

    public event System.Action OnWin;
    public event System.Action OnLost;
    public event System.Action OnCoinsCollected;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
        _animatorController = GetComponent<PlayerAnimationController>();
    }

    private void Start()
    {
        _health.OnDie += OnDie;
        _maxVelocityMagnitude = Mathf.Sqrt(Mathf.Pow(_jumpPower, 2) + Mathf.Pow(_horizontalSpeed, 2));
    }

    private void OnDestroy()
    {
        _health.OnDie -= OnDie;
    }

    private void FixedUpdate()
    {
        if (!_isActive) //если не активны
            return;
        Movement();
        UpdateSide();
    }

    private void Movement()
    {
        HorizontalMove();
        VerticalMove();
        ClampVelocity();
    }

    private void HorizontalMove()
    {
        float horizontalAxis = SimpleInput.GetAxis("Horizontal");
        var velocity = _rigidBody.velocity;
        velocity.x = horizontalAxis * _horizontalSpeed;
        _rigidBody.velocity = velocity;
        _animatorController.SetSpeed(velocity.x == 0 ? 0 : (int)Mathf.Sign(velocity.x));
    }

    private void VerticalMove()
    {
        if (_isCanJump && SimpleInput.GetAxis("Vertical") > 0)
        {
            _isCanJump = false;
            _rigidBody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _animatorController.SetJump();
        }
    }

    private void ClampVelocity()
    {
        float velocity = _rigidBody.velocity.magnitude;
        velocity = Mathf.Clamp(velocity, 0f, _maxVelocityMagnitude);
        _rigidBody.velocity = _rigidBody.velocity.normalized * velocity;
    }

    private void UpdateSide()
    {
        var isNeedUpdate = Mathf.Abs(_rigidBody.velocity.x) > 0f;
        if (!isNeedUpdate)
            return;

        var side = Mathf.Sign(_rigidBody.velocity.x);
        var localScale = transform.localScale;
        if(Mathf.Sign(localScale.x)!=side)
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isActive)
            return;

        if(collision.gameObject.CompareTag("Ground"))
        {
            _isCanJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!_isActive)
            return;

        var coin = col.gameObject.GetComponent<Coin>();
        if(coin!=null)
        {
            coin.gameObject.SetActive(false);
            OnCoinsCollected?.Invoke();
        }
        var finish = col.gameObject.GetComponent<Finish>();
        if(finish !=null)
        {
            Deactivate();
            _animatorController.SetSpeed(0);
            OnWin?.Invoke();
        }

    }

    private void Deactivate()
    {
        _isActive = false;
        _rigidBody.velocity = Vector2.zero;
    }

    private void OnDie()
    {
        Deactivate();
    }
}
