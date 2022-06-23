using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item", order = 0)]
public class Item : SerializedScriptableObject
{
    [SerializeField] private Image icon;
    [SerializeField] private int count;
    [SerializeField] private ResourceType resourceType;
    
    public event Action OnItemChange;
    
    public int GetCount() => count;
    public void DecreaseCount()
    {
        OnItemChange?.Invoke();
        count--;
    }

    public void IncreaseCount()
    {
        OnItemChange?.Invoke();
        count++;
    }

    public void ResetItem()
    {
        count = 0;
    }

    public void LoadItems()
    {
    }

    public void DecreaseCount(int count)
    {
        this.count -= count;
        OnItemChange?.Invoke();
    }
}
