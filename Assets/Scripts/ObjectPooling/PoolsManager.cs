using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class PoolsManager : MonoBehaviour
{
    public static PoolsManager Instance;
    Dictionary<GameObject, Pool> _ObjectsPool = new Dictionary<GameObject, Pool>();
    Dictionary<GameObject, GameObject> _instanceToPrefab = new Dictionary<GameObject, GameObject>();
    GameObject _holder;

    private void Awake()
    {
        Instance = this;
        if(_holder == null)
        {
            _holder = new GameObject("Holder");
        }
    }

    public GameObject TakeObjFromPool(GameObject prefab, IData data = null)
    {
        if (_ObjectsPool.ContainsKey(prefab) == false)
        {
            _ObjectsPool.Add(prefab, new Pool(prefab));
        }

        GameObject instance = _ObjectsPool[prefab].GetObj();

        if (_instanceToPrefab.ContainsKey(instance) == false) 
        {
            _instanceToPrefab.Add(instance, prefab);
        }

        instance.transform.SetParent(null);
        if (data is TransformData transformData)
        {
            instance.transform.SetPositionAndRotation(transformData.Position, transformData.Rotation);
            instance.transform.localScale = transformData.Scale;
        }
            
        instance.SetActive(true);
        return instance;
    }
    public void BackObjToPool(GameObject obj)
    {
        if (_instanceToPrefab.TryGetValue(obj, out GameObject prefab))
        {

            obj.transform.rotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.transform.SetParent(_holder.transform);

            _instanceToPrefab.Remove(obj);

            obj.SetActive(false);
            _ObjectsPool[prefab].BackObj(obj);
        }
        else
        {
            Debug.Log("Khong phai obj trong pool! " + obj.name);
        }
    }
}
