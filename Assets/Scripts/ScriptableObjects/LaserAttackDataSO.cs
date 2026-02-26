using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser", menuName = "Boss/Attack data/Laser Attack")]
public class LaserAttackDataSO : BaseAttackDataSO
{
    public GameObject ProjectilePref;

    public float Duration;
    public float RotateSpeed;
    public float FireSpeed;

    public Vector2 MaxSize;

    public int ProjectilePerTime;
    public override IAttackStrategy CreateAttackStrategy(BossController boss)
    {
        LaserAttackData data = new LaserAttackData()
        {
            ProjectilePerTime = ProjectilePerTime,
            ProjectilePref = ProjectilePref,
            Duration = Duration,
            RotateSpeed = RotateSpeed,
            MaxSize = MaxSize,
            FireSpeed = FireSpeed,
        };

        return new LaserAttack(data, boss);
    }
}
