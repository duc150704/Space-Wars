using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy
{
    public IEnumerator Attack(BossController enemy);
}

public class WaveAttack : IAttackStrategy
{
    BossController _enemy;
    public WaveAttack(BossController enemy)
    {
        _enemy = enemy;
    }

    public IEnumerator Attack(BossController enemy)
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++)
        {
            CreateAndRotate(Quaternion.Euler(0f, 0f, 40f) * enemy.GunPositions[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 80f) * enemy.GunPositions[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 120f) * enemy.GunPositions[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 160f) * enemy.GunPositions[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 200f) * enemy.GunPositions[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 240f) * enemy.GunPositions[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 280f) * enemy.GunPositions[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 320f) * enemy.GunPositions[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 360f) * enemy.GunPositions[0].right);
            yield return wait;
        }
    }

    void CreateAndRotate(Vector3 direction)
    {
        GameObject go = PoolsManager.Instance.TakeObjFromPool(_enemy.Projectiles[0]);
        go.transform.SetPositionAndRotation(_enemy.GunPositions[0].position, Quaternion.identity);
        go.transform.localScale = new Vector3(2f, 2f, 1f);
        go.GetComponent<Projectiles>()?.RotateInDirection(direction);
    }
}

public class MissileAttack : IAttackStrategy
{
    BossController _enemy;

    public MissileAttack(BossController enemy)
    {
        _enemy = enemy;
    }
    public IEnumerator Attack(BossController enemy)
    {
        for(int i = 0; i < 2; i++)
        {
            Vector3 dir = enemy.GetPlayerPosition() - enemy.GunPositions[1].position;
            CreateAndRotate(Quaternion.Euler(0f, 0f, 10f) * dir);
            CreateAndRotate(Quaternion.Euler(0f, 0f, -10f) * dir);
            CreateAndRotate(dir);
            yield return new WaitForSeconds(1f);
        }
    }

    void CreateAndRotate(Vector3 direction)
    {
        GameObject go = PoolsManager.Instance.TakeObjFromPool(_enemy.Projectiles[1]);
        go.transform.SetPositionAndRotation(_enemy.GunPositions[1].position, Quaternion.identity);
        go.transform.localScale = new Vector3(2f, 2f, 1f);
        go.GetComponent<Projectiles>()?.RotateInDirection(direction);
    }
}

public class BulletAttack : IAttackStrategy
{
    BossController _enemy;

    public BulletAttack(BossController enemy)
    {
        _enemy = enemy;
    }

    public IEnumerator Attack(BossController enemy)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        for (int i = 0; i < 30; i++)
        {
            Vector3 dir = enemy.GetPlayerPosition() - enemy.GunPositions[1].position;
            CreateAndRotate(dir);
            yield return wait;
            if (i == 20)
                yield return new WaitForSeconds(1f);
        }
    }

    void CreateAndRotate(Vector3 direction)
    {
        GameObject go = PoolsManager.Instance.TakeObjFromPool(_enemy.Projectiles[2]);
        go.transform.SetPositionAndRotation(_enemy.GunPositions[1].position, Quaternion.identity);
        go.transform.localScale = new Vector3(2f, 2f, 1f);
        go.GetComponent<Projectiles>()?.RotateInDirection(direction);
    }
}

public class CrossAttack : IAttackStrategy
{
    public BossController _enemy;
    public CrossAttack(BossController enemy)
    {
        _enemy = enemy;
    }

    public IEnumerator Attack(BossController enemy)
    {
        for(int i = 0; i < 3; i++)
        {
            CreateAndRotate(Vector3.right);
            CreateAndRotate(Vector3.up);
            CreateAndRotate(Vector3.left);
            CreateAndRotate(Vector3.down);
            yield return new WaitForSeconds(1f);
        }
    }

    void CreateAndRotate(Vector3 direction)
    {
        GameObject go = PoolsManager.Instance.TakeObjFromPool(_enemy.Projectiles[0]);
        go.transform.SetPositionAndRotation(_enemy.GunPositions[1].position, Quaternion.identity);
        go.transform.localScale = new Vector3(2f, 2f, 1f);
        go.GetComponent<Projectiles>()?.RotateInDirection(direction);
    }
}
