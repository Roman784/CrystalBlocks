using System.Collections.Generic;
using UnityEngine;

public class LineCleaner : MonoBehaviour
{
    public static LineCleaner Instance;

    private void Awake()
    {
        Instance = Singleton.Get<LineCleaner>();
    }

    public void DestroyBlocks()
    {
        HashSet<Cell> cells = GetCellsOnFilledLines();

        foreach (Cell cell in cells)
        {
            cell.OwnedBlock?.StartDestruction();
            cell.OwnedBlock = null;
        }

        EventBus.Instance.BlocksDestroyed.Invoke(cells.Count);
    }

    // Находит блоки на заполненных линиях и возвращает HashSet из клеток, где эти блоки находятся.
    private HashSet<Cell> GetCellsOnFilledLines()
    {
        HashSet<Cell> cellsOnFilledLines = new HashSet<Cell>();

        AddHorizontalBlocks(cellsOnFilledLines);
        AddVerticalBlocks(cellsOnFilledLines);

        return cellsOnFilledLines;
    }

    private void AddHorizontalBlocks(HashSet<Cell> cellsOnFilledLines) => AddBlocks(cellsOnFilledLines, true);
    private void AddVerticalBlocks(HashSet<Cell> cellsOnFilledLines) => AddBlocks(cellsOnFilledLines, false);

    // Добавляет блоки в переданный хешсет.
    private void AddBlocks(HashSet<Cell> cellsOnFilledLines, bool isHorizontal)
    {
        Cell[,] cellMatrix = Field.Instance.GetCellMatrix();

        for (int i = 0; i < cellMatrix.GetLength(0); i++)
        {
            List<Cell> cells = new List<Cell>(); // Список блоков на текущей линии.
            for (int j = 0; j < cellMatrix.GetLength(1); j++)
            {
                // Если на линии попалась пустая клетка.
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

        EventBus.Instance.BlocksDestroyed.Invoke(_destroyedBlocksCount);
    }
}
