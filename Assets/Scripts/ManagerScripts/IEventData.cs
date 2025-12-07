using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventData { }

public class EnemyDeadEventData : IEventData 
{
    GameObject gameObject;
}
