using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttackDataSO : ScriptableObject
{
    public abstract IAttackStrategy CreateAttackStrategy(BossController boss);
}

public abstract class BaseProjectileDataSO : BaseAttackDataSO
{
    public GameObject ProjectilePref;

    public float TimeBetweenWave;
    public float ProjectileSpeed;

    public int ProjectilePerWave;
    public int WaveCount;

}
