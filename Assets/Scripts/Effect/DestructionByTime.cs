using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionByTime : MonoBehaviour
{
    [SerializeField] float _time;
    private void OnEnable()
    {
        Invoke("Deactive", _time);
    }

    public void Deactive()
    {
        PoolsManager.Instance.BackObjToPool(gameObject);
    }
}
