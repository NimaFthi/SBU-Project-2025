using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D Rigidbody;
    public float Speed = 3;
    public Transform[] Path;

    private int _currentPathIndex = 0;
    private Transform _targetPoint;
    private float _currentDirection;

    private void Start()
    {
        _currentPathIndex = 0;
        _targetPoint = Path[_currentPathIndex];
        _currentDirection = (_targetPoint.position - transform.position).normalized.x;
    }

    public void Update()
    {
        CalculateDirection();
    }

    private void CalculateDirection()
    {
        if (Vector2.Distance(transform.position, _targetPoint.position) < 0.5)
        {
            _currentPathIndex++;
            if (_currentPathIndex >= Path.Length)
            {
                _currentPathIndex = 0;
            }
            
            _targetPoint = Path[_currentPathIndex];
            _currentDirection = (_targetPoint.position - transform.position).normalized.x;
            spriteRenderer.flipX = _currentDirection > 0 ? true : false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Rigidbody.linearVelocity = new Vector2(_currentDirection * Speed, 0f);
    }
    
    
}
