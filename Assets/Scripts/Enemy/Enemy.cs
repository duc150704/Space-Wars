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
    public bool CanShoot
    {
        get { return _canShoot; }
        set { _canShoot = value; }
    }

    [SerializeField] protected GameObject _destructionEffect;
    [SerializeField] protected List<Transform> _gunPosition = new();
    [SerializeField] protected List<GameObject> _projectile;

    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected IMoveStrategy _moveStrategy;

    protected void OnEnable()
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
        Debug.Log("1");
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
        GameObject go = PoolsManager.Instance.TakeObjFromPool(_projectile);
        go.transform.position = position;
        go.transform.rotation = quaternion;
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
        EventManager.Notify(EEventType.EnemyDead);
        PoolsManager.Instance.BackObjToPool(gameObject);
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
            timeCounter -= Time.deltaTime;
            yield return null;
        }
    }
}
