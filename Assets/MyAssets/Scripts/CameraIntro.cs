using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraIntro : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera startingVcam;

    private void Start()
    {
        startingVcam.Priority = 1;
    }
}
