using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public enum EWaveState
    {
        WAITTING,
        SPAWNED,
        DONE
    }
    EWaveState _currentWaveState = EWaveState.WAITTING;

    [SerializeField] List<GameObject> _enemyType = new List<GameObject>();
    [SerializeField] int _totalEnemy;
}
