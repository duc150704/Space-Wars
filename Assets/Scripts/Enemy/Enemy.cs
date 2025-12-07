using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] protected float _maxHealth;
    protected float _currentHealth;
    protected bool _canShoot = false;

    [SerializeField] protected GameObject _destructionEffect;
    [SerializeField] protected List<Transform> _gunPosition = new();
    [SerializeField] protected List<GameObject> _projectile;
    protected Animator _animator;

    protected SpriteRenderer _spriteRenderer;
    protected IMoveStrategy _moveStrategy;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }
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

    public void SetPositon(Vector3 target)
    {
        if (!gameObject)
            return;
        transform.position = target;
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

    //public void MoveTo(Vector3 position, float speed)
    //{
    //    StartCoroutine(Move(position, speed));
    //}

    //IEnumerator Move(Vector3 position, float speed)
    //{
    //    while(Vector3.Distance(transform.position, position) > 0)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    //        yield return null;
    //    }
    //}

    public void Die()
    {
        GameObject effect = PoolsManager.Instance.TakeObjFromPool(_destructionEffect);
        effect.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0f, 0f, 180f));
        PoolsManager.Instance.BackObjToPool(gameObject);
    }

    private void OnDisable()
    {
        EventManager.Notify(EEventType.EnemyDead);
    }

    public void SetMoveStrategy(IMoveStrategy moveStrategy)
    {
        _moveStrategy = moveStrategy;
    }

    public void Go(float time, Vector3 target, Action onComplete)
    {
        if (_moveStrategy == null)
        {
            Debug.LogWarning("Chua co strategy");
            return;
        }

        StartCoroutine(_moveStrategy.Move(time, target, onComplete));
    }
}
