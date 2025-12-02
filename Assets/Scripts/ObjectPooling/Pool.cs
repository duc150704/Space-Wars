using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    Stack<GameObject> _stack = new Stack<GameObject>();
    GameObject _objPref;
    GameObject _tmpObj;
    
    public Pool(GameObject obj)
    {
        this._objPref = obj;
    }
    public GameObject GetObj()
    {
        if (_stack.Count > 0)
        {
            _tmpObj = _stack.Pop();
            _tmpObj.SetActive(true);
            return _tmpObj;
        }
        _tmpObj = GameObject.Instantiate(_objPref);
        return _tmpObj;
    }
    public void BackObj(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        _stack.Push(obj);
    }
}
