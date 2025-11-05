using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageble
{
    [SerializeField] GameObject _explEffect;
    [SerializeField] float _maxHealth;
    float _currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        Invoke("DestructionByTime", 5);
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
        GameObject go =  Instantiate(_explEffect, transform.position, Quaternion.identity);
        go.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }

    public void DestructionByTime()
    {
        Destroy(gameObject);
    }
}
