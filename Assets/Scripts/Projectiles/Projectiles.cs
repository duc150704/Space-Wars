using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] Vector3 _direction;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.Self);
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
    public void RotateInDirection(Vector3 direction)
    {
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }
    public void MoveAndRotateInDirection(Vector3 direction, float speed)
    {
        MoveInDirection(speed, direction);
        RotateInDirection(direction);
    }
}