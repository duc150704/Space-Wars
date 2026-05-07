using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShipAnimator : MonoBehaviour
{
    [SerializeField] GameObject _explEffect;
    [SerializeField] Animator _engineAnimator;
    [SerializeField] float _explEffectScale;
    [SerializeField] SpriteRenderer _engineSprite;

    SpriteRenderer _spriteRenderer;
    Collider2D _collider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        EventManager.Subscribe(EEvent.OnPlayerDead, OnDead);
        EventManager.Subscribe(EEvent.OnPlayerRespawn, OnRespawn);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EEvent.OnPlayerDead, OnDead);
        EventManager.Unsubscribe(EEvent.OnPlayerRespawn, OnRespawn);
    }

    public void PowerUpEngine(bool isPoweringUp)
    {
        _engineAnimator.SetBool("isPowering", isPoweringUp);
    }

    public void OnDead()
    {
        HideShip();
        ShowExplEffect();
    }

    public void OnRespawn()
    {
        ShowShip();
    }

    public void OnAppear()
    {

    }

    private void ShowExplEffect()
    {
        GameObject go = PoolsManager.Instance.TakeObjFromPool(_explEffect);
        go.transform.position = transform.position;
        go.transform.localScale = Vector3.one * _explEffectScale;
    }

    private void ShowShip()
    {
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
        _engineSprite.enabled = true;
    }

    private void HideShip()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        _engineSprite.enabled = false;
    }
}
