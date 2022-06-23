using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;
    public Inventory inventory;
    public Gear gear;
    public Transform playerGatherTargetPoint;
    
    private void Awake()
    {
        Instance = this;
    }
}
