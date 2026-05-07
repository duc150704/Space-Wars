using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMoveStrategy : IMoveStrategy
{
    Transform _enemy;
    public StraightMoveStrategy(Transform enemy)
    {
        _enemy = enemy;
    }
    public IEnumerator Move(float time, Vector3 target, Action onComplete)
    {
        float timeCounter = 0f;
        Vector3 originalPos = _enemy.position;
        while(timeCounter < time)
        {
            _enemy.position = Vector3.Lerp(originalPos, target, timeCounter / time);
            timeCounter += Time.deltaTime;
            yield return null;
        }
        _enemy.position = target;
        onComplete?.Invoke();
    }
}
