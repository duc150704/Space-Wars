using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossState 
{
    void Enter(BossController boss);
    IEnumerator Excute(BossController boss);
    void Exit(BossController boss);
}

public class MoveState : IBossState
{
    IMoveStrategy _moveStrategy;
    Vector3 _target;
    float _time;

    public MoveState(IMoveStrategy moveStrategy, Vector3 target, float time)
    {
        _moveStrategy = moveStrategy;
        _target = target;
        _time = time;
    }

    public void Enter(BossController boss)
    {
        boss.SetMoveStrategy(_moveStrategy);
    }

    public IEnumerator Excute(BossController boss)
    {
        boss.Go(_time, _target, () => { });
        yield return new WaitForSeconds(_time + 0.5f);
    }

    public void Exit(BossController boss)
    {
        boss.SetMoveStrategy(null);
    }
}

public class AttackState : IBossState
{
    IAttackStrategy _attackStrategy;
    public AttackState(IAttackStrategy attackStrategy)
    {
        _attackStrategy = attackStrategy;
    }   

    public void Enter(BossController boss)
    {
        boss.SetAttackStrategy(_attackStrategy);
    }

    public IEnumerator Excute(BossController boss)
    {
        yield return boss.StartCoroutine(_attackStrategy.Attack(boss));
    }

    public void Exit(BossController boss)
    {
        boss.SetAttackStrategy(null);
    }
}