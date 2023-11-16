using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Figure : MonoBehaviour
{
    [SerializeField] private Block[] _blocks;
    private Dictionary<Vector2Int, Block> _blocksByCoordinates = new Dictionary<Vector2Int, Block>(); // ����� �� ����������� ������������ ��������� �����.
    public Block OriginBlock => _blocks[0]; // �������� ���� ������, ������������ �������� �� �������������.

    [SerializeField] private float _distanceToPlaceBlock; // ����������� ��� ���������� ������.

    public static UnityEvent Placed = new UnityEvent();

    [Space]

    private bool _isMousePressed;
    private Vector2 _startMousePosition;

    [Space]

    [SerializeField] private float _revertSpeed;
    private Vector2 _initialPosition;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void Init(Vector2 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        _initialPosition = position;

        RotateBlocks();
        InitBlocksCoordinates();
    }

    private void Update()
    {
        FollowMouse();
    } 

    // ��������� ������� ������ � �� ���������.
    // ���������� ��������������� ������������ ��������� �����.
    private void InitBlocksCoordinates()
    {
        foreach (Block block in _blocks)
        {
            Vector2 relativePosition = new Vector2(block.transform.position.x - OriginBlock.transform.position.x, block.transform.position.y - OriginBlock.transform.position.y);
            Vector2Int coordinate = new Vector2Int(Mathf.RoundToInt(relativePosition.x + relativePosition.y), Mathf.RoundToInt(relativePosition.y - relativePosition.x));

            _blocksByCoordinates.Add(coordinate, block);
        }
    }

    // ������������ ����� � ����������� �� �������� ������ (����� ����� ���� ���������� � ���� �������).
    private void RotateBlocks()
    {
        float angle = -transform.rotation.eulerAngles.z;
        foreach(Block block in _blocks)
        {
            block.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }

    public void OnMouseDown()
    {
        _isMousePressed = true;
        _startMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    public void OnMouseUp()
    {
        _isMousePressed = false;

        bool placementResult = TryToPlaceFigure();

        if (!placementResult)
        {
            StartCoroutine(RevertToInitialPosition());
        }
    }

    public void FollowMouse()
    {
        if (!_isMousePressed) return;

        Vector2 position = (Vector2)(_camera.ScreenToWorldPoint(Input.mousePosition)) - _startMousePosition;
        transform.position = position;
    }

    private IEnumerator RevertToInitialPosition()
    {
        while ((Vector2)transform.position != _initialPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, _initialPosition, _revertSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // ��������� ������ �� ���� � ���������� true ��� false � ����������� �� ����������.
    // true - ������ ���������, false - ���.
    public bool TryToPlaceFigure()
    {
        Cell originCell = GetNearestCell(); // �������� ���� ����������� �� ��������� ������.
        Dictionary<Cell, Block> blocksByCell = GetBlocksByCell(originCell); // ������ � �����, ������� ����� �� ��� ���������.

        // � ������, ���� �� ��� ���� ������ ������� ������.
        if (blocksByCell.Count != _blocks.Length)
            return false;

        PlaceBlocks(blocksByCell);
        Destroy();

        Placed.Invoke();

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
            float distance = Vector2.Distance(OriginBlock.transform.position, cell.transform.position);
            if (distance < _distanceToPlaceBlock && distance < minDistance)
            {
                minDistance = distance;
                nearestCell = cell;
            }
        }

        return nearestCell;
    }

    // ���������� ������� ������ � ������, ������� ����� �� ��� ������������.
    private Dictionary<Cell, Block> GetBlocksByCell(Cell originCell)
    {
        Dictionary<Cell, Block> blocksByCell = new Dictionary<Cell, Block>();
        Cell[,] cellMatrix = Field.Instance.GetCellMatrix();

        if (originCell != null)
        {
            foreach (var item in _blocksByCoordinates)
            {
                Vector2Int blockCoordinate = item.Key;
                Block block = item.Value;

                Vector2Int cellCoordinate = originCell.Coordinate + blockCoordinate;
                Cell cell = cellMatrix[cellCoordinate.x, cellCoordinate.y];

                if (Field.Instance.IsValidCell(cellCoordinate) && cell.IsEmpty)
                    blocksByCell.Add(cell, block);
            }
        }

        return blocksByCell;
    }

    public void Destroy()
    {
        FigureSelectionPanel.Instance.RemoveFigure(this);
        Destroy(gameObject);
    }

    public Block[] GetBlocks() => _blocks;
    public Dictionary<Vector2Int, Block> GetBlocksByCoordinates() => _blocksByCoordinates;
}
