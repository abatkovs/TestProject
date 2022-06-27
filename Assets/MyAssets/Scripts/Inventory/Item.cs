using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item", order = 0)]
public class Item : SerializedScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private int count;
    [SerializeField] private ResourceType resourceType;
    
    public event Action OnItemChange;
    
    public int GetCount() => count;
    public Sprite GetTexture() => icon;
    
    public void DecreaseCount()
    {
        count--;
        OnItemChange?.Invoke();
    }

    public void DecreaseCount(int itemCount)
    {
        count -= itemCount;
        OnItemChange?.Invoke();
    }

    public void IncreaseCount()
    {
        count++;
        OnItemChange?.Invoke();
    }

    public void ResetItem()
    {
        count = 0;
    }

    public void LoadItems()
    {
    }


}
