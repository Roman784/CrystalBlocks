using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field Instance; 

    [SerializeField] private Cell[] _allCells;
    private Cell[,] _cellMatrix;

    [Space]

    [SerializeField] private Vector2Int _fieldSize;
    [SerializeField] private Vector2 _cellsPositionOffset;
    [SerializeField] private float _distanceBetweenCells;

    private void Awake()
    {
        Instance = Singleton.Get<Field>();

        InitField();
    }

    private void InitField()
    {
        _cellMatrix = new Cell[_fieldSize.x, _fieldSize.y];

        int cellIndex = 0;
        for (int x = 0; x < _fieldSize.x; x++)
        {
            for (int y = 0; y < _fieldSize.y; y++)
            {
                Vector2 position = new Vector2(x - y, x + y) * _distanceBetweenCells + _cellsPositionOffset;
                _allCells[cellIndex].Init(new Vector2Int(x, y), position);

                _cellMatrix[x, y] = _allCells[cellIndex];

                cellIndex++;
            }
        }
    }

    public Cell[] GetAllCells() => _allCells;
    public Cell[,] GetCellMatrix() => _cellMatrix;

    // —уществует ли €чейка, провер€ет, не выход€т ли координаты за пределы пол€.
    public bool IsValidCell(Vector2Int coordinate)
    {
        if (coordinate.x < 0 || coordinate.x >= _fieldSize.x) return false;
        if (coordinate.y < 0 || coordinate.y >= _fieldSize.y) return false;

        return true;
    }
}
