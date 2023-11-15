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
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;

        InitField();
    }

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
                if (distance < _distanceToPlaceBlock && cell.IsEmpty)
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

    // Размещает блоки на клетки соответственно переданному словарю.
    private void PlaceBlocks(Dictionary<Cell, Block> blocksByCell)
    {
        foreach (var item in blocksByCell)
        {
            Cell cell = item.Key;
            Block block = item.Value;

            cell.OwnedBlock = block;
            block.transform.SetParent(cell.transform);
            block.transform.localPosition = Vector3.zero;
        }
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

        _cellmatrix = new Cell[fieldSize.x, fieldSize.y];

        foreach (Cell cell in _allCells)
        {
            int x = (int)cell.transform.localPosition.x;
            int y = (int)cell.transform.localPosition.y;

            _cellmatrix[x, y] = cell;
        }
    }

    public Cell[,] GetCellMatrix() => _cellmatrix;
}
