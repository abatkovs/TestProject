using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public abstract void Interact();
    
    public void OnTriggerEnter(Collider other)
    {
        Interact();
    }

    public void OnCollisionEnter(Collision other)
    {
        Interact();
    }
}
