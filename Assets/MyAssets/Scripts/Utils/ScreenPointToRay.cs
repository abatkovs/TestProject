using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPointToRay : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector2 pos;

    private void Start()
    {
        UIVirtualJoystick.OnClick += ClickPos;
        cam = Camera.main;
    }

    private void OnDestroy()
    {
        UIVirtualJoystick.OnClick -= ClickPos;
    }

    private void ClickPos(Vector2 position)
    {
        this.pos = position;
        SelectionRay();
    }

    private void SelectionRay()
    {
        var ray = cam.ScreenPointToRay(pos);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask)) return;
        if (hitInfo.transform.TryGetComponent(out Shop shop))
        {
            shop.OnInteract();
        }
        Debug.Log(hitInfo.transform.name);
        Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.yellow);
    }
}
