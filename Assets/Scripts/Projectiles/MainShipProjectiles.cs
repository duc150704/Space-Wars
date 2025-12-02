using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShipProjectiles : Projectiles
{
    [SerializeField] float _damage;
    [SerializeField] GameObject _projectileExplEffect;
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageble damagebleObject = collision.gameObject.GetComponent<IDamageble>();
        if(damagebleObject != null)
        {
            damagebleObject.GetDamage(_damage);
            GameObject effect = Instantiate(_projectileExplEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
