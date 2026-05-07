using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageble
{
    [SerializeField] GameObject _explEffect;
    [SerializeField] float _maxHealth;

    float _currentHealth;
    bool _isDestroyed = false;
    private void OnEnable()
    {
        _currentHealth = _maxHealth;
        _isDestroyed = false;
    }

    private void OnDisable()
    {
        EventManager.Notify(EEvent.EnemyDead);
    }

    public void GetDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) 
        {
            Die();
        }
    }

    public void Die()
    {
        if (_isDestroyed) return;
        _isDestroyed = true;
        PoolsManager.Instance.BackObjToPool(gameObject);
        PoolsManager.Instance.TakeObjFromPool(_explEffect, new TransformData(transform));
    }
}
