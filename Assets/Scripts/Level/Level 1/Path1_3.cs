using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path1_3 : Path
{
    [SerializeField] List<Bezier> beziers = new List<Bezier>();
    [SerializeField] List<Formations> formations = new List<Formations>();
    protected override IEnumerator SpawnEnemy()
    {
        int enemyEachGroup = _wave.TotalEnemy / formations.Count;
        int extra = _wave.TotalEnemy % formations.Count;
        StartCoroutine(Group1(enemyEachGroup + extra));
        StartCoroutine(Group2(enemyEachGroup));
        yield return null;
    }

    IEnumerator Group1(int enemyEachGroup)
    {
        for (int i = 0; i < enemyEachGroup; i++)
        {
            Vector3 des = formations[0].GetPositionInRectangle(i);
            Spawn(_spawnPosition[0].position, beziers[0], des);
            yield return new WaitForSeconds(_spawningTime);
        }
    }
    IEnumerator Group2(int enemyEachGroup)
    {
        for (int i = 0; i < enemyEachGroup; i++)
        {
            Vector3 des = formations[1].GetPositionInRectangleFip(i);
            Spawn(_spawnPosition[1].position, beziers[1], des);
            yield return new WaitForSeconds(_spawningTime);
        }
    }

    void Spawn(Vector3 spawnPosition, Bezier bezier, Vector3 des)
    {
        GameObject enemy = PoolsManager.Instance.TakeObjFromPool(_wave.EnemyType[0]);
        enemy.transform.SetPositionAndRotation(spawnPosition, Quaternion.Euler(0f, 0f, 180f));

        Enemy e = enemy.GetComponent<Enemy>();
        _spawnedEnermyList.Add(e);

        e.SetMoveStrategy(new BezierMoveStrategy(enemy.transform, bezier, true));
        e.Go(_movingTime, Vector3.zero, () =>
        {
            e.SetMoveStrategy(new StraightMoveStrategy(enemy.transform));
            e.Go(_movingTime * 0.5f, des, () => { });
        });
    }
}
