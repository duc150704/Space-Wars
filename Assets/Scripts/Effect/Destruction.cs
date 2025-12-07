using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    public void Destroy()
    {
        PoolsManager.Instance.BackObjToPool(gameObject);
    }
}
