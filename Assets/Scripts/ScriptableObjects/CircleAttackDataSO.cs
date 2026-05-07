using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Boss/Attack data/Circle Attack")]
public class CircleAttackDataSO : BaseProjectileDataSO
{
    public override IAttackStrategy CreateAttackStrategy(BossController boss)
    {
        CircleAttackData data = new CircleAttackData()
        {
            ProjectilePerWave = this.ProjectilePerWave,
            ProjectilePref = this.ProjectilePref,
            ProjectileSpeed = this.ProjectileSpeed,
            TimeBetweenWave = this.TimeBetweenWave,
            WaveCount = this.WaveCount,
        };

        return new CircleAttack(data, boss);
    }
}
