using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AttackType
{
    public float DelayBeforeAttack = 0f;
    public float DelayAfterAttack = 0f;
    public Vector2 MoveTo = Vector2.zero;

    public List<BaseAttackDataSO> AttackStrategies = new List<BaseAttackDataSO>();

    public bool IsExecuteConcurrently = false;
    public bool WaitForComplete = true;
}

public class BossController : Enemy
{
    IBossState _currentState;

    [SerializeField] List<AttackType> _attackTypes;
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
    }

    private new void Update()
    {
       // 
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void Active()
    {
        StartCoroutine(Active_IE());
    }
    IEnumerator Active_IE()
    {
        while (true)
        {
            foreach (var attack in _attackTypes) 
            { 
                if(attack.MoveTo != null)
                {
                    SetState(new MoveState(new StraightMoveStrategy(this.transform), attack.MoveTo, _moveSpeed));
                    yield return StartCoroutine(_currentState.Excute(this));
                }

                if(attack.DelayBeforeAttack > 0)
                {
                    yield return new WaitForSeconds(attack.DelayBeforeAttack);
                }

                foreach(var a in attack.AttackStrategies)
                {
                    IAttackStrategy attackStrategy = a.CreateAttackStrategy(this);
                    SetState(new AttackState(attackStrategy));
                    if (attack.IsExecuteConcurrently)
                    {
                        StartCoroutine(_currentState.Excute(this));
                    }
                    else
                    {
                        yield return StartCoroutine(_currentState.Excute(this));
                    }
                }

                if(attack.DelayAfterAttack > 0)
                {
                    yield return new WaitForSeconds(attack.DelayAfterAttack);
                }
            }
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
