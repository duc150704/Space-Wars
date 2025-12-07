using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageble
{
    [SerializeField] GameObject _explEffect;

    [SerializeField] float _maxHealth;
    [SerializeField] float _timeToDestroy;

    float _currentHealth;
    bool _isDestroyed = false;
    private void OnEnable()
    {
        _currentHealth = _maxHealth;
        _isDestroyed = false;
        StopAllCoroutines();
        StartCoroutine(DestructionByTime());
    }

    public void GetDamage(float damage)
    {
        if (_isDestroyed)
            return;
        _currentHealth -= damage;
        if (_currentHealth <= 0) 
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.LogWarning('1');
        _isDestroyed = true;
        PoolsManager.Instance.TakeObjFromPool(_explEffect, new TransformData(transform));
        EventManager.Notify(EEventType.EnemyDead);
        PoolsManager.Instance.BackObjToPool(gameObject);
    }

    IEnumerator DestructionByTime()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        Debug.LogError('1');
        _isDestroyed = true;
        EventManager.Notify(EEventType.EnemyDead);
        PoolsManager.Instance.BackObjToPool(gameObject);
    }
}
