using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Enemy
{
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

    public void CenterShoot()
    {
        base.CreateProjectile(_projectile[0], _gunPosition[0].position, Quaternion.Euler(0f, 0f, 180f));
    }
}
