using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BezierPathStrategy : IPathStrategy
{
    Bezier _bezier;
    public BezierPathStrategy(Bezier bezier)
    {
        _bezier = bezier;
    }

    public IEnumerator StartPath(Enemy enemy, float time, Action onComplete)
    {
        if (!_bezier)
        {
            Debug.LogWarning("Chua khoi tao Bezier!");
            yield break;
        }

        float timeCounter = 0;
        while(timeCounter < time)
        {
            Vector3 targetPos = _bezier.Calculate(timeCounter / time);
            if (enemy == null || enemy.gameObject.activeSelf == false)
            {
                break;
            }
            enemy.SetPositon(targetPos);
            timeCounter += Time.deltaTime;
            yield return null;
        }
        onComplete?.Invoke();
    }
}
