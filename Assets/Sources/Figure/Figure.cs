using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FigureMovement), typeof(FigurePlacement), typeof(FigureTransformation))]
public class Figure : MonoBehaviour
{
    public static UnityEvent<Figure> Placed = new UnityEvent<Figure>();

    [SerializeField] private Block[] _blocks;
    private Dictionary<Vector2Int, Block> _blocksByCoordinates = new Dictionary<Vector2Int, Block>(); // Ѕлоки по координатам относительно основного блока.
    public Block OriginBlock => _blocks[0]; // ќсновной блок фигуры, относительно которого всЄ расчитываетс€.

    private FigurePlacement _placement;
    private FigureMovement _movement;
    private FigureTransformation _transformation;

    private void Awake()
    {
        _placement = GetComponent<FigurePlacement>();
        _movement = GetComponent<FigureMovement>();
        _transformation = GetComponent<FigureTransformation>();
    }

    public void Init(Vector2 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        _movement.SetInitialPosition(position);
        _transformation.RotateBlocks();
        _transformation.ReduceScale();

        InitBlocksCoordinates();
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

    private void OnMouseDown()
    {
        _transformation.NormalizeScale();
    }

    private void OnMouseUp()
    {
        bool placementResult = _placement.TryToPlace();

        if (placementResult)
        {
            Placed.Invoke(this);
        }
        else
        {
            _transformation.ReduceScale();
            StartCoroutine(_movement.RevertToInitialPosition());
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public Block[] GetBlocks() => _blocks;
    public Dictionary<Vector2Int, Block> GetBlocksByCoordinates() => _blocksByCoordinates;
}
