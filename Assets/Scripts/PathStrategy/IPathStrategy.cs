using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathStrategy
{
    public IEnumerator StartPath(Enemy enemy, float time,Action onComplete);
}
