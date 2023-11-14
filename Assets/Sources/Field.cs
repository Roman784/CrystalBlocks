using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field Instance; 

    [SerializeField] private Cell[] _allCells;
    private Cell[,] _cells;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;

        InitField();
    }

    public bool TryToPlace(Block[] blocks)
    {
        return false;
    }

    private void InitField()
    {
        Vector2Int fieldSize = Vector2Int.zero;
        Vector2Int minCellPosition = Vector2Int.zero;
        Vector2Int maxCellPosition = Vector2Int.zero;

        foreach (Cell cell in _allCells)
        {
            if (cell.transform.localPosition.x < minCellPosition.x) minCellPosition.x = (int)cell.transform.localPosition.x;
            if (cell.transform.localPosition.y < minCellPosition.y) minCellPosition.y = (int)cell.transform.localPosition.y;
            if (cell.transform.localPosition.x > maxCellPosition.x) maxCellPosition.x = (int)cell.transform.localPosition.x;
            if (cell.transform.localPosition.y > maxCellPosition.y) maxCellPosition.y = (int)cell.transform.localPosition.y;
        }

        fieldSize = maxCellPosition - minCellPosition + Vector2Int.one;

        _cells = new Cell[fieldSize.x, fieldSize.y];
    }    
}
