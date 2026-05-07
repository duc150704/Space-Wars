using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }

    Camera _mainCam;
    Vector2 _mousePos;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _mainCam = Camera.main;
    }

    private void Update()
    {
        GetMousePosition();
    }

    void GetMousePosition()
    {
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector2 MousePositon() => _mousePos;

    public bool IsShootinButtonPressed()
    {
        return Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space);
    }
}
