using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventManager.Subscribe(EEventType.GameStart, PlayBackgroundMusic);
    }

    [SerializeField] AudioSource _backgroundMusic;
    [SerializeField] AudioSource _sfx;

    [SerializeField] AudioClip _mainShipShootingSound;
    [SerializeField] AudioClip _background;
    public void PlayMainShipShootingSound()
    {
        _sfx.PlayOneShot(_mainShipShootingSound);
    }

    public void PlayBackgroundMusic()
    {
        _backgroundMusic.clip = _background;
        _backgroundMusic.Play();
    }
}
