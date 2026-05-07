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

    public GameObject GetProjectile(GameObject go, Vector2 position, float scale = 1f)
    {
        GameObject newObj = PoolsManager.Instance.TakeObjFromPool(go);
        newObj.transform.SetPositionAndRotation(position, Quaternion.identity);
        newObj.transform.localScale = new Vector3(scale, scale, 1f);

        return newObj;
    }

    public virtual void Fire(GameObject go, Vector2 direction, float speed = 0f)
    {
        go.GetComponent<Projectiles>()?.RotateInDirection(direction);
    }

    public IEnumerator Rotate(GameObject go, float duration, float speed)
    {
        Projectiles projectiles = go.GetComponent<Projectiles>();
        while(duration >= 0 && projectiles != null)
        {
            projectiles.Rotate(speed);
            duration += Time.deltaTime;
            yield return null;
        }
    }

    public virtual void SpawnProjectile(GameObject projectile, Vector2 position, Vector2 direction, float speed)
    {
        GameObject go = GetProjectile(projectile, position, 2f);
        Fire(go, direction, speed);
    }
}

public class CircleAttack : AttackStrategy<CircleAttackData>
{
    public CircleAttack(CircleAttackData data, BossController bossCtrl) : base(data, bossCtrl)
    {
    }

    public override IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(Data.TimeBetweenWave);
        float angle = 360f / Data.ProjectilePerWave;

        for (int i = 0; i < Data.WaveCount; i++)
        {
            Vector2 dir = Vector2.right;
            dir = Quaternion.AngleAxis(15f * i, Vector3.forward) * dir;
            for (int j = 0; j < Data.ProjectilePerWave; j++)
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
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, 
                Quaternion.AngleAxis(Data.AngleBetweenMissile, Vector3.forward) * dir, Data.ProjectileSpeed);
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, dir, Data.ProjectileSpeed);
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, 
                Quaternion.AngleAxis(-Data.AngleBetweenMissile, Vector3.forward) * dir, Data.ProjectileSpeed);
            yield return wait;
        }
    }
}

public class BulletAttack : AttackStrategy<BulletAttackData>
{
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
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, Vector2.up, Data.ProjectileSpeed);
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, Vector2.left, Data.ProjectileSpeed);
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, Vector2.right, Data.ProjectileSpeed);
            SpawnProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position, Vector2.down, Data.ProjectileSpeed);

            yield return wait;
        }
    }
}

public class LaserAttack : AttackStrategy<LaserAttackData>
{
    List<GameObject> _laserList = new List<GameObject>();
    BoxCollider2D _collider2D = new BoxCollider2D();

    public LaserAttack(LaserAttackData data, BossController boss) : base(data, boss) 
    { 
    }
    public override IEnumerator Attack()
    {
        int angleBetweenProjectile = 360 / Data.ProjectilePerTime;
        Vector2 dir = Vector2.right;

        for(int i = 0; i < Data.ProjectilePerTime; i++)
        {
            GameObject go = GetProjectile(Data.ProjectilePref, BossCtrl.GunPositions[0].position);
            BossCtrl.StartCoroutine(Scale(go));
            _laserList.Add(go);
            go.GetComponent<Projectiles>()?.RotateInDirection(Quaternion.Euler(0f, 0f, angleBetweenProjectile * (i + 1)) * dir);
            BossCtrl.StartCoroutine(LaserFire(go, Data.FireSpeed, Data.MaxSize));

            yield return null;
        }

        foreach (var item in _laserList)
        {
            BossCtrl.StartCoroutine(Rotate(item, Data.Duration, Data.RotateSpeed));
        }

        yield return new WaitForSeconds(Data.Duration);
        foreach (var go in _laserList) 
        { 
            PoolsManager.Instance.BackObjToPool(go);
        }
    }

    IEnumerator Scale(GameObject go)
    {
        float time = 2f;
        Vector3 originScale = go.transform.localScale;
        while(time > 0 && go)
        {
            time -= Time.deltaTime;
            go.transform.localScale = Vector3.Lerp(originScale, new Vector3(7,7,5), 0.5f);
            yield return null;
        }
    }

    IEnumerator LaserFire(GameObject go, float speed, Vector2 maxSize)
    {
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        BoxCollider2D collider2D = go.GetComponent<BoxCollider2D>();

        while (spriteRenderer != null && (spriteRenderer.size.y < maxSize.y))
        {
            Vector2 size = spriteRenderer.size;

            if (size.y < maxSize.y)
                size.y += speed * Time.deltaTime;

            spriteRenderer.size = size;
            collider2D.offset = new Vector2(spriteRenderer.size.x / 2, spriteRenderer.size.y / 2 );
            collider2D.size = size;

            yield return null;
        }
    }
}
