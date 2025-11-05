using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class RectangularPath : Path
{
    [SerializeField] float _horizontalSpaceBetweenEnemy;
    [SerializeField] float _verticalSpaceBetweenEnemy;
    private void Start()
    {
        StartCoroutine(StartPath());
    }

    IEnumerator StartPath()
    {
        yield return new WaitUntil(() => _wave.CurrentWaveState == Wave.EWaveState.SPAWNING);
        WaitForSeconds waitTime = new WaitForSeconds(0f);

        for(float i = _pathPoints[0].position.y; i >= 0; i -= _verticalSpaceBetweenEnemy)
        {
            for (float j = _pathPoints[0].position.x; j <= _pathPoints[0].position.x * -1; j += _horizontalSpaceBetweenEnemy)
            {
                if (_spawnedEnermyList.Count >= _wave.TotalEnemy)
                    break;
                GameObject enemy = Instantiate(_wave.EnemyType[0], _spawnPositionList[0].position, Quaternion.Euler(0f, 0f, 180f));
                _spawnedEnermyList.Add(enemy);
                enemy.GetComponent<Enemy>().MoveTo(new Vector3(j, i, 0f), _enemyMoveSpeed);
                yield return waitTime;
            }
        }

        _wave.CurrentWaveState = Wave.EWaveState.SPAWNED;
        base.RandomEnemyShoot();
    }
}
