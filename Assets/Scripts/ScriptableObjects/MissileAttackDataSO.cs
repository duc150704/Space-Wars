using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Boss/Attack data/Missile Attack")]
public class MissileAttackDataSO : BaseProjectileDataSO
{
    public float AngleBetweenMissile = 15f;
    public override IAttackStrategy CreateAttackStrategy(BossController boss)
    {
        MissileAttackData data = new MissileAttackData() 
        { 
            ProjectileSpeed = this.ProjectileSpeed,
            ProjectilePerWave = this.ProjectilePerWave,
            ProjectilePref = this.ProjectilePref,
            TimeBetweenWave = this.TimeBetweenWave,
            WaveCount = this.WaveCount,
            AngleBetweenMissile = this.AngleBetweenMissile,
        };

        return new MissileAttack(data, boss);
    }
}
