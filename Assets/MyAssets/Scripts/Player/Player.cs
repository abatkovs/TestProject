using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Vector3 respawnPos;

    private void Start()
    {
        if (respawnPoint == null)
        {
            respawnPos = transform.position;
            return;
        }

        respawnPos = respawnPoint.position;
    }
}
