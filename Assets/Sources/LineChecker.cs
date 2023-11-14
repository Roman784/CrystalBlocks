using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineChecker : MonoBehaviour
{
    // Ќаходит блоки на заполненных лини€х и возвращает HashSet из них.
    public HashSet<Block> Check()
    {
        HashSet<Block> blocksOnFilledLines = new HashSet<Block>();
        Cell[,] cellMatrix = Field.Instance.GetCellMatrix();

        // ƒобавление горизонтальных клеток.
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

                blocks.Add(cellMatrix[x, y].OwnedBlock);
            }

            blocksOnFilledLines.AddRange(blocks);
        }

        // ƒобавление вертикальных клеток.
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

            blocksOnFilledLines.AddRange(blocks);
        }

        return blocksOnFilledLines;
    }
}
