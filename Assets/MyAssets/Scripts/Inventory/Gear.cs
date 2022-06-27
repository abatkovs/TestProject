using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [field: SerializeField] public Tool Pickaxe { get; private set; }
    [field: SerializeField] public Tool Axe { get; private set; }
    [field: SerializeField] public Tool Sword { get; private set; }
    [field: SerializeField] public Tool Armor { get; private set; }

}

[Serializable]
public class Tool
{
    public static event Action<ToolType> OnToolUpgrade;
    public ToolType toolType;
    public int toolTier;

    public void UpgradeToolTier()
    {
        toolTier++;
        OnToolUpgrade?.Invoke(toolType);
    }
}

public enum ToolType
{
    None,
    Axe,
    Pickaxe,
    Sword,
    Armor,
}