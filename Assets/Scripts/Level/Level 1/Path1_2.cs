using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path1_2 : Path
{
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitUntil(() => _wave.CurrentWaveState == Wave.EWaveState.SPAWNING);
        for(int i = 0; i < _wave.TotalEnemy; i += 3)
        {
            for(int j = 0; j < 3; j++)
            {
                SpawnAsteroid(_wave.EnemyType[0], _spawnPositionList[j].position, Random.Range(1f, 3f));
                yield return new WaitForSeconds(0.6f);
            }

            yield return new WaitForSeconds(0.5f);
        }
        _wave.CurrentWaveState = Wave.EWaveState.SPAWNED;

        yield return new WaitForSeconds(1f);

        _wave.CurrentWaveState = Wave.EWaveState.DONE;
    }

    void SpawnAsteroid(GameObject gameObject, Vector3 position,float scale)
    {
        GameObject newObject = Instantiate(gameObject, position, Quaternion.identity);
        newObject.transform.localScale = new Vector3(scale, scale);
    }
}
