using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] protected float _timeToDestroy = 4;
    [SerializeField]public float _dropChance = 0.5f;
    protected Rigidbody2D _rigidbody;
    WaitForSeconds _time;


    private void Awake()
    {
        _time = new WaitForSeconds(_timeToDestroy);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Drop()
    {
        if (_rigidbody != null) 
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        _rigidbody.AddForce(new Vector2(Random.Range(0,5), 5), ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Destruction());
    }

    IEnumerator Destruction()
    {
        yield return _time;
        PoolsManager.Instance.BackObjToPool(gameObject);
    }
}
