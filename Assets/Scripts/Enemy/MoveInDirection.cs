using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] Vector3 _direction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_moveSpeed * _direction * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector3 direction, float speed = 5)
    {
        _direction = direction;
        _moveSpeed = speed;
    }
}