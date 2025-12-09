using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BezierMoveStrategy : IMoveStrategy
{
    Bezier _bezier;
    Transform _enemy;
    bool _rotateAlongThePath = false;
    public bool RotateAlongThePath
    {
        set
        {
            _rotateAlongThePath = value;
        }
    }

    public BezierMoveStrategy(Transform enemy, Bezier bezier, bool rotateAlongThePath = false)
    {
        _enemy = enemy;
        _bezier = bezier;
        _rotateAlongThePath = rotateAlongThePath;
    }
    public IEnumerator Move(float time, Vector3 target, Action onComplete)
    {
        if (!_bezier || !_enemy)
        {
            Debug.LogWarning("Chua co bezier hoac enemy");
            yield break;
        }

        float timeCounter = 0f;
        Vector3 prePositon = _enemy.position;
        Quaternion preRotation = _enemy.rotation;

        Enemy e = _enemy.gameObject.GetComponent<Enemy>();
        while(timeCounter < time)
        {
            _enemy.position = _bezier.Calculate(timeCounter / time);
            if (_rotateAlongThePath)
            {
                e.RotateFollowDirection(_enemy.position - prePositon);   
            }
            timeCounter += Time.deltaTime;
            yield return null;
        }

        onComplete?.Invoke();

        timeCounter = 0f;
        Quaternion nowRotation = _enemy.rotation;
        float rotateTime = 0.5f;

        while (timeCounter <= rotateTime)
        {
            _enemy.rotation = Quaternion.Lerp(nowRotation, preRotation, timeCounter / rotateTime);
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }
}
