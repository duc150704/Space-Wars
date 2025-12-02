using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        c();
    }

    public void c()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }

}
