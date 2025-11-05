using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _waveName;
    [SerializeField] List<Wave> _waveList = new List<Wave>();

    private void Start()
    {
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        for(int i = 0; i < _waveList.Count; i++)
        {
            _waveList[i].gameObject.SetActive(true);
            Debug.Log($"Start wave {i + 1}");

            yield return StartCoroutine(DisplayWaveName(_waveList[i]));
            _waveList[i].CurrentWaveState = Wave.EWaveState.SPAWNING;

            yield return new WaitUntil(() => _waveList[i].CurrentWaveState == Wave.EWaveState.SPAWNED);
            Debug.Log($"Spawned");
            yield return StartCoroutine(WaveCompletedChecker(_waveList[i]));
            _waveList[i].CurrentWaveState = Wave.EWaveState.DONE;

            _waveList[i].gameObject.SetActive(false);
            Debug.Log($"End wave {i + 1}");
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator DisplayWaveName(Wave wave)
    {
        yield return new WaitForSeconds(1f);
        _waveName.text = wave.Name;
        _waveName.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _waveName.gameObject.SetActive(false);
    }

    IEnumerator WaveCompletedChecker(Wave wave)
    {
        WaitForSeconds wait = new WaitForSeconds(2f);

        while (wave.gameObject.activeSelf) 
        {
            if(!FindAnyObjectByType<Enemy>())
            {
                Debug.Log("Wave completed");
                yield break;
            } else
            {
                yield return wait;
            }
        }
    }
}
