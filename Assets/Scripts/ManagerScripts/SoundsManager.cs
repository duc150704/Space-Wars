using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        { 
        Destroy(this);
        }
    }

    [SerializeField] AudioSource _backgroundMusic;
    [SerializeField] AudioSource _sfx;

    [SerializeField] AudioClip _mainShipShootingSound;
    public void PlayMainShipShootingSound()
    {
        _sfx.clip = _mainShipShootingSound;
        _sfx.Play();
    }
}
