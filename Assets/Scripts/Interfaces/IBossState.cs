using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossState 
{
    IEnumerator Excute(BossController boss);
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

    public IEnumerator Excute(BossController boss)
    {
        boss.Go(_time, _target, () => { });
        yield return new WaitForSeconds(_time + 0.5f);
    }
}

public class AttackState : IBossState
{
    AttackStrategy _attackStrategy;
    public AttackState(AttackStrategy attackStrategy)
    {
        _attackStrategy = attackStrategy;
    }   

    public IEnumerator Excute(BossController boss)
    {
        yield return null;
        //yield return boss.StartCoroutine(_attackStrategy.Attack());
    }

}