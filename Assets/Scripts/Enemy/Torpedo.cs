using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : Enemy
{
    bool _isLocked = false;

    public override bool CanShoot
    {
        get => _canShoot;
        set
        {
            if (!_isLocked)
            {
                _canShoot = value;
                _isLocked = true;
            }
        }
    }
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    public override void Shoot()
    {
        base.Shoot();
    }

    public void L1Shoot()
    {
        base.CreateProjectile(_projectile[0], _gunPosition[1].position, Quaternion.Euler(0f, 0f, 180f));
    }    
    public void L2Shoot()
    {
        base.CreateProjectile(_projectile[0], _gunPosition[3].position, Quaternion.Euler(0f, 0f, 180f));
    }    
    public void L3Shoot()
    {
        base.CreateProjectile(_projectile[0], _gunPosition[5].position, Quaternion.Euler(0f, 0f, 180f));
        Debug.Log("Shoot");
    }

    public void R1Shoot()
    {
        base.CreateProjectile(_projectile[0], _gunPosition[0].position, Quaternion.Euler(0f, 180f, 180f));
    }    
    public void R2Shoot()
    {
        base.CreateProjectile(_projectile[0], _gunPosition[2].position, Quaternion.Euler(0f, 180f, 180f));
    }    
    public void R3Shoot()
    {
        base.CreateProjectile(_projectile[0], _gunPosition[4].position, Quaternion.Euler(0f, 180f, 180f));
    }
}
