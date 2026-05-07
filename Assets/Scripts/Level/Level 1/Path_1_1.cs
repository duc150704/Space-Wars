using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_1_1 : Path
{
    [SerializeField] Bezier _bezier;
    [SerializeField] Formations _formation;
    protected override IEnumerator SpawnEnemy()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_spawningTime);
        for(int i = 0; i < _wave.TotalEnemy; i++)
        {
            Vector3 targetPos = _formation.GetPositionInRectangle(i);

            GameObject enemy = PoolsManager.Instance.TakeObjFromPool(_wave.EnemyType[0]);
            enemy.transform.SetPositionAndRotation(_spawnPosition[0].position, Quaternion.Euler(0f, 0f, 180f));

            Enemy e = enemy.GetComponent<Enemy>();
            _spawnedEnermyList.Add(e);

            e.SetMoveStrategy(new BezierMoveStrategy(enemy.transform, _bezier));
            e.Go(_movingTime, Vector3.zero, () => {
                e.SetMoveStrategy(new StraightMoveStrategy(enemy.transform));
                e.Go(_movingTime * 0.8f, targetPos, () => { });
            });

            yield return waitForSeconds;
        }
    }
}
