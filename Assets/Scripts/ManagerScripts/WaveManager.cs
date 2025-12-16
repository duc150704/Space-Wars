using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class WaveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _waveName;
    [SerializeField] List<Wave> _waveList = new List<Wave>();


    int _enemyReamining;

    private void Awake()
    {
        EventManager.Subscribe(EEventType.EnemyDead, OnEnemyDead);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(EEventType.EnemyDead, OnEnemyDead);
    }

    private void Start()
    {
        _waveName.alpha = 0f;
        StartCoroutine(StartLevel_IE());
    }

    IEnumerator StartLevel_IE()
    {
        yield return new WaitForSeconds(2f);
        EventManager.Notify(EEventType.StartPlaying);
        for(int i = 0; i < _waveList.Count; i++)
        {
            _enemyReamining = _waveList[i].TotalEnemy;

            _waveList[i].gameObject.SetActive(true);
            Debug.Log($"Start " + _waveList[i].name.ToString());

            yield return StartCoroutine(DisplayWaveName(_waveList[i]));
            _waveList[i].CurrentWaveState = Wave.EWaveState.SPAWNING;

            yield return new WaitUntil(() => _enemyReamining <= 0);
            _waveList[i].CurrentWaveState = Wave.EWaveState.DONE;

            _waveList[i].gameObject.SetActive(false);
            Debug.Log($"End " + _waveList[i].name.ToString());
            yield return new WaitForSeconds(3f);
        }

        GameManager.Instance.ChangeState(GameManager.GameState.Win);
    }

    IEnumerator DisplayWaveName(Wave wave)
    {
        _waveName.text = wave.Name;
        yield return StartCoroutine(Fade(2f, false));
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(Fade(2f, true));
    }

    IEnumerator Fade(float time, bool reverse)
    {
        float timeCounter = 0;
        while(timeCounter <= time)
        {
            _waveName.alpha = Mathf.Lerp((reverse) ? 1f : 0f, (reverse) ? 0f : 1f, timeCounter/time);
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }

    public void OnEnemyDead()
    {
        _enemyReamining -= 1;
    }
}
