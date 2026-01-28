using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : Enemy
{
    IBossState _currentState;
    AttackStrategy _attackStrategy;

    CircleAttack _waveAttack;
    MissileAttack _missileAttack;
    CrossAttack _crossAttack;
    BulletAttack _bulletAttack;

    [SerializeField] List<Vector3> _pathPoints = new List<Vector3>();
    [SerializeField] float _time;

    ShipController _shipController;

    public static event Action<float, float> OnHealthChanged;
    protected new void Start()
    {
        base.Start();
        GetShipController();
    }

    private new void Update()
    {
        
    }

    void Init()
    {
       
    }

    void GetShipController()
    {
        if (_shipController == null)
        _shipController = FindObjectOfType<ShipController>();
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
            SetState(new AttackState(new CircleAttack(_projectile[0], _gunPosition[0])));
            yield return StartCoroutine(_currentState.Excute(this));

            yield return new WaitForSeconds(1f);

            SetState(new MoveState(new StraightMoveStrategy(this.transform), _pathPoints[1], _time));
            yield return StartCoroutine(_currentState.Excute(this));

            //SetState(new AttackState(new MissileAttack(this)));
            //yield return StartCoroutine(_currentState.Excute(this));

            //SetState(new AttackState(new CrossAttack(this)));
            //yield return StartCoroutine(_currentState.Excute(this));

            //yield return new WaitForSeconds(1f);

            //SetState(new MoveState(new StraightMoveStrategy(this.transform), _pathPoints[2], _time));
            //yield return StartCoroutine(_currentState.Excute(this));

            //SetState(new AttackState(new BulletAttack(this)));
            //yield return StartCoroutine(_currentState.Excute(this));

            //yield return new WaitForSeconds(1f);

            //SetState(new MoveState(new StraightMoveStrategy(this.transform), _pathPoints[0], _time));
            //yield return StartCoroutine(_currentState.Excute(this));

        }
    }

    public void SetState(IBossState state)
    {
        if (_currentState == state)
            return;
        _currentState = state;


        /////
        




    }

    public void SetAttackStrategy(AttackStrategy attackStrategy)
    {
        _attackStrategy = attackStrategy;
    }

    public Vector3 GetPlayerPosition()
    {
        if (_shipController)
            return _shipController.transform.position;
        GetShipController();
        return new Vector3(0f, 1f, 0f);
    }
}
