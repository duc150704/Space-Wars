using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected Vector3 _direction;
    [SerializeField] protected bool _canBeDestroy = true;

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.Self);
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

    public void Rotate(float speed)
    {
        speed = speed * Time.deltaTime;
        transform.Rotate(0f, 0f, speed);
    }
}