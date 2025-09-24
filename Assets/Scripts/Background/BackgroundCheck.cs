using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCheck : MonoBehaviour
{
    [SerializeField] float yPos;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -yPos)
        {
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }
    }
}
