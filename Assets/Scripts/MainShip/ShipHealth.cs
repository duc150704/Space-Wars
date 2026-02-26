using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] int _lives;
    bool _isShieldActive = false;
    bool _isDeath = false;
    public bool IsDeath => _isDeath;
    public bool IsShieldActive => _isShieldActive;  
    public static event Action<int> OnLivesChanged;
    public int Lives
    {
        get => _lives;
        set
        {
            _lives = (value < 0) ? 0 : value;
            OnLivesChanged?.Invoke(_lives);
        }
    }

    private void OnEnable()
    {
        Shields.OnShieldActive += OnShield;
    }

    private void OnDisable()
    {
        Shields.OnShieldActive -= OnShield;
    }

    private void Start()
    {
        OnLivesChanged?.Invoke(_lives);
    }

    private void OnShield(bool isActive)
    {
        _isShieldActive = isActive;
    }

    public bool GetDamage()
    {
        if (_isDeath || _isShieldActive)
            return false;
        EventManager.Notify(EEvent.OnPlayerDead);
        OnDead();
        return true;
    }

    public void OnDead()
    {
        Lives--;
        _isDeath = true;
        if(Lives <= 0)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Lose);
            return;
        }
        OnRespawn();
    }

    void OnRespawn()
    {
        EventManager.Notify(EEvent.OnPlayerRespawn);
        _isDeath = false;
    }

}
