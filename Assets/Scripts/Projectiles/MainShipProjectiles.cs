using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShipProjectiles : Projectiles
{
    [SerializeField] float _damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if(go.tag == "Enemy")
        {
            go.GetComponent<Enemy>().GetDamage(_damage);
            Destroy(gameObject);
        }
    }
}
