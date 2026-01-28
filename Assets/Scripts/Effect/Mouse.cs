using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;
        transform.position = InputManager.Instance.MousePositon();
    }
}
