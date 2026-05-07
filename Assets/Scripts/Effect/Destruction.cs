using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum DestructionByType 
{ 
    None,
    Time,
    Animation,
}


public class Destruction : MonoBehaviour
{
    [SerializeField] DestructionByType _type;
    [SerializeField] float _timeToDestroy;
    WaitForSeconds _wait;
    private void Awake()
    {
        _wait = new WaitForSeconds(_timeToDestroy);
    }

    private void OnEnable()
    {
        switch (_type)
        {
            case DestructionByType.None:
                break;
            case DestructionByType.Time:
                StartCoroutine(DestroyByTimeIE());
                break;
            case DestructionByType.Animation:
                //Ham Destroy() gan trong Animation event;
                break;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator DestroyByTimeIE()
    {
        yield return _wait;
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        PoolsManager.Instance.BackObjToPool(gameObject);
    }
}
