using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private TMP_Text woodT1Txt;
    [SerializeField] private Item woodT1Item;
    
    private void Start()
    {
        woodT1Item.OnItemChange += UpdateUIItemCount;
    }

    private void UpdateUIItemCount()
    {
        woodT1Txt.text = $"{woodT1Item.GetCount()}";
    }
}
