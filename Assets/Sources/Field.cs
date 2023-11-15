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
        Instance = Singlton.Get<Field>();

        InitField();
    }

    private void InitField()
    {
        Vector2Int minCellPosition = Vector2Int.zero;
        Vector2Int maxCellPosition = Vector2Int.zero;

        // ������� ����������� � ������������ ���������� ������� ������.
        foreach (Cell cell in _allCells)
        {
            if (cell.transform.localPosition.x < minCellPosition.x) minCellPosition.x = (int)cell.transform.localPosition.x;
            if (cell.transform.localPosition.y < minCellPosition.y) minCellPosition.y = (int)cell.transform.localPosition.y;
            if (cell.transform.localPosition.x > maxCellPosition.x) maxCellPosition.x = (int)cell.transform.localPosition.x;
            if (cell.transform.localPosition.y > maxCellPosition.y) maxCellPosition.y = (int)cell.transform.localPosition.y;
        }

        Vector2Int fieldSize = maxCellPosition - minCellPosition + Vector2Int.one;

        _cellmatrix = new Cell[fieldSize.x, fieldSize.y];

        // ��������� ������� ������.
        foreach (Cell cell in _allCells)
        {
            int x = (int)cell.transform.localPosition.x;
            int y = (int)cell.transform.localPosition.y;

            _cellmatrix[x, y] = cell;
        }
    }

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
                if (distance < _distanceToPlaceBlock && cell.IsEmpty)
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

    public Cell[,] GetCellMatrix() => _cellmatrix;
}
