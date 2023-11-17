using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FigureMovement), typeof(FigurePlacement))]
public class Figure : MonoBehaviour
{
    public static UnityEvent<Figure> Placed = new UnityEvent<Figure>();

    [SerializeField] private Block[] _blocks;
    private Dictionary<Vector2Int, Block> _blocksByCoordinates = new Dictionary<Vector2Int, Block>(); // ����� �� ����������� ������������ ��������� �����.
    public Block OriginBlock => _blocks[0]; // �������� ���� ������, ������������ �������� �� �������������.

    private FigurePlacement _placement;
    private FigureMovement _movement;

    private void Awake()
    {
        _placement = GetComponent<FigurePlacement>();
        _movement = GetComponent<FigureMovement>();
    }

    public void Init(Vector2 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        _movement.SetInitialPosition(position);

        RotateBlocks();
        InitBlocksCoordinates();
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

    public void MouseUp()
    {
        bool placementResult = _placement.TryToPlace();

        if (placementResult)
        {
            Placed.Invoke(this);
        }
        else
        {
            StartCoroutine(_movement.RevertToInitialPosition());
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
