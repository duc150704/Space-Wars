using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    [SerializeField] float _A;
    [SerializeField] float _f;
    [SerializeField] float _phi;
    float _t = 0;
    [SerializeField] bool _vertical = false;
    Vector3 _originPosition;


    // Start is called before the first frame update
    void Start()
    {
        _originPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _t += Time.deltaTime;
        float value = _A * Mathf.Sin(2 * Mathf.PI * _f * _t + _phi * Mathf.Deg2Rad);
        if ( _vertical)
        {
            transform.position = new Vector3(_originPosition.x, _originPosition.y + value, transform.position.z) ;
        }
        else
        {
            transform.position = new Vector3(_originPosition.x + value, _originPosition.y, transform.position.z);
        }
    }
}
