using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Figure))]
public class FigurePlacement : MonoBehaviour
{
    [SerializeField] private float _distanceToPlaceBlock; // ����������� ��� ���������� ������.

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

    // ��������� ������ �� ���� � ���������� true ��� false � ����������� �� ����������.
    // true - ������ ���������, false - ���.
    public bool TryToPlace()
    {
        Cell originCell = GetNearestCell(); // �������� ���� ����������� �� ��������� ������.
        Dictionary<Cell, Block> blocksByCell = new Dictionary<Cell, Block>(); // ������ � �����, ������� ����� �� ��� ���������.

        bool canPlace = CanPlace(originCell, out blocksByCell);

        if (!canPlace) return false;

        PlaceBlocks(blocksByCell);

        return true;
    }

    // ��������� ����� �� ������ �������� ����������� �������.
    private void PlaceBlocks(Dictionary<Cell, Block> blocksByCell)
    {
        foreach (var item in blocksByCell)
        {
            Cell cell = item.Key;
            Block block = item.Value;

            block.Place(cell);
        }
    }

    // ���������� ��������� � ��������� ����� ������.
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

    // ���������� ������� ������ � ������, ������� ����� �� ��� ������������.
    public Dictionary<Cell, Block> GetBlocksByCell(Cell originCell)
    {
        Dictionary<Cell, Block> blocksByCell = new Dictionary<Cell, Block>();
        Cell[,] cellMatrix = Field.Instance.GetCellMatrix();

        if (originCell != null)
        {
            foreach (var item in _figure.GetBlocksByCoordinates())
            {
                Vector2Int blockCoordinate = item.Key;
                Block block = item.Value;

                Vector2Int cellCoordinate = originCell.Coordinate + blockCoordinate;

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
