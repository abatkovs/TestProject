using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _mainCam;
    private void Start() {
        _mainCam = Camera.main;
    }
    private void LateUpdate() {
        transform.forward = _mainCam.transform.forward;
    }
}
