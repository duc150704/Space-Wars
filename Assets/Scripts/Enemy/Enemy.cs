using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] protected float _maxHealth;

    protected float _currentHealth;
    protected bool _canShoot = false;
    public virtual bool CanShoot
    {
        get => _canShoot;
        set => _canShoot = value;
    }

    [SerializeField] protected GameObject _destructionEffect;
    [SerializeField] protected List<Transform> _gunPosition = new();
    [SerializeField] protected List<GameObject> _projectile;
    protected Animator _animator;

    protected SpriteRenderer _spriteRenderer;

    protected void Start()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        if (_canShoot && _spriteRenderer.isVisible)
        {
            Shoot();            
        }
    }


    public virtual void Shoot()
    {
        _animator.SetTrigger("Attack");
        _canShoot = false;
    }

    public void GetDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected GameObject CreateProjectile(GameObject _projectile, Vector3 position, Quaternion quaternion)
    {
        GameObject go = Instantiate(_projectile, position, quaternion);
        return go;
    }

    public void MoveTo(Vector3 position, float speed)
    {
        StartCoroutine(Move(position, speed));
    }

    IEnumerator Move(Vector3 position, float speed)
    {
        while(Vector3.Distance(transform.position, position) > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void Die()
    {
        GameObject go = Instantiate(_destructionEffect, transform.position, Quaternion.Euler(0f, 0f, 180f));
        Destroy(gameObject);
    }
}
