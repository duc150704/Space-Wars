using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Boss/Attack data/Bullet Attack")]
public class BulletAttackDataSO : BaseProjectileDataSO
{
    public float DelayTime;
    public override IAttackStrategy CreateAttackStrategy(BossController boss)
    {
        BulletAttackData data = new BulletAttackData()
        { 
            TimeBetweenWave = this.TimeBetweenWave,
            DelayTime = this.DelayTime,
            ProjectilePerWave = this.ProjectilePerWave,
            ProjectilePref = this.ProjectilePref,
            ProjectileSpeed = this.ProjectileSpeed,
            WaveCount = this.WaveCount,
        };

        return new BulletAttack(data, boss);
    }
}
