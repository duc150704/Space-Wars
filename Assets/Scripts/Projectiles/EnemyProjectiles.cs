using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : Projectiles
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<ShipController>().HasShield)
            {
                return;
            }
            PoolsManager.Instance.BackObjToPool(gameObject);
            collision.gameObject.GetComponent<ShipController>().Destruction();
        }
    }
}
