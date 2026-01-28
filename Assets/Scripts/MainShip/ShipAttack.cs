using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttack : MonoBehaviour
{
    [SerializeField] int _gunPower;
    [SerializeField] int _maxGunPower;

    [SerializeField] float _shootCoolDownTime;
    float _shootCoolDownTimeCounter = 0;
    bool _canShoot = true;

    [SerializeField] GameObject _currentProjectile;
    [SerializeField] Transform _middleGun;
    [SerializeField] Transform _leftGun;
    [SerializeField] Transform _rightGun;

    public static event Action<int> OnGunPowerChanged;
    public int GunPower
    {
        get => _gunPower;
        set
        {
            _gunPower = (value <= 0) ? 1 : value;
            OnGunPowerChanged?.Invoke(_gunPower);
        }
    }

    private void Start()
    {
        OnGunPowerChanged?.Invoke(_gunPower);
    }

    private void Update()
    {
        CanShootCheck();   
    }

    void CanShootCheck()
    {
        _shootCoolDownTimeCounter += Time.deltaTime;
        if (_shootCoolDownTimeCounter > _shootCoolDownTime)
        {
            _canShoot = true;
        }
        else
        {
            _canShoot = false;
        }
    }


    public bool Shoot()
    {
        if (!_canShoot)
            return false;
        _shootCoolDownTimeCounter = 0f;
        switch (_gunPower)
        {
            case 1:
                CreatePojectiles(_middleGun.position);
                break;
            case 2:
                CreatePojectiles(_leftGun.position);
                CreatePojectiles(_rightGun.position);
                break;
            case 3:
                CreatePojectiles(_middleGun.position);
                CreatePojectiles(_leftGun.position);
                CreatePojectiles(_rightGun.position);
                break;
            default:
                CreatePojectiles(_middleGun.position);
                CreatePojectiles(_leftGun.position);
                CreatePojectiles(_rightGun.position);
                break;
        }

        return true;
    }

    void CreatePojectiles(Vector3 position)
    {
        GameObject poj = PoolsManager.Instance.TakeObjFromPool(_currentProjectile);
        poj.transform.position = position;
    }

    public void PowerUp()
    {
        if (_gunPower >= _maxGunPower)
            return;
        ++GunPower;
    }
}
