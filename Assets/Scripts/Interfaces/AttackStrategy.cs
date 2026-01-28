using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStrategy
{
    public abstract IEnumerator Attack();

    public void SpawnProjectile(GameObject projectile, Vector2 position, Vector2 direction, float speed)
    {
        GameObject go = PoolsManager.Instance.TakeObjFromPool(projectile);
        go.transform.SetPositionAndRotation(position, Quaternion.identity);
        go.transform.localScale = new Vector3(2f, 2f, 1f);
        go.GetComponent<Projectiles>()?.RotateInDirection(direction);
    }
}

public class CircleAttack : AttackStrategy
{
    GameObject _projectile;
    Transform _gunPos;
    Vector2[] _direction;
    public CircleAttack(GameObject projectile, Transform gunPosition)
    {
        _projectile = projectile;
        _gunPos = gunPosition;
    }

    public override IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        //float angle = 360f / projectilePerCircle;

        //for (int i = 0; i < circleCount; i++)
        //{
        //    Vector2 dir = Vector2.right;
        //    for(int j = 0; j < projectilePerCircle; j++)
        //    {
        //        SpawnProjectile(_projectile, _gunPos.position, dir, speed);
        //    }
            yield return wait;
        //}
    }
}

public class MissileAttack 
{
    //BossController _enemy;

    //public MissileAttack(BossController enemy)
    //{
    //    _enemy = enemy;
    //}
    public IEnumerator Attack()
    {
    //    for(int i = 0; i < 2; i++)
    //    {
    //        Vector3 dir = enemy.GetPlayerPosition() - enemy.GunPositions[1].position;
    //        CreateAndRotate(Quaternion.Euler(0f, 0f, 10f) * dir);
    //        CreateAndRotate(Quaternion.Euler(0f, 0f, -10f) * dir);
    //        CreateAndRotate(dir);
            yield return new WaitForSeconds(1f);
    //    }
    }

    //void CreateAndRotate(Vector3 direction)
    //{
    //    GameObject go = PoolsManager.Instance.TakeObjFromPool(_enemy.Projectiles[1]);
    //    go.transform.SetPositionAndRotation(_enemy.GunPositions[1].position, Quaternion.identity);
    //    go.transform.localScale = new Vector3(2f, 2f, 1f);
    //    go.GetComponent<Projectiles>()?.RotateInDirection(direction);
    //}
}

public class BulletAttack
{
    BossController _enemy;

    public BulletAttack(BossController enemy)
    {
        _enemy = enemy;
    }

    public IEnumerator Attack()
    {
        //WaitForSeconds wait = new WaitForSeconds(0.1f);
        //for (int i = 0; i < 30; i++)
        //{
        //    Vector3 dir = enemy.GetPlayerPosition() - enemy.GunPositions[1].position;
        //    CreateAndRotate(dir);
        //    yield return wait;
        //    if (i == 20)
                yield return new WaitForSeconds(1f);
        //}
    }

    void CreateAndRotate(Vector3 direction)
    {
        GameObject go = PoolsManager.Instance.TakeObjFromPool(_enemy.Projectiles[2]);
        go.transform.SetPositionAndRotation(_enemy.GunPositions[1].position, Quaternion.identity);
        go.transform.localScale = new Vector3(2f, 2f, 1f);
        go.GetComponent<Projectiles>()?.RotateInDirection(direction);
    }
}

public class CrossAttack 
{
    public BossController _enemy;
    public CrossAttack(BossController enemy)
    {
        _enemy = enemy;
    }

    public IEnumerator Attack()
    {
        //for(int i = 0; i < 3; i++)
        //{
        //    CreateAndRotate(Vector3.right);
        //    CreateAndRotate(Vector3.up);
        //    CreateAndRotate(Vector3.left);
        //    CreateAndRotate(Vector3.down);
            yield return new WaitForSeconds(1f);
        //}
    }

    void CreateAndRotate(Vector3 direction)
    {
        GameObject go = PoolsManager.Instance.TakeObjFromPool(_enemy.Projectiles[0]);
        go.transform.SetPositionAndRotation(_enemy.GunPositions[1].position, Quaternion.identity);
        go.transform.localScale = new Vector3(2f, 2f, 1f);
        go.GetComponent<Projectiles>()?.RotateInDirection(direction);
    }
}
