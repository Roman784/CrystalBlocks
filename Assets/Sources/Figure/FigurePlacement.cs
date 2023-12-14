using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Figure))]
public class FigurePlacement : MonoBehaviour
{
    [SerializeField] private float _distanceToPlaceBlock; // ѕогрешность при размещении блоков.

    private Figure _figure;

    private void Awake()
    {
        _figure = GetComponent<Figure>();
    }

    public bool CanPlace(Cell originCell, out Dictionary<Cell, Block> blocksByCell)
    {
        blocksByCell = GetBlocksByCell(originCell);
        return blocksByCell.Count == _figure.GetBlocks().Length;
    }

    // –азмещает фигуру на поле и возвращает true или false в зависимости от результата.
    // true - фигура размещена, false - нет.
    public bool TryToPlace()
    {
        Cell originCell = GetNearestCell(); // ќсновной блок размещаетс€ на ближайшей клетке.
        Dictionary<Cell, Block> blocksByCell = new Dictionary<Cell, Block>(); //  летки и блоки, которые будут на них размещены.

        bool canPlace = CanPlace(originCell, out blocksByCell);

        if (!canPlace) return false;

        PlaceBlocks(blocksByCell);

        return true;
    }

    // –азмещает блоки на клетки согласно переданному словарю.
    private void PlaceBlocks(Dictionary<Cell, Block> blocksByCell)
    {
        foreach (var item in blocksByCell)
        {
            Cell cell = item.Key;
            Block block = item.Value;

            block.Place(cell);
        }
    }

    // ¬озвращает ближайшую к основному блоку клетку.
    private Cell GetNearestCell()
    {
        Cell[] cells = Field.Instance.GetAllCells();

        float minDistance = _distanceToPlaceBlock;
        Cell nearestCell = null;

        foreach (Cell cell in cells)
        {
            float distance = Vector2.Distance(_figure.OriginBlock.transform.position, cell.transform.position);
            if (distance < _distanceToPlaceBlock && distance < minDistance)
            {
                minDistance = distance;
                nearestCell = cell;
            }
        }

        return nearestCell;
    }

    // ¬озвращает словарь клеток и блоков, которые могут на них разместитьс€.
    public Dictionary<Cell, Block> GetBlocksByCell(Cell originCell)
    {
        Dictionary<Cell, Block> blocksByCell = new Dictionary<Cell, Block>();
        Cell[,] cellMatrix = Field.Instance.GetCellMatrix();

        if (originCell != null)
        {
            // ѕроходим по всем блокам фигуры и находим дл€ них подход€щие клетки.
            foreach (var item in _figure.GetBlocksByCoordinates())
            {
                Vector2Int blockCoordinate = item.Key;
                Block block = item.Value;

                //  оординаты клетки относительно основной.
                Vector2Int cellCoordinate = originCell.Coordinates + blockCoordinate;

                // ѕроверка на существование клетки.
                if (!Field.Instance.IsValidCell(cellCoordinate)) break;

                Cell cell = cellMatrix[cellCoordinate.x, cellCoordinate.y];

                if (cell.IsEmpty)
                    blocksByCell.Add(cell, block);
                else
                    break;
            }
        }

        return blocksByCell;
    }
}
