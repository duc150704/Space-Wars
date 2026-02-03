using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackData { }

public class CircleAttackData : IAttackData
{
    public float ProjectileSpeed;
    public float TimeBetweenCircle;
    public float AngleBetweenCircle;

    public int ProjectilePerCircle;
    public int CircleCount;

    public GameObject ProjectilePref;
}

public class MissileAttackData : IAttackData
{
    public float ProjectileSpeed;
    public float TimeBetweenWave;

    public int ProjectilePerWave;
    public int WaveCount;

    public GameObject ProjectilePref;
}

public class BulletAttackData : IAttackData
{
    public float ProjectileSpeed;
    public float DelayTime;
    public float TimeBetweenWave;

    public int ProjectilePerWave;
    public int WaveCount;

    public GameObject ProjectilePref;
}

public class CrossAttackData : IAttackData
{
    public float ProjecctileSpeed;
    public float TimeBetweenWave;

    public int ProjectilePerWave;
    public int WaveCount;

    public GameObject ProjectilePref;
}
