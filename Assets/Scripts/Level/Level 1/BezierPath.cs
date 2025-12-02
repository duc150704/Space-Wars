using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPath : Path
{
    [SerializeField] List<Bezier> beziers = new List<Bezier>();
    [SerializeField] bool _rotationByPath;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartPath());
    }

    IEnumerator StartPath()
    {
        yield return new WaitUntil(() => _wave.CurrentWaveState == Wave.EWaveState.SPAWNING);
        base.RandomEnemyShoot();    
        for(int i = 0; i< _wave.TotalEnemy; i++)
        {
            int r = Random.Range(0, beziers.Count);
            GameObject enemy = Instantiate(_wave.EnemyType[0], _spawnPositionList[0].position, Quaternion.Euler(0f, 0f, 180f));
            _spawnedEnermyList.Add(enemy);
            BezierFollower obj = enemy.GetComponent<BezierFollower>();
            obj.InitPath(beziers[r], 10f);

            yield return new WaitForSeconds(3f);
        }

        _wave.CurrentWaveState = Wave.EWaveState.SPAWNED;
    }
}
