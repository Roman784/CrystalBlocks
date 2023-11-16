using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Figure : MonoBehaviour
{
    [SerializeField] private Block[] _blocks;
    private Dictionary<Vector2Int, Block> _blocksByCoordinates = new Dictionary<Vector2Int, Block>(); // Ѕлоки по координатам относительно основного блока.
    public Block OriginBlock => _blocks[0]; // ќсновной блок фигуры, относительно которого всЄ расчитываетс€.

    [SerializeField] private float _distanceToPlaceBlock; // ѕогрешность при размещении блоков.

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

    // «аполн€ет словарь блоков и их координат.
    //  оординаты устанавливаютс€ относительно основного блока.
    private void InitBlocksCoordinates()
    {
        foreach (Block block in _blocks)
        {
            Vector2 relativePosition = new Vector2(block.transform.position.x - OriginBlock.transform.position.x, block.transform.position.y - OriginBlock.transform.position.y);
            Vector2Int coordinate = new Vector2Int(Mathf.RoundToInt(relativePosition.x + relativePosition.y), Mathf.RoundToInt(relativePosition.y - relativePosition.x));

            _blocksByCoordinates.Add(coordinate, block);
        }
    }

    // ѕоворачивает блоки в зависимости от поворота фигуры (чтобы блики были направлены в одну сторону).
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

    // –азмещает фигуру на поле и возвращает true или false в зависимости от результата.
    // true - фигура размещена, false - нет.
    public bool TryToPlaceFigure()
    {
        Cell originCell = GetNearestCell(); // ќсновной блок размещаетс€ на ближайшей клетке.
        Dictionary<Cell, Block> blocksByCell = GetBlocksByCell(originCell); //  летки и блоки, которые будут на них размещены.

        // ¬ случае, если не дл€ всех блоков нашлась клетка.
        if (blocksByCell.Count != _blocks.Length)
            return false;

        PlaceBlocks(blocksByCell);
        Destroy();

        Placed.Invoke();

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
            float distance = Vector2.Distance(OriginBlock.transform.position, cell.transform.position);
            if (distance < _distanceToPlaceBlock && distance < minDistance)
            {
                minDistance = distance;
                nearestCell = cell;
            }
        }

        return nearestCell;
    }

    // ¬озвращает словарь клеток и блоков, которые могут на них разместитьс€.
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
