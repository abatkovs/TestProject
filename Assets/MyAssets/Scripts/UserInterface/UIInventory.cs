using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private UIItem itemPrefabUI;
    [SerializeField] private List<UIItem> inventoryItems;
    [SerializeField] private GameObject inventoryContainer;
    [Header("Fit to screen")]
    [SerializeField] private RectTransform itemContainerRectTransform;
    [SerializeField] private float fromSides = -50;
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private int rows = 5;
    
    private void Start()
    {
        SetHeight();
        SetGridLayoutSizes();
    }

    private void OnEnable()
    {
        Debug.Log("Enabled");
        PopulateUIItems();
    }
    
    private void SetGridLayoutSizes()
    {
        var width = itemContainerRectTransform.rect.width - (gridLayout.padding.left * 2);
        var height = itemContainerRectTransform.rect.height - (gridLayout.padding.bottom + gridLayout.padding.top);
        var cellSize = new Vector2(width / rows, height / rows);
        gridLayout.cellSize = cellSize;
        //gridLayout.cellSize = new Vector2()
    }

    private void SetHeight()
    {
        float halfScreenHeight = Screen.height / 2f;
        itemContainerRectTransform.sizeDelta = new Vector2(fromSides, halfScreenHeight);
    }

    private void PopulateUIItems()
    {
        ClearItems();
        var playerItems = GameManager.Instance.inventory.GetInventoryItems();

        foreach (var item in playerItems)
        {
            if (item.GetCount() == 0) continue;
            UIItem uiItem = Instantiate(itemPrefabUI, Vector3.zero, Quaternion.identity, inventoryContainer.transform);
            uiItem.SetTexture(item.GetTexture());
            uiItem.SetText(item.GetCount().ToString());
            inventoryItems.Add(uiItem);
        }
    }

    private void ClearItems()
    {
        foreach (var item in inventoryItems)
        {
            Destroy(item.transform.gameObject);
        }
        inventoryItems.Clear();
    }
}
