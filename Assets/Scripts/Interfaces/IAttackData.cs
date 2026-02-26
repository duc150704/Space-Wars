using UnityEngine;

public interface IAttackData { }

public class BaseProjectileAttackData : IAttackData
{
    public GameObject ProjectilePref;

    public float ProjectileSpeed;
    public float TimeBetweenWave;

    public int ProjectilePerWave;
    public int WaveCount;
}

public class CircleAttackData : BaseProjectileAttackData { }

public class CrossAttackData : BaseProjectileAttackData { }

public class MissileAttackData : BaseProjectileAttackData
{
    public float AngleBetweenMissile;
}

public class BulletAttackData : BaseProjectileAttackData
{
    public float DelayTime;
}

public class LaserAttackData : IAttackData
{
    public GameObject ProjectilePref;
    
    public float Duration = 3f;
    public float FireSpeed;
    public float RotateSpeed;

    public Vector2 MaxSize;

    public int ProjectilePerTime = 5;
}