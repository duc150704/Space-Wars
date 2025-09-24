using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField]float _speed = 5f;

    [SerializeField] Vector3 _direction;

    Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _speed * _direction;
    }

    public void MoveUp(float speed)
    {
        _speed = speed;
        _direction = Vector3.up;
    }
    public void MoveDown(float speed)
    {
        _speed = speed;
        _direction = Vector3.down;
    }
    public void MoveInDirection(float speed, Vector3 direction)
    {
        _speed = speed;
        _direction = direction.normalized;
    }
}
