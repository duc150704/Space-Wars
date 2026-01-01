using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button _playButton;
    [SerializeField] Slider _bgmMusicSlider;
    [SerializeField] Slider _sfxSlider;

    private void Start()
    {
        _playButton.onClick.AddListener(Play);

        _bgmMusicSlider.onValueChanged.AddListener(SetBackgroundMusicVolume);
        _sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return null;
        SoundsManager.Instance.LoadData();
        _bgmMusicSlider.value = SoundsManager.BgmVolume;
        _sfxSlider.value = SoundsManager.SfxVolume;
        GameManager.Instance.ChangeState(GameManager.GameState.Menu);
    }

    public void Play()
    {
        SoundsManager.Instance.StopBgm();
        SceneController.Instance.LoadScene(ESceneName.Level_1, () =>
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
        });
    }

    public void SetBackgroundMusicVolume(float value)
    {
        SoundsManager.BgmVolume = value;
    }

    public void SetSFXVolume(float value)
    {
        SoundsManager.SfxVolume = value;
    }
}
