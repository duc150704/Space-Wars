using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path1_5 : Path
{
    protected override IEnumerator SpawnEnemy()
    {
        EventManager.Notify(EEvent.OnBossAppear);
        GameObject boss = PoolsManager.Instance.TakeObjFromPool(_wave.EnemyType[0]);
        boss.transform.SetPositionAndRotation(_spawnPosition[0].position, Quaternion.Euler(0f, 0f, 180f));

        BossController b = boss.GetComponent<BossController>();
        b.SetMoveStrategy(new StraightMoveStrategy(boss.transform));
        b.Go(_movingTime, new Vector3(0f, 0f, 0f), () =>
        {
            b.Appear();
        });
        yield return new WaitForSeconds(_movingTime - 1);
        
    }
}
