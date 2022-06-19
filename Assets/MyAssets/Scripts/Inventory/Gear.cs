using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [field: SerializeField] public Tool Pickaxe { get; private set; }
    [field: SerializeField] public Tool Axe { get; private set; }
    [field: SerializeField] public Tool Sword { get; private set; }

}

[Serializable]
public class Tool
{
    public ToolType toolType;
    public int toolTier;
}

public enum ToolType
{
    None,
    Axe,
    Pickaxe,
    Sword,
}