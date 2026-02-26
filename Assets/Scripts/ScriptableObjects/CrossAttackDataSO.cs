using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Boss/Attack data/Cross Attack")]
public class CrossAttackDataSO : BaseProjectileDataSO
{
    public override IAttackStrategy CreateAttackStrategy(BossController boss)
    {
        CrossAttackData data = new CrossAttackData()
        { 
            ProjectileSpeed = this.ProjectileSpeed,
            ProjectilePerWave = this.ProjectilePerWave,
            ProjectilePref = this.ProjectilePref,
            TimeBetweenWave = this.TimeBetweenWave,
            WaveCount = this.WaveCount,
        };

        return new CrossAttack(data, boss);
    }
}
