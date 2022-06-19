using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Inventory : SerializedMonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    public static event Action OnInventoryUpdate;
    private void Start()
    {
        LoadAllItems();
        ResetItems();
    }

    private void ResetItems()
    {
        foreach (var item in items)
        {
            item.ResetItem();
        }
    }

    private void LoadAllItems()
    {
        Debug.Log("Populate inventory item types");
        items.Clear();
        var inventoryItems = Resources.LoadAll<Item>("Data/Items");
        items.AddRange(inventoryItems);
    }

    public void AddItem(Item item)
    {
        item.IncreaseCount();
        OnInventoryUpdate?.Invoke();
    }
}
