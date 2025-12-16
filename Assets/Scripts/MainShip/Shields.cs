using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour
{
    [SerializeField] float _duration;
    Collider2D _collider;

    private void Awake()
    {
        EventManager.Subscribe(EEventType.ShieldOn, TurnOnShield);
    }

    private void Start()
    {
        _collider = GetComponentInParent<Collider2D>();
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
        gameObject.SetActive(true);
        _collider.enabled = false;

        yield return new WaitForSeconds(_duration);

        gameObject.SetActive(false);
        _collider.enabled = true;
    }
}
