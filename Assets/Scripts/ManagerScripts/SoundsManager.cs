using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ESoundType
{
    BgmMenu,
    BgmGamePlay,

    Bullet1,

    ShipExpl,
}

[System.Serializable]
public struct SoundData
{
    public ESoundType SoundType;
    public AudioClip Clip;
}

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }
    public static float BgmVolume {
        get => Instance._bgmSource.volume;
        set
        {
            Instance._bgmSource.volume = value;
            PlayerPrefs.SetFloat("bgmVolume", value);
        }
    }
    public static float SfxVolume
    {
        get => Instance._sfxSource.volume;
        set
        {
            Instance._sfxSource.volume = value;
            PlayerPrefs.SetFloat("sfxVolume", value);
        }
    }

    [SerializeField] AudioSource _bgmSource;
    [SerializeField] AudioSource _sfxSource;

    [SerializeField] List<SoundData> _soundList = new List<SoundData>();
    [SerializeField] Dictionary<ESoundType, AudioClip> _soundDictionary = new Dictionary<ESoundType, AudioClip>();

    private void Awake()
    {
        Instance = this;
        foreach (var item in _soundList)
        {
            _soundDictionary.Add(item.SoundType, item.Clip);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadData()
    {
        _bgmSource.volume = PlayerPrefs.GetFloat("bgmVolume");
        _sfxSource.volume = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void StopBgm()
    {
        _bgmSource.Stop();
    }

    public void PauseBgm()
    {
        _bgmSource.Pause();
    }
    void PlaySFX(ESoundType sound)
    {
        if (!_soundDictionary.ContainsKey(sound))
        {
            Debug.Log("Khong co am thanh: " + sound.ToString());
            return;
        }
        _sfxSource.PlayOneShot(_soundDictionary[sound]);
    }

    void PlayBackgroundMusic(ESoundType sound)
    {
        _bgmSource.clip = _soundDictionary[sound];
        _bgmSource.Play();
    }

    public static void PlaySound(ESoundType sound, bool isBackgroundMusic = false)
    {
        if (isBackgroundMusic)
        {
            Instance.PlayBackgroundMusic(sound);
        }
        else
        {
            Instance.PlaySFX(sound);
        }
    }
}
