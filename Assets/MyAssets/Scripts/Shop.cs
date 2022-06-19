using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text priceTxt;
    [SerializeField] private Item requiredItem;
    [SerializeField] private int currPrice = 100;
    private int axeToolTier;
    private void Start()
    {
        axeToolTier = GameManager.Instance.gear.Axe.toolTier;
        priceTxt.text = $"{axeToolTier * 100} W";
        currPrice = axeToolTier * 100;
    }

    public void UpgradeAxe()
    {
        Debug.Log("Upgrade Item");
        if (requiredItem.GetCount() < currPrice) return;
        requiredItem.DecreaseCount(currPrice);
        GameManager.Instance.gear.Axe.toolTier++;
        axeToolTier = GameManager.Instance.gear.Axe.toolTier;
        priceTxt.text = $"{axeToolTier * 100} W";
        currPrice = axeToolTier * 100;
        
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpgradeAxe();
    }
}
