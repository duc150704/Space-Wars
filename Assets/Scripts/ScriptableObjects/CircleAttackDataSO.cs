using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CircleAttackDataSO", menuName = "CircleAttackDataSO")]
public class CircleAttackDataSO : ScriptableObject
{
    public float ProjectileSpeed = 5f;
    public float TimeBetweenCircle = 1f;
    public float AngleBetweenCircle = 15f;

    public int ProjectilePerCircle = 8;
    public int CircleCount = 5;

    public GameObject ProjectilePref;
}

public class MissileAttackDataSO: ScriptableObject
{
    public float ProjectileSpeed = 10f;
    public float TimeBetweenWave = 1;

    public int ProjectilePerWave = 3;
    public int WaveCount = 2;

    public GameObject ProjectilePref;
}

public class BulletAttackDataSO: ScriptableObject
{
    public float ProjectileSpeed = 10f;
    public float DelayTime = 0.1f;
    public float TimeBetweenWave = 1;

    public int ProjectilePerWave = 20;
    public int WaveCount = 2;

    public GameObject ProjectilePref;
}

public class CrossAttackDataSO : ScriptableObject
{
    public float ProjecctileSpeed = 10f;
    public float TimeBetweenWave = 1;

    public int ProjectilePerWave = 4;
    public int WaveCount = 2;

    public GameObject ProjectilePref;
}