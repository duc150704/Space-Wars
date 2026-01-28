using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formations : MonoBehaviour
{
    [SerializeField] int _column;
    [SerializeField] float _horizontalSpace;
    [SerializeField] float _verticalSpace;
    [SerializeField] Vector3 _center;

    public Vector3 GetPositionInRectangle(int index)
    {
        int col = index % _column;
        int row = index / _column;

        float x = (col - (_column - 1) * 0.5f ) * _horizontalSpace;
        float y = row * _verticalSpace;

        return transform.position + new Vector3 (x + _center.x, _center.y - y, 0);
    }  

    public Vector3 GetPositionInRectangleFip(int index)
    {
        int col = index % _column;
        int row = index / _column;

        float x = (col - (_column - 1) * 0.5f) * _horizontalSpace;
        float y = row * _verticalSpace;

        return transform.position + new Vector3(_center.x - x, _center.y - y, 0);
    }
}
