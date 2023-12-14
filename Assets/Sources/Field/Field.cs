using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field Instance; 

    [SerializeField] private Cell[] _allCells;
    private Cell[,] _cellMatrix;

    [Space]

    private int _fieldSize;
    [SerializeField] private Vector2 _cellsPositionOffset;
    [SerializeField] private float _distanceBetweenCells;

    private void Awake()
    {
        Instance = Singleton.Get<Field>();

        InitField();
    }

    private void InitField()
    {
        // Определение размера поля и инициализация матрицы клеток.
        _fieldSize = Mathf.FloorToInt(Mathf.Sqrt(_allCells.Length));
        _cellMatrix = new Cell[_fieldSize, _fieldSize];

        // Заполнение матрицы.
        int cellIndex = 0;
        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                // Настройка клетки, установка координат и позиции.
                Vector2 position = new Vector2(x - y, x + y) * _distanceBetweenCells + _cellsPositionOffset;
                _allCells[cellIndex].Init(new Vector2Int(x, y), position);

                _cellMatrix[x, y] = _allCells[cellIndex];

                cellIndex++;
            }
        }
    }

    public Cell[] GetAllCells() => _allCells;
    public Cell[,] GetCellMatrix() => _cellMatrix;

    // Существует ли ячейка, проверяет, не выходят ли координаты за пределы поля.
    public bool IsValidCell(Vector2Int coordinate)
    {
        if (coordinate.x < 0 || coordinate.x >= _fieldSize) return false;
        if (coordinate.y < 0 || coordinate.y >= _fieldSize) return false;

        return true;
    }
}
