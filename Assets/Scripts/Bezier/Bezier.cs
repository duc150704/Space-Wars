using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    [SerializeField] Transform _p0;
    [SerializeField] Transform _p1;
    [SerializeField] Transform _p2;
    [SerializeField] Transform _p3;

    public Vector3 Move(float t)
    {
        Mathf.Clamp01(t);
        return Mathf.Pow(1 - t, 3) * _p0.position
            + 3 * t * Mathf.Pow(1 - t, 2) * _p1.position
            + 3 * t * t * (1 - t) * _p2.position
            + t * t * t * _p3.position;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_p0.position, 0.5f);
        Gizmos.DrawSphere(_p1.position, 0.5f);
        Gizmos.DrawSphere(_p2.position, 0.5f);
        Gizmos.DrawSphere(_p3.position, 0.5f);

        Gizmos.DrawLine(_p0.position, _p1.position);
        Gizmos.DrawLine(_p1.position, _p2.position);
        Gizmos.DrawLine(_p2.position, _p3.position);

        Gizmos.color = Color.green;

        float time = 2f;

        for (float timeCounter = 0f; timeCounter <= time; timeCounter += 0.05f)
        {
            Vector3 point = Move(timeCounter / time);
            Gizmos.DrawSphere(point, 0.1f);
        }

    }


}
