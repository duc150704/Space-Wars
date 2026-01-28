using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour
{
    [SerializeField] float _duration;
    SpriteRenderer _spriteRenderer;

    public static event Action<bool> OnShieldActive;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        EventManager.Subscribe(EEvent.OnPlayerRespawn, OnShipAppear);
        EventManager.Subscribe(EEvent.GameStart, OnShipAppear);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EEvent.OnPlayerRespawn, OnShipAppear);
        EventManager.Unsubscribe(EEvent.GameStart, OnShipAppear);
    }

    private void OnShipAppear()
    {
        StartCoroutine(OnShipRespawnIE());
    }

    private IEnumerator OnShipRespawnIE()
    {
        ActiveShield();
        yield return new WaitForSeconds(_duration);
        DeActiveShield();
    }

    private void ActiveShield()
    {
        _spriteRenderer.enabled = true;
        OnShieldActive?.Invoke(true);
    }

    private void DeActiveShield()
    {
        _spriteRenderer.enabled = false;
        OnShieldActive?.Invoke(false);
    }
}
