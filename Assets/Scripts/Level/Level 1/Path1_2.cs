using System.Collections;
using UnityEngine;

public class Path1_2 : Path
{
    protected override IEnumerator SpawnEnemy()
    {
        int asteroidsEachPos = _wave.TotalEnemy / _spawnPosition.Count;
        int extra = _wave.TotalEnemy % _spawnPosition.Count;
        foreach (var item in _spawnPosition)
        {
            StartCoroutine(SpawnAsteroids(item.position, asteroidsEachPos));
        }
        StartCoroutine(SpawnAsteroids(_spawnPosition[0].position, extra));
        yield return null;
    }
    
    IEnumerator SpawnAsteroids(Vector3 position, int quantity)
    {
        float randomTime = 0f;
        float randomScale = 0f;
        for(int i = 0; i < quantity; i++)
        {
            randomTime = Random.Range(0.2f, 3f);
            randomScale = Random.Range(1f, 3f);
            var data = new TransformData(position, Quaternion.identity, new Vector3(randomScale, randomScale, 1f));
            PoolsManager.Instance.TakeObjFromPool(_wave.EnemyType[0], data);
            yield return new WaitForSeconds(randomTime);
        }
    }
}
