using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : Enemy
{
    IBossState _currentState;

    CircleAttack _circleAttack;
    MissileAttack _missileAttack;
    BulletAttack _bulletAttack;
    CrossAttack _crossAttack;

    CircleAttackData _circleAttackData;
    MissileAttackData _missileAttackData;
    BulletAttackData _bulletAttackData;
    CrossAttackData _crossAttackData;

    [SerializeField] List<Vector3> _pathPoints = new List<Vector3>();
    [SerializeField] float _moveSpeed;

    public static event Action<float, float> OnHealthChanged; //current / maxHealth;

    private new void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.RegisterBoss(this);
    }
    protected new void Start()
    {
        base.Start();
        Init();
    }

    private new void Update()
    {
       // 
    }

    void Init()
    {
        InitAttackData();
        InitAttackStratrgy();
    }

    void InitAttackStratrgy()
    {
        _circleAttack = new CircleAttack(_circleAttackData, this);
        _missileAttack = new MissileAttack(_missileAttackData, this);
        _bulletAttack = new BulletAttack(_bulletAttackData, this);
        _crossAttack = new CrossAttack(_crossAttackData, this);
    }

    void InitAttackData()
    {
        _circleAttackData = new CircleAttackData()
        {
            ProjectilePerCircle = 8,
            ProjectileSpeed = 5f,
            CircleCount = 5,
            ProjectilePref = Projectiles[0],
            TimeBetweenCircle = 0.5f,
        };

        _missileAttackData = new MissileAttackData()
        {
            ProjectilePerWave = 3,
            ProjectileSpeed = 5f,
            ProjectilePref = Projectiles[1],
            TimeBetweenWave = 1f,
            WaveCount = 3
        };

        _bulletAttackData = new BulletAttackData()
        {
            ProjectilePerWave = 15,
            ProjectileSpeed = 8f,
            ProjectilePref = Projectiles[2],
            WaveCount = 2,
            DelayTime = 0.1f,
            TimeBetweenWave = 1f,
        };

        _crossAttackData = new CrossAttackData()
        {
            ProjecctileSpeed = 5f,
            ProjectilePerWave = 4,
            TimeBetweenWave = 1f,
            WaveCount = 2,
            ProjectilePref = Projectiles[0],
        };
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void Appear()
    {
        StartCoroutine(Appear_IE());
    }
    IEnumerator Appear_IE()
    {
        while (true)
        {
            SetState(new AttackState(_circleAttack));
            yield return StartCoroutine(_currentState.Excute(this));

            yield return new WaitForSeconds(1f);

            SetState(new MoveState(new StraightMoveStrategy(this.transform), _pathPoints[1], _moveSpeed));
            yield return StartCoroutine(_currentState.Excute(this));

            SetState(new AttackState(_missileAttack));
            StartCoroutine(_currentState.Excute(this));

            SetState(new AttackState(_crossAttack));
            yield return StartCoroutine(_currentState.Excute(this));

            yield return new WaitForSeconds(1f);

            SetState(new MoveState(new StraightMoveStrategy(this.transform), _pathPoints[2], _moveSpeed));
            yield return StartCoroutine(_currentState.Excute(this));

            SetState(new AttackState(_bulletAttack));
            yield return StartCoroutine(_currentState.Excute(this));

            yield return new WaitForSeconds(1f);

            SetState(new MoveState(new StraightMoveStrategy(this.transform), _pathPoints[0], _moveSpeed));
            yield return StartCoroutine(_currentState.Excute(this));

        }
    }

    public void SetState(IBossState state)
    {
        if (_currentState == state)
            return;
        _currentState = state;
    }

    public Vector3 GetPlayerPosition()
    {
        return GameManager.Instance.GetPlayerPosition();
    }
}
