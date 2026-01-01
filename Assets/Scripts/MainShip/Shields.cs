using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour
{
    [SerializeField] float _duration;
    SpriteRenderer _spriteRenderer;
    ShipController _shipController;

    private void Awake()
    {
        EventManager.Subscribe(EEventType.ShieldOn, TurnOnShield);
    }

    private void Start()
    {
        _shipController = GetComponentInParent<ShipController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(EEventType.ShieldOn, TurnOnShield);
    }

    public void TurnOnShield()
    {
        StartCoroutine(TurnOnShield_IE());
    }

    private IEnumerator TurnOnShield_IE()
    {
        _spriteRenderer.enabled = true;
        _shipController.HasShield = true;
        yield return new WaitForSeconds(_duration);
        _shipController.HasShield = false;
        _spriteRenderer.enabled = false;
    }
}
