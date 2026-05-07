using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : Enemy
{
    //bool _locked = false;
    protected new void OnEnable()
    {
        base.OnEnable();
        //_locked = false;
    }

    //public override void Shoot()
    //{
    //   // if (_locked)
    //       // return;
    //    base.Shoot();
    //    //_locked = true;
    //}

    public void L1Shoot()
    {
        GameObject go = base.CreateProjectile(_projectile[0], _gunPosition[1].position, Quaternion.Euler(0f, 0f, 180f));
        go.GetComponent<EnemyProjectiles>().MoveInDirection(6f, transform.up);
    }
    public void L2Shoot()
    {
        GameObject go = base.CreateProjectile(_projectile[0], _gunPosition[3].position, Quaternion.Euler(0f, 0f, 180f));
        go.GetComponent<EnemyProjectiles>().MoveInDirection(6f, transform.up);

    }
    public void L3Shoot()
    {
        GameObject go = base.CreateProjectile(_projectile[0], _gunPosition[5].position, Quaternion.Euler(0f, 0f, 180f));
        go.GetComponent<EnemyProjectiles>().MoveInDirection(6f, transform.up);

    }

    public void R1Shoot()
    {
        GameObject go = base.CreateProjectile(_projectile[0], _gunPosition[0].position, Quaternion.Euler(0f, 180f, 180f));
        go.GetComponent<EnemyProjectiles>().MoveInDirection(6f, transform.up);

    }
    public void R2Shoot()
    {
        GameObject go = base.CreateProjectile(_projectile[0], _gunPosition[2].position, Quaternion.Euler(0f, 180f, 180f));
        go.GetComponent<EnemyProjectiles>().MoveInDirection(6f, transform.up);

    }
    public void R3Shoot()
    {
        GameObject go = base.CreateProjectile(_projectile[0], _gunPosition[4].position, Quaternion.Euler(0f, 180f, 180f));
        go.GetComponent<EnemyProjectiles>().MoveInDirection(6f, transform.up);

    }
}
