using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Wave> _waveList = new List<Wave>();

    int _enemyReamining;

    private void Awake()
    {
        EventManager.Subscribe(EEvent.EnemyDead, OnEnemyDead);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(EEvent.EnemyDead, OnEnemyDead);
    }

    private void Start()
    {
        StartCoroutine(StartLevel_IE());
    }

    IEnumerator StartLevel_IE()
    {
        yield return new WaitForSeconds(2f);
        EventManager.Notify(EEvent.GameStart);
        for(int i = 0; i < _waveList.Count; i++)
        {
            _enemyReamining = _waveList[i].TotalEnemy;

            _waveList[i].gameObject.SetActive(true);
            Debug.Log($"Start " + _waveList[i].name.ToString());

            yield return StartCoroutine(DisplayWaveName(_waveList[i], 3f));
            _waveList[i].CurrentWaveState = Wave.EWaveState.SPAWNING;

            yield return new WaitUntil(() => _enemyReamining <= 0);
            _waveList[i].CurrentWaveState = Wave.EWaveState.DONE;

            _waveList[i].gameObject.SetActive(false);
            Debug.Log($"End " + _waveList[i].name.ToString());
            yield return new WaitForSeconds(3f);
        }

        GameManager.Instance.ChangeState(GameManager.GameState.Win);
    }

    IEnumerator DisplayWaveName(Wave wave, float time)
    {
        UIManager.Instance.ShowWaveName(wave.Name, time);
        yield return new WaitForSeconds(time);
    }

    public void OnEnemyDead()
    {
        _enemyReamining -= 1;
        //Debug.Log(_enemyReamining.ToString());
    }
}
