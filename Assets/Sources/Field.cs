using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Field : MonoBehaviour
{
    public static Field Instance; 

    [SerializeField] private Cell[] _allCells;
    private Cell[,] _cellMatrix;

    private void Awake()
    {
        Instance = Singleton.Get<Field>();

        InitField();
    }

    private void InitField()
    {
        Vector2Int minCellPosition = Vector2Int.zero;
        Vector2Int maxCellPosition = Vector2Int.zero;

        // Ќаходит минимальные и максимальные координаты позиций клеток.
        foreach (Cell cell in _allCells)
        {
            if (cell.Coordinate.x < minCellPosition.x) minCellPosition.x = cell.Coordinate.x;
            if (cell.Coordinate.y < minCellPosition.y) minCellPosition.y = cell.Coordinate.y;
            if (cell.Coordinate.x > maxCellPosition.x) maxCellPosition.x = cell.Coordinate.x;
            if (cell.Coordinate.y > maxCellPosition.y) maxCellPosition.y = cell.Coordinate.y;
        }

        Vector2Int fieldSize = maxCellPosition - minCellPosition + Vector2Int.one;

        _cellMatrix = new Cell[fieldSize.x, fieldSize.y];

        // «аполн€ет матрицу клеток.
        foreach (Cell cell in _allCells)
        {
            int x = cell.Coordinate.x;
            int y = cell.Coordinate.y;

            _cellMatrix[x, y] = cell;
        }
    }

    public Cell[] GetAllCells() => _allCells;
    public Cell[,] GetCellMatrix() => _cellMatrix;

    // —уществует ли €чейка, провер€ет, не выход€т ли координаты за пределы пол€.
    public bool IsValidCell(Vector2Int coordinate)
    {
        if (coordinate.x < 0 || coordinate.x >= _cellMatrix.GetLength(0)) return false;
        if (coordinate.y < 0 || coordinate.y >= _cellMatrix.GetLength(1)) return false;

        return true;
    }
}
