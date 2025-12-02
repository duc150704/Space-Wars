using System.Collections;
using UnityEngine;

public class Dreadnought : Enemy
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    public void Shoot1()
    {
        StartCoroutine(Shoot1_IE());
    }

    IEnumerator Shoot1_IE()
    {
        for(int i = 0;i <= 15; i++)
        {
            CreateAndRotate(Quaternion.Euler(0f, 0f, 40f) * _gunPosition[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 80f) * _gunPosition[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 120f) * _gunPosition[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 160f) * _gunPosition[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 200f) * _gunPosition[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 240f) * _gunPosition[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 280f) * _gunPosition[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 320f) * _gunPosition[0].right);
            CreateAndRotate(Quaternion.Euler(0f, 0f, 360f) * _gunPosition[0].right);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void CreateAndRotate(Vector3 direction)
    {
        GameObject go = base.CreateProjectile(_projectile[0], _gunPosition[0].position, Quaternion.identity);
        go.GetComponent<Projectiles>()?.RotateInDirection(direction);
    }

}
