using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TODO: Hex grid for easier placement of new islands
public class HexGrid : MonoBehaviour
{
    [SerializeField] private bool displayCoords = true;
    [SerializeField] private int width = 6;
    [SerializeField] private int height = 6;
    [SerializeField] private float distance = 2f;

    public HexCell cellPrefab;
    public TMP_Text cellLabelPrefab;
    [SerializeField] private Canvas gridCanvas;
    
    private HexCell[] _cells;
    
    void Awake ()
    {
        if (gridCanvas == null) gridCanvas = GetComponentInChildren<Canvas>();

        //GenerateGrid();
    }
    
    [ContextMenu("Create grid")]
    private void GenerateGrid()
    {
        _cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }
    
    [ContextMenu("Clear grid")]
    private void ClearGrid()
    {
        foreach (var cell in _cells)
        {
            Destroy(cell.gameObject);
            
        }
        Array.Clear(_cells,0,_cells.Length);
    }
    [ContextMenu("Reset Grid")]
    private void ResetGrid()
    {
        ClearGrid();
        GenerateGrid();
    }

    void CreateCell (int x, int z, int i) {
        Vector3 position;
        // ReSharper disable once PossibleLossOfFraction
        position.x = ((x + z * 0.5f - z / 2) * (HexMetrics.InnerRadius * 2f))/distance;
        position.y = 0f;
        position.z = (z * (HexMetrics.OuterRadius * 1.5f))/distance;

        HexCell cell = _cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        
        if (!displayCoords) return;
        TMP_Text label = Instantiate<TMP_Text>(cellLabelPrefab, gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = $"{x.ToString()} \n {z.ToString()}";
    }
}
