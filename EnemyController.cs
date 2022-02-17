using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float _speed = 5f;
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;

    private Transform _target;
    private EnemyAnimationController _animatorController;


    private void Awake()
    {
        _target = CustomRandom.RandomBetweenTwo()? _firstPoint : _secondPoint;
        _animatorController = GetComponentInChildren<EnemyAnimationController>();
    }
    private void UpdateSide(float side)
    {
        var localScale = transform.localScale;
        if(Mathf.Sign(localScale.x)!=side)
        {
            localScale.x *= -1;
        }
    }

    private void Update()
    {
        var direction = (_target.position - transform.position).normalized;
        var moveDistance = _speed * Time.deltaTime;
        var distanceToTarget = Vector3.Distance(_target.position, transform.position);

        if(moveDistance>distanceToTarget)
        {
            _target = _target == _firstPoint ? _secondPoint : _firstPoint;
            moveDistance = distanceToTarget;
        }
        transform.Translate(direction * moveDistance);
        _animatorController.SetSpeed((int)Mathf.Sign(direction.x));
        UpdateSide((int)Mathf.Sign(-direction.x));
    }

    public static class CustomRandom
    {
        public static bool RandomBetweenTwo()
        {
            return Random.Range(0, 2) == 0;
        }
    }
}
