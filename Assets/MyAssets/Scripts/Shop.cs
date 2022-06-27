using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private bool upgradeAxe;
    [SerializeField] private int currPrice = 3;
    [SerializeField] private int priceMulti = 30;
    [SerializeField] private TMP_Text currPriceTxt;
    [SerializeField] private int rewardCount = 1;
    [SerializeField] private TMP_Text rewardTxt;
    [SerializeField] private ToolType toolToUpgrade = ToolType.Axe;
    [SerializeField] private Item requiredResource;
    [SerializeField] private Item resourceToAdd;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private int queuedItemCount;
    [SerializeField] private TMP_Text queueCountTxt;
    [SerializeField] private float craftTime = 5f;
    [SerializeField] private float craftTimer;

    [SerializeField] private Image progressBar;

    [SerializeField] private GameObject progressGO;
    [SerializeField] private GameObject pricesGO;
    private GameObject _player;
    [SerializeField] private TriggerEvent trigerEvents;
    
    private void Start()
    {
        pricesGO.SetActive(false);
        trigerEvents.OnTriggerEnterEvent += EnterTrigger;
        trigerEvents.OnTriggerExitEvent += ExitTrigger;
        playerInventory = GameManager.Instance.inventory;
        _player = GameManager.Instance.player;
    }

    private void ExitTrigger()
    {
        Debug.Log("Exit");
        pricesGO.SetActive(false);
    }

    private void EnterTrigger()
    {
        Debug.Log("Enter");
        pricesGO.SetActive(true);
        
        if (upgradeAxe)
        {
            var tier = GameManager.Instance.gear.Axe.toolTier;
            currPrice = priceMulti * tier;
            rewardCount = tier + 1;
            rewardTxt.text = rewardCount.ToString();
            currPriceTxt.text = currPrice.ToString();
        }
    }

    private void Update()
    {
        if (queuedItemCount > 0)
        {
            progressGO.SetActive(true);
            craftTimer += Time.deltaTime;
            UpgradeProgressBar();
            return;
        }
        else
        {
            progressGO.SetActive(false);
        }
    }

    public void AddToCraftingQueue()
    {
        if (requiredResource.GetCount() < currPrice) return;
        Debug.Log("Add item to queue");
        requiredResource.DecreaseCount(currPrice);
        queuedItemCount++;
        queueCountTxt.text = queuedItemCount.ToString();
    }

    private void UpgradeProgressBar()
    {
        var fill = craftTimer / craftTime;
        progressBar.fillAmount = fill;
        if (craftTimer > craftTime)
        {
            craftTimer = 0;
            queuedItemCount--;
            queueCountTxt.text = queuedItemCount.ToString();
            
            if (upgradeAxe)
            {
                GameManager.Instance.gear.Axe.UpgradeToolTier();
                return;
            }
            
            playerInventory.AddItem(resourceToAdd);
        }
    }

    // public void UpgradeAxe()
    // {
    //     Debug.Log("Upgrade Item");
    //     if (requiredItem.GetCount() < currPrice) return;
    //     requiredItem.DecreaseCount(currPrice);
    //     GameManager.Instance.gear.Axe.UpgradeToolTier();
    //     _toolTier = GameManager.Instance.gear.Axe.toolTier;
    //     priceTxt.text = $"{_toolTier * 100} W";
    //     currPrice = _toolTier * 100;
    // }

    public void OnInteract()
    {
        AddToCraftingQueue();
    }


    private void OnDestroy()
    {
        trigerEvents.OnTriggerEnterEvent -= EnterTrigger;
        trigerEvents.OnTriggerExitEvent -= ExitTrigger;
    }
}
