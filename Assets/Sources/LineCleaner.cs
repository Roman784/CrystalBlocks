using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineCleaner : MonoBehaviour
{
    public static LineCleaner Instance;
    public static UnityEvent<int> BlocksDestroyed = new UnityEvent<int>();

    private void Awake()
    {
        Instance = Singleton.Get<LineCleaner>();
    }

    private void Start()
    {
        FigureSelectionPanel.AvailabilityChecked.AddListener(DestroyBlocks);
    }

    private void DestroyBlocks()
    {
        HashSet<Cell> cells = GetCellsOnFilledLines();

        foreach (Cell cell in cells)
        {
            cell.OwnedBlock?.StartDestruction();
            cell.OwnedBlock = null;
        }

        BlocksDestroyed.Invoke(cells.Count);
    }

    // Находит блоки на заполненных линиях и возвращает HashSet из клеток, где эти блоки находятся.
    private HashSet<Cell> GetCellsOnFilledLines()
    {
        HashSet<Cell> cellsOnFilledLines = new HashSet<Cell>();
        Cell[,] cellMatrix = Field.Instance.GetCellMatrix();

        AddHorizontalBlocks(cellsOnFilledLines, cellMatrix);
        AddVerticalBlocks(cellsOnFilledLines, cellMatrix);

        return cellsOnFilledLines;
    }

    private void AddHorizontalBlocks(HashSet<Cell> cellsOnFilledLines, Cell[,] cellMatrix)
    {
        AddBlocks(cellsOnFilledLines, cellMatrix, true);
    }

    private void AddVerticalBlocks(HashSet<Cell> cellsOnFilledLines, Cell[,] cellMatrix)
    {
        AddBlocks(cellsOnFilledLines, cellMatrix, false);
    }

    private void AddBlocks(HashSet<Cell> cellsOnFilledLines, Cell[,] cellMatrix, bool isHorizontal)
    {
        for (int i = 0; i < cellMatrix.GetLength(0); i++)
        {
            List<Cell> cells = new List<Cell>();
            for (int j = 0; j < cellMatrix.GetLength(1); j++)
            {
                if (cellMatrix[i, j].IsEmpty && !isHorizontal || cellMatrix[j, i].IsEmpty && isHorizontal)
                {
                    cells.Clear();
                    break;
                }

                if (!isHorizontal)
                    cells.Add(cellMatrix[i, j]);

                if (isHorizontal)
                    cells.Add(cellMatrix[j, i]);
            }

            cellsOnFilledLines.UnionWith(cells);
        }
    }

    // Вознаграждение за просмотр рекламы.
    public void DestroyMiddleBlocks()
    {
        Cell[,] cellMatrix = Field.Instance.GetCellMatrix();
        int _destroyedBlocksCount = 0;

        for (int i = 1; i < cellMatrix.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < cellMatrix.GetLength(1) - 1; j++)
            {
                if (!cellMatrix[i, j].IsEmpty)
                {
                    cellMatrix[i, j].OwnedBlock?.StartDestruction();
                    cellMatrix[i, j].OwnedBlock = null;

                    _destroyedBlocksCount += 1;
                }
            }
        }

        BlocksDestroyed.Invoke(_destroyedBlocksCount);
    }
}
