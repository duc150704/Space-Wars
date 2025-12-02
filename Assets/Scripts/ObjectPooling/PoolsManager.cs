using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    public static PoolsManager Instance;
    Dictionary<GameObject, Pool> _dictionary = new Dictionary<GameObject, Pool>();
    Dictionary<GameObject, GameObject> _instanceToPrefab = new Dictionary<GameObject, GameObject>();
    GameObject _holder;

    [SerializeField] List<GameObject> _initGameObject = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
        if(_holder == null)
        {
            _holder = new GameObject("Holder");
        }

    }

    private void Start()
    {
        foreach (var item in _initGameObject)
        {
            List<GameObject> list = new List<GameObject>();
            for(int i = 0; i <= 10; i++)
            {
                var newObj = TakeObjFromPool(item);
                list.Add(newObj);
            }
            foreach (var item1 in list)
            {
                BackObjToPool(item1);
            }
        }
    }

    public GameObject TakeObjFromPool(GameObject prefab)
    {
        if (_dictionary.ContainsKey(prefab) == false)
        {
            _dictionary.Add(prefab, new Pool(prefab));
        }

        GameObject instance = _dictionary[prefab].GetObj();
        if (_instanceToPrefab.ContainsKey(instance) == false) 
        {
            _instanceToPrefab.Add(instance, prefab);
        }
        instance.transform.SetParent(null);
        return instance;
    }
    public void BackObjToPool(GameObject obj)
    {
        if (_instanceToPrefab.TryGetValue(obj, out GameObject prefab))
        {
            obj.transform.SetParent(_holder.transform);
            _dictionary[prefab].BackObj(obj);
        }
    }
}
