using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Path : MonoBehaviour
{
    [SerializeField] protected float _movingTime;
    [SerializeField] protected float _shootingTime;
    [SerializeField] protected float _spawningTime;
    [SerializeField, Range(0, 1)] protected float _shootingChance;

    [SerializeField] protected Wave _wave;
    [SerializeField] protected List<Transform> _spawnPosition = new List<Transform>();

    protected List<Enemy> _spawnedEnermyList = new List<Enemy>();

    protected void Start()
    {
        StartCoroutine(Spawn());
    }
    protected IEnumerator Spawn()
    {
        yield return new WaitUntil(() => _wave.CurrentWaveState == Wave.EWaveState.SPAWNING);
        yield return StartCoroutine(SpawnEnemy());
        _wave.CurrentWaveState = Wave.EWaveState.SPAWNED;
        StartCoroutine(RandomEnemyShooting());
    }

    protected abstract IEnumerator SpawnEnemy();
    IEnumerator RandomEnemyShooting()
    {
        WaitForSeconds waittingTime = new WaitForSeconds(_shootingTime > 0 ? _shootingTime : 1f);

        int enemyDoShoot;
        float time;
        while (true)
        {
            RandomizeList();
            enemyDoShoot = Mathf.CeilToInt((_spawnedEnermyList.Count - 1) * _shootingChance);

            for (int i = 0; i < enemyDoShoot; i++)
            {
                _spawnedEnermyList[i]?.Shoot();
                time = Random.Range(0.2f, 0.8f);
                yield return time;
            }
            yield return waittingTime;
        }
    }

    void RandomizeList()
    {
        for (int i = _spawnedEnermyList.Count - 1; i >= 0; i--)
        {
            int randomNumber = UnityEngine.Random.Range(0, i);
            Enemy tmp = _spawnedEnermyList[i];
            _spawnedEnermyList[i] = _spawnedEnermyList[randomNumber];
            _spawnedEnermyList[randomNumber] = tmp;
        }
    }

    
}
