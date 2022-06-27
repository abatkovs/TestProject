using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemText;

    public void SetTexture(Sprite itemSprite)
    {
        itemImage.transform.gameObject.SetActive(true);
        itemImage.sprite = itemSprite;
    }

    public void SetText(string text)
    {
        itemText.transform.gameObject.SetActive(true);
        itemText.text = text;
    }
}
