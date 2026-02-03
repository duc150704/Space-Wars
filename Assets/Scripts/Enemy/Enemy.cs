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
    [SerializeField] protected bool _canShoot = false;
    bool _isDead = false;
    public bool CanShoot
    {
        get { return _canShoot; }
        set { _canShoot = value; }
    }

    Transform _transform;
    public Transform Transform => _transform;

    [SerializeField] protected GameObject _destructionEffect;
    [SerializeField] protected List<Transform> _gunPosition = new();
    [SerializeField] protected List<GameObject> _projectile;

    public List<Transform> GunPositions => _gunPosition;
    public List<GameObject> Projectiles => _projectile;

    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected IMoveStrategy _moveStrategy;

    protected void OnEnable()
    {
        StopAllCoroutines();
        _isDead = false;
        _currentHealth = _maxHealth;
        _transform = this.transform;
    }
    protected void Start()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        if (_canShoot /*&& _spriteRenderer.isVisible*/)
        {
            Shoot();            
        }
    }

    public void SetPositon(Vector3 target)
    {
        transform.position = target;
    }


    public virtual void Shoot()
    {
        _animator.SetTrigger("Attack");
        _canShoot = false;
    }

    public virtual void GetDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected GameObject CreateProjectile(GameObject _projectile, Vector3 position, Quaternion quaternion)
    {
        GameObject go = PoolsManager.Instance.TakeObjFromPool(_projectile);
        go.transform.position = position;
        go.transform.rotation = quaternion;
        return go;
    }

    public void Die()
    {
        if (_isDead)
            return;
        _isDead = true;
        GameObject effect = PoolsManager.Instance.TakeObjFromPool(_destructionEffect);
        effect.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0f, 0f, 180f));
        EventManager.Notify(EEvent.EnemyDead);
        PoolsManager.Instance.BackObjToPool(gameObject);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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

    public void RotateFollowDirection(Vector3 direction)
    {
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }

    public void RotateFollowObject(GameObject obj, float time = 0)
    {
        StartCoroutine(RotateFollowObject_IE(obj, time));
    }                                                                                                                                                                                                                   
    IEnumerator RotateFollowObject_IE(GameObject obj, float time)
    {
        float timeCounter = 0f;
        while(timeCounter <= time)
        {
            Vector3 direction = obj.transform.position - transform.position;
            RotateFollowDirection(direction);
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }
}
