using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy 
{
    IEnumerator Attack();
}

public abstract class AttackStrategy<T> : IAttackStrategy
{
    public readonly T Data;
    public BossController BossCtrl;

    public AttackStrategy(T data, BossController bossCtrl)
    {
        Data = data;
        BossCtrl = bossCtrl;
    }

    public abstract IEnumerator Attack();

    public void SpawnProjectile(GameObject projectile, Vector2 position, Vector2 direction, float speed)
    {
        GameObject go = PoolsManager.Instance.TakeObjFromPool(projectile);
        go.transform.SetPositionAndRotation(position, Quaternion.identity);
        go.transform.localScale = new Vector3(2f, 2f, 1f);
        go.GetComponent<Projectiles>()?.RotateInDirection(direction);
    }
}

public class CircleAttack : AttackStrategy<CircleAttackData>
{
    public CircleAttack(CircleAttackData data, BossController bossCtrl) : base(data, bossCtrl)
    {
    }

    public override IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(Data.TimeBetweenCircle);
        float angle = 360f / Data.ProjectilePerCircle;

        for (int i = 0; i < Data.CircleCount; i++)
        {
            Vector2 dir = Vector2.right;
            dir = Quaternion.AngleAxis(i * Data.AngleBetweenCircle, Vector3.forward) * dir;
            for (int j = 0; j < Data.ProjectilePerCircle; j++)
            {
                dir = Quaternion.AngleAxis(j * angle, Vector3.forward) * dir;
                SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, dir, Data.ProjectileSpeed);
            }
            yield return wait;
        }
    }
}

public class MissileAttack : AttackStrategy<MissileAttackData>
{
    public MissileAttack(MissileAttackData data, BossController boss) : base(data, boss)
    {
    }
    public override IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(Data.TimeBetweenWave);
        for (int i = 0; i < Data.WaveCount; i++)
        {
            Vector3 dir = (BossCtrl.GetPlayerPosition() - BossCtrl.Transform.position).normalized;
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, Quaternion.AngleAxis(15f, Vector3.forward) * dir, Data.ProjectileSpeed);
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, dir, Data.ProjectileSpeed);
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, Quaternion.AngleAxis(-15f, Vector3.forward) * dir, Data.ProjectileSpeed);
            yield return wait;
        }
    }
}

public class BulletAttack : AttackStrategy<BulletAttackData>
{
    BossController _enemy;

    public BulletAttack(BulletAttackData data, BossController boss) : base(data, boss)
    {
        
    }

    public override IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(Data.DelayTime);
        for (int i = 0; i < Data.WaveCount; i++)
        {
            for (int j = 0; j < Data.ProjectilePerWave; j++) 
            {
                Vector3 dir = (BossCtrl.GetPlayerPosition() - BossCtrl.Transform.position).normalized;
                SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, dir, Data.ProjectileSpeed);
                yield return wait;
            }

            yield return new WaitForSeconds(Data.TimeBetweenWave);
        }
    }
}

public class CrossAttack : AttackStrategy<CrossAttackData>
{
    public CrossAttack(CrossAttackData data, BossController boss) : base(data, boss) 
    {

    }

    public override IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(Data.TimeBetweenWave);
        for(int i = 0;i < Data.WaveCount; i++)
        {
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, Vector2.up, Data.ProjecctileSpeed);
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, Vector2.left, Data.ProjecctileSpeed);
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, Vector2.right, Data.ProjecctileSpeed);
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, Vector2.down, Data.ProjecctileSpeed);

            yield return wait;
        }
    }

}
