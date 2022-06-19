using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInput : MonoBehaviour
{
    private void Update()
    {
        if (!Touchscreen.current.primaryTouch.press.IsPressed()) return;
        var touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
        Debug.Log(touchPos);
    }
}
