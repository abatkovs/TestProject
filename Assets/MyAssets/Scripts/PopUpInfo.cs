using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpInfo : MonoBehaviour
{
    public string displayInfo;
    [SerializeField] private TMP_Text displayTxt;
    [SerializeField] private float timToLive = 1f;
    [SerializeField] private float speed = 10f;
    
    private void Start()
    {
        Destroy(gameObject, timToLive);
        displayTxt.text = displayInfo;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
