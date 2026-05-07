using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : Projectiles
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool isDamaged = collision.gameObject.GetComponent<ShipHealth>().GetDamage();
            if (isDamaged && _canBeDestroy) 
            {
                PoolsManager.Instance.BackObjToPool(gameObject);
            }
        }
    }
}
