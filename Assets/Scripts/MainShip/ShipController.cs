using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    ShipAnimator _animator;
    ShipMovement _movement;
    ShipAttack _attack;
    ShipHealth _health;

    Transform _transform;

    Vector2 _shipPositon;
    Vector2 _mousePosition;
    float _distanceToMouse;

    public Transform Transform => _transform;

    private void Awake()
    {
        _animator = GetComponent<ShipAnimator>();
        _movement = GetComponent<ShipMovement>();
        _attack = GetComponent<ShipAttack>();
        _health = GetComponent<ShipHealth>();

        _transform = this.transform;
    }

    private void OnEnable()
    {
        GameManager.Instance.RegisterPlayer(this);
    }
    private void Update()
    {
        HandleEngineAnimaton();
        HandleInput();
    }

    private void HandleEngineAnimaton()
    {
        if (_health.IsDeath)
            return;
        _shipPositon = _transform.position;
        _mousePosition = InputManager.Instance.MousePositon();
        _distanceToMouse = Vector2.Distance(_shipPositon, _mousePosition);
        _animator.PowerUpEngine(_distanceToMouse > 0.1f);
    }
    
    private void HandleInput()
    {
        if (_health.IsDeath)
            return;

        if (!_movement.CanMove)
            return;

        if (InputManager.Instance.IsShootinButtonPressed())
        {
            if (_attack.Shoot())
            {
                _movement.KnockBack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&& !_health.IsShieldActive)
        {
            collision.gameObject.GetComponent<IDamageble>()?.GetDamage(5f);
            _health.GetDamage();
        }
    }
}
