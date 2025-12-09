using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path1_4 : Path
{
    [SerializeField] List<Bezier> beziers = new List<Bezier>();
    ShipController shipController;

    protected override IEnumerator SpawnEnemy()
    {
        shipController = FindObjectOfType<ShipController>();

        int randomBezier;
        for (int i = 0; i < _wave.TotalEnemy; i++)
        {
            randomBezier = Random.Range(0, beziers.Count);
            GameObject enemy = PoolsManager.Instance.TakeObjFromPool(_wave.EnemyType[0]);
            enemy.transform.SetPositionAndRotation(_spawnPosition[randomBezier].position, Quaternion.Euler(0f, 0f, 180f));

            Enemy e = enemy.GetComponent<Enemy>();
            _spawnedEnermyList.Add(e);

            e.SetMoveStrategy(new BezierMoveStrategy(enemy.transform, beziers[randomBezier]));
            e.RotateFollowObject(shipController.gameObject);
            e.Go(_movingTime, Vector3.zero, () =>
            {
                PoolsManager.Instance.BackObjToPool(enemy);
                EventManager.Notify(EEventType.EnemyDead);
            });
            yield return new WaitForSeconds(_spawningTime);
        }
    }
}
