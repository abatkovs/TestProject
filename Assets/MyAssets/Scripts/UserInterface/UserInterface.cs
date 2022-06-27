using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInterface : MonoBehaviour, IPointerClickHandler
{
    private GameManager _gameManager;
    
    [Header("Gear levels")] 
    [SerializeField] private TMP_Text swordLvl;
    [SerializeField] private TMP_Text axeLvl;
    [SerializeField] private TMP_Text pickaxeLvl;
    [SerializeField] private TMP_Text armorLvl;

    [Header("Inventory")]
    [SerializeField] private UIButton closeInventoryBtn;
    [SerializeField] private UIButton openInventoryBtn;
    [SerializeField] private GameObject inventoryUI;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
        Tool.OnToolUpgrade += ToolUpgraded;
        closeInventoryBtn.OnClick += CloseInventory;
        openInventoryBtn.OnClick += OpenInventory;
    }

    private void OpenInventory()
    {
        inventoryUI.SetActive(true);
    }

    private void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }

    private void ToolUpgraded(ToolType toolType)
    {
        switch (toolType)
        {
            case ToolType.Pickaxe:
                pickaxeLvl.text = $"LVL {_gameManager.gear.Pickaxe.toolTier}";
                break;
            case ToolType.Axe:
                axeLvl.text = $"LVL {_gameManager.gear.Axe.toolTier}";
                break;
            case ToolType.Sword:
                swordLvl.text = $"LVL {_gameManager.gear.Sword.toolTier}";
                break;
            default:
                armorLvl.text = $"LVL {_gameManager.gear.Armor.toolTier}";
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var result = eventData.pointerCurrentRaycast;
        if (result.gameObject.TryGetComponent(out IUIInteract interact))
        {
            interact.Click();
        }
    }

    private void OnDestroy()
    {
        Tool.OnToolUpgrade -= ToolUpgraded;
    }
}
