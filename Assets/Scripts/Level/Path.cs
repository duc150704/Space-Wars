using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] protected float _enemyMoveSpeed;
    [SerializeField] protected float _timeBetweenShootingWave;
    [SerializeField, Range(0, 1)] protected float _shootingEnemyPercent;

    [SerializeField] protected Wave _wave;
    [SerializeField] protected List<Transform> _spawnPositionList = new List<Transform>();
    [SerializeField] protected List<Transform> _pathPoints = new List<Transform>();

    protected List<GameObject> _spawnedEnermyList = new List<GameObject>();

    protected void RandomEnemyShoot()
    {
        StartCoroutine(RandomSpawnedEnemy());
    }
    IEnumerator RandomSpawnedEnemy()
    {
        WaitForSeconds waittingTime = new WaitForSeconds(_timeBetweenShootingWave > 0 ? _timeBetweenShootingWave : 1f);
        while (true)
        {
            RandomizeList(_spawnedEnermyList);
            for (int i = 0; i < Mathf.Lerp(1, _spawnedEnermyList.Count - 1, _shootingEnemyPercent); i++) 
            {
                if(_spawnedEnermyList.Count ==0 )
                    yield return new WaitUntil(() => _spawnedEnermyList.Count > 0);
                if (_spawnedEnermyList[i] == null)
                {
                    _spawnedEnermyList.Remove(_spawnedEnermyList[i]);
                    continue;
                }
                _spawnedEnermyList[i].GetComponent<Enemy>().CanShoot = true;
                yield return new WaitForSeconds(0.5f);
            }
            yield return waittingTime;
        }
    }
    void RandomizeList(List<GameObject> list)
    {
        for(int i = list.Count - 1; i >= 0; i--)
        {
            int randomNumber = UnityEngine.Random.Range(0, i);
            GameObject tmp = list[i];
            list[i] = list[randomNumber];
            list[randomNumber] = tmp;
        }
    } 
}
