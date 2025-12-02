using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_4Path : Path
{
    GameObject _boss;
    Dreadnought _dreadnought;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartPath());
    }

    IEnumerator StartPath()
    {
        yield return new WaitForSeconds(5.0f);

        yield return StartCoroutine(CameraController.ZoomOut(2f, 11f));
        // yield return new WaitUntil(() => _wave.CurrentWaveState == Wave.EWaveState.SPAWNING);
        _boss = Instantiate(_wave.EnemyType[0], _spawnPositionList[0].position, Quaternion.Euler(0f, 0f, 180f));  
        _dreadnought = _boss.GetComponent<Dreadnought>();
        _dreadnought.MoveTo(Vector3.zero,2f);

        _dreadnought.Shoot1();
    }
}
