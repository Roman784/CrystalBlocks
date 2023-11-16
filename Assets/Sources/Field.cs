using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Field : MonoBehaviour
{
    public static Field Instance; 

    [SerializeField] private Cell[] _allCells;
    private Cell[,] _cellmatrix;

    [Space]

    [SerializeField] private float _distanceToPlaceBlock; // Погрешность при размещении блоков.

    public UnityEvent FigurePlaced = new UnityEvent();

    private void Awake()
    {
        Instance = Singleton.Get<Field>();

        InitField();
    }

    private void InitField()
    {
        Vector2Int minCellPosition = Vector2Int.zero;
        Vector2Int maxCellPosition = Vector2Int.zero;

        // Находит минимальные и максимальные координаты позиций клеток.
        foreach (Cell cell in _allCells)
        {
            if (cell.Coordinates.x < minCellPosition.x) minCellPosition.x = cell.Coordinates.x;
            if (cell.Coordinates.y < minCellPosition.y) minCellPosition.y = cell.Coordinates.y;
            if (cell.Coordinates.x > maxCellPosition.x) maxCellPosition.x = cell.Coordinates.x;
            if (cell.Coordinates.y > maxCellPosition.y) maxCellPosition.y = cell.Coordinates.y;
        }

        Vector2Int fieldSize = maxCellPosition - minCellPosition + Vector2Int.one;

        _cellmatrix = new Cell[fieldSize.x, fieldSize.y];

        // Заполняет матрицу клеток.
        foreach (Cell cell in _allCells)
        {
            int x = cell.Coordinates.x;
            int y = cell.Coordinates.y;

            _cellmatrix[x, y] = cell;
        }
    }

    public Cell[,] GetCellMatrix() => _cellmatrix;

    // Размещает фигуру на поле и возвращает true или false в зависимости от результата.
    // true - все блоки размещены, false - хотя бы 1 блок не размещён.
    public bool TryToPlaceFigure(Figure figure)
    {
        Dictionary<Cell, Block> blocksByCell = new Dictionary<Cell, Block>(); // Клетка и блок, который будет на ней размещён.

        foreach (Block block in figure.GetBlocks())
        {
            bool canPlace = false; // Нашлась ли клетка для размещения данного блока.
            foreach (Cell cell in _allCells)
            {
                float distance = Vector2.Distance(block.transform.position, cell.transform.position);
                if (distance < _distanceToPlaceBlock && cell.IsEmpty && !blocksByCell.ContainsKey(cell))
                {
                    canPlace = true;
                    blocksByCell.Add(cell, block);

                    continue;
                }
            }

            if (!canPlace) // Если хотя бы 1 блок не смог разместиться, остальные также не размещаются.
                return false;
        }

        PlaceBlocks(blocksByCell);
        figure.Destroy();

        FigurePlaced.Invoke();

        return true;
    }

    // Размещает блоки на клетки согласно переданному словарю.
    private void PlaceBlocks(Dictionary<Cell, Block> blocksByCell)
    {
        foreach (var item in blocksByCell)
        {
            Cell cell = item.Key;
            Block block = item.Value;

            block.Place(cell);
        }
    }
}
