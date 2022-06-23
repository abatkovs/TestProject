using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptions : MonoBehaviour, IUIInteract
{
    [SerializeField] private GameObject volume;
    [SerializeField] private GameObject close;
    public void Click()
    {
        //TODO: Pause game open options menu
        Debug.Log("Options open");
    }
}
