using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BezierMoveStrategy : IMoveStrategy
{
    Bezier _bezier;
    Transform _enemy;

    public BezierMoveStrategy(Transform enemy, Bezier bezier)
    {
        _enemy = enemy;
        _bezier = bezier;
    }
    public IEnumerator Move(float time, Vector3 target, Action onComplete)
    {
        if (!_bezier || !_enemy)
        {
            Debug.LogWarning("Chua co bezier hoac enemy");
            yield break;
        }

        float timeCounter = 0f;
        while(timeCounter < time)
        {
            _enemy.position = _bezier.Calculate(timeCounter / time);
            timeCounter += Time.deltaTime;
            yield return null;
        }

        onComplete?.Invoke();
    }
}
