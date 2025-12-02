using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollower : MonoBehaviour
{
    public Bezier _bezier;
    public float _time;
    float _timeCounter = 0f;

    private void Start()
    {

    }

    private void Update()
    {
        _timeCounter += Time.deltaTime;
        transform.position = _bezier.Move( _timeCounter / _time );
    }
    public void InitPath(Bezier bezier, float time)
    {
        _bezier = bezier;
        _time = time;
    }
}
