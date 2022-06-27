using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopUpInfo : MonoBehaviour
{
    public string displayInfo;
    [SerializeField] private TMP_Text displayTxt;
    [SerializeField] private float timToLive = 1f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Image popUpImage;
    public Texture2D popUpTexture;
    private static readonly int TextureID = Shader.PropertyToID("_Texture");

    private void Start()
    {
        Destroy(gameObject, timToLive);
        displayTxt.text = displayInfo;
        popUpImage.material.SetTexture(TextureID, popUpTexture);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
