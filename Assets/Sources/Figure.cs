using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField] private Block[] _blocks;
    private Dictionary<Vector2Int, Block> _blocksByCoordinates; // Ѕлоки по координатам относительно основного блока.
    public Block OriginBlock => _blocks[0]; // ќсновной блок фигуры, относительно которого всЄ расчитываетс€.

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
            Vector2Int coordinates = new Vector2Int((int)(relativePosition.x + relativePosition.y), (int)(relativePosition.y - relativePosition.x));

            _blocksByCoordinates.Add(coordinates, block);
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

        bool placementResult = Field.Instance.TryToPlaceFigure(this);

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

    public void Destroy()
    {
        FigureSelectionPanel.Instance.RemoveFigure(this);
        Destroy(gameObject);
    }

    public Block[] GetBlocks() => _blocks;
    public Dictionary<Vector2Int, Block> GetBlocksByCoordinates() => _blocksByCoordinates;
}
