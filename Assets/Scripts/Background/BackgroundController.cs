using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] float _speed;

    void Update()
    {
        transform.Translate(_speed * Vector3.down * Time.deltaTime);
    }
}
