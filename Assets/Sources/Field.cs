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

    [SerializeField] private float _distanceToPlaceBlock; // ����������� ��� ���������� ������.

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

        // ������� ����������� � ������������ ���������� ������� ������.
        foreach (Cell cell in _allCells)
        {
            if (cell.Coordinates.x < minCellPosition.x) minCellPosition.x = cell.Coordinates.x;
            if (cell.Coordinates.y < minCellPosition.y) minCellPosition.y = cell.Coordinates.y;
            if (cell.Coordinates.x > maxCellPosition.x) maxCellPosition.x = cell.Coordinates.x;
            if (cell.Coordinates.y > maxCellPosition.y) maxCellPosition.y = cell.Coordinates.y;
        }

        Vector2Int fieldSize = maxCellPosition - minCellPosition + Vector2Int.one;

        _cellmatrix = new Cell[fieldSize.x, fieldSize.y];

        // ��������� ������� ������.
        foreach (Cell cell in _allCells)
        {
            int x = cell.Coordinates.x;
            int y = cell.Coordinates.y;

            _cellmatrix[x, y] = cell;
        }
    }

    public Cell[,] GetCellMatrix() => _cellmatrix;

    // ��������� ������ �� ���� � ���������� true ��� false � ����������� �� ����������.
    // true - ��� ����� ���������, false - ���� �� 1 ���� �� ��������.
    public bool TryToPlaceFigure(Figure figure)
    {
        Dictionary<Cell, Block> blocksByCell = new Dictionary<Cell, Block>(); // ������ � ����, ������� ����� �� ��� ��������.

        foreach (Block block in figure.GetBlocks())
        {
            bool canPlace = false; // ������� �� ������ ��� ���������� ������� �����.
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

            if (!canPlace) // ���� ���� �� 1 ���� �� ���� ������������, ��������� ����� �� �����������.
                return false;
        }

        PlaceBlocks(blocksByCell);
        figure.Destroy();

        FigurePlaced.Invoke();

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
}
