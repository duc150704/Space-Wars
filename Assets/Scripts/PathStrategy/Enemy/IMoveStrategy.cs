using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveStrategy
{
    public IEnumerator Move(float time, Vector3 target, Action onComplete);
}
