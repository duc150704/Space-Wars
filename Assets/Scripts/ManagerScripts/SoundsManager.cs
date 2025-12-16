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
public struct SounData
{
    public ESoundType SoundType;
    public AudioClip Clip;
}

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }
    [SerializeField, Range(0, 1)] float _bgmVolume;
    [SerializeField, Range(0, 1)] float _sfxVolume;

    [SerializeField] AudioSource _bgmSource;
    [SerializeField] AudioSource _sfxSource;

    [SerializeField] List<SounData> _soundList = new List<SounData>();
    [SerializeField] Dictionary<ESoundType, AudioClip> _soundDictionary = new Dictionary<ESoundType, AudioClip>();

    private void Awake()
    {
        Instance = this;
        foreach (var item in _soundList)
        {
            _soundDictionary.Add(item.SoundType, item.Clip);
        }

        
    }
    public void PlaySFX(ESoundType sound)
    {
        _sfxSource.PlayOneShot(_soundDictionary[sound]);
    }

    public void PlayBackgroundMusic(ESoundType sound)
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
