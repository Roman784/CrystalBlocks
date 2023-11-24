using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineCleaner : MonoBehaviour
{
    public static UnityEvent<int> BlocksDestroyed = new UnityEvent<int>();
    private void Start()
    {
        Figure.Placed.AddListener(DestroyBlocks);
    }

    public void DestroyBlocks(Figure _)
    {
        HashSet<Cell> cells = GetCellsOnFilledLines();

        foreach (Cell cell in cells)
        {
            cell.OwnedBlock?.StartDestroy();
            cell.OwnedBlock = null;
        }

        BlocksDestroyed.Invoke(cells.Count);
    }

    // Находит блоки на заполненных линиях и возвращает HashSet из клеток, где эти блоки находятся.
    public HashSet<Cell> GetCellsOnFilledLines()
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
}
