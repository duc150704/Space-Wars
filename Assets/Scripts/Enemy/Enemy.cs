using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _maxHealth;
    float _currentHealth;

    [SerializeField] GameObject _enemyDestructionEffect;
    [SerializeField] List<Transform> _gunPosition = new List<Transform>();
    [SerializeField] GameObject _projectile;
    Animator _animator;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        
    }
    public void Shoot()
    {
        CreateProjectiles(_gunPosition[0].position);
        CreateProjectiles(_gunPosition[1].position);
    }

    void CreateProjectiles(Vector3 gunPosition)
    {
        GameObject projectile = Instantiate(_projectile, gunPosition, Quaternion.identity);
    }

    public void GetDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            GameObject go = Instantiate(_enemyDestructionEffect, transform.position, Quaternion.Euler(0f, 0f, 180f));
            Destroy(go,0.6f);
            Destroy(gameObject);
        }
    }
}
