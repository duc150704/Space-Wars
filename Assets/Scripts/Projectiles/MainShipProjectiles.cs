using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShipProjectiles : Projectiles
{
    [SerializeField] float _damage;
    [SerializeField] GameObject _projectileExplEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageble damagebleObject = collision.gameObject.GetComponent<IDamageble>();
        if(damagebleObject != null)
        {
            damagebleObject.GetDamage(_damage);
            GameObject effect = PoolsManager.Instance.TakeObjFromPool(_projectileExplEffect, new TransformData(transform));
            PoolsManager.Instance.BackObjToPool(gameObject);
        }
    }
}
