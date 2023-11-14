using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineChecker : MonoBehaviour
{
    private void Start()
    {
        Field.Instance.FigurePlaced.AddListener(Check);
    }

    // Находит блоки на заполненных линиях.
    private void Check()
    {
        HashSet<Block> blocksInFilledLine = new HashSet<Block>();
        Cell[,] cellMatrix = Field.Instance.GetCellMatrix();

        // Добавление горизонтальных клеток.
        for (int y = 0; y < cellMatrix.GetLength(0); y++)
        {
            List<Block> blocks = new List<Block>();
            for (int x = 0; x < cellMatrix.GetLength(1); x++)
            {
                if (cellMatrix[x, y].IsEmpty)
                {
                    blocks.Clear();
                    break;
                }

                blocksInFilledLine.Add(cellMatrix[x, y].OwnedBlock);
            }

            blocks.AddRange(blocks);
        }

        // Добавление вертикальных клеток.
        for (int x = 0; x < cellMatrix.GetLength(0); x++)
        {
            List<Block> blocks = new List<Block>();
            for (int y = 0; y < cellMatrix.GetLength(1); y++)
            {
                if (cellMatrix[x, y].IsEmpty)
                {
                    blocks.Clear();
                    break;
                }

                blocks.Add(cellMatrix[x, y].OwnedBlock);
            }

            blocksInFilledLine.AddRange(blocks);
        }
    }
}
