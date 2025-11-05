using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public enum EWaveState
    {
        WAITTING,
        SPAWNING,
        SPAWNED,
        DONE
    }
    EWaveState _currentWaveState = EWaveState.WAITTING;
    public EWaveState CurrentWaveState
    {
        get => _currentWaveState;
        set => _currentWaveState = value;
    }

    [SerializeField] List<GameObject> _enemyType = new List<GameObject>();
    [SerializeField] int _totalEnemy;
    [SerializeField] string _name;

    public List<GameObject> EnemyType
    {
        get => _enemyType;
        set => _enemyType = value;
    }

    public int TotalEnemy
    {
        get => _totalEnemy;
        set => _totalEnemy = value;
    }

    public string Name => _name;
}
