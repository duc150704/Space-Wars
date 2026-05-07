using UnityEngine;
using UnityEngine.UIElements;

public interface IData { }

public class TransformData : IData
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }

    public TransformData(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    public TransformData(Transform t) 
    {
        Position = t.position;
        Rotation = t.rotation;
        Scale = t.localScale;
    }
}