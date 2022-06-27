using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour, IUIInteract
{

    public event Action OnClick;

    public void Click()
    {
        Debug.Log("Btn click event");
        OnClick?.Invoke();
    }
}
