using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameStart : MonoBehaviour, IUIInteract
{

    [SerializeField] private GameObject startDisplay;
    [SerializeField] private CinemachineVirtualCamera startingVCam;
    [SerializeField] private GameObject[] otherUIElements;
    
    private void Start()
    {
        if (startDisplay == null) startDisplay = transform.gameObject;
    }

    public void Click()
    {
        startDisplay.SetActive(false);
        startingVCam.Priority = 0;

        foreach (var uiElement in otherUIElements)
        {
            uiElement.SetActive(true);
        }
    }
}
