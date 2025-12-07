using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] Vector3 _direction;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_moveSpeed * _direction * Time.deltaTime, Space.World);
    }
}