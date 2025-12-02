using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] bool _autoGen;
    [SerializeField] float _omega;
    [SerializeField] int _reverse;

    private void Start()
    {
        if (_autoGen)
        {
            _reverse = Random.Range(1, 3) == 1 ? -1 : 1;
            _omega = Random.Range(4, 15) * 10;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, _omega * _reverse  * Time.deltaTime);
    }
}
