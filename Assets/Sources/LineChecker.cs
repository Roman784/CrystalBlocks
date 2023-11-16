using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineChecker : MonoBehaviour
{
    // Находит блоки на заполненных линиях и возвращает HashSet из них.
    public HashSet<Block> GetBlocksOnFilledLines()
    {
        HashSet<Block> blocksOnFilledLines = new HashSet<Block>();
        Cell[,] cellMatrix = Field.Instance.GetCellMatrix();

        AddHorizontalBlocks(blocksOnFilledLines, cellMatrix);
        AddVerticalBlocks(blocksOnFilledLines, cellMatrix);
        
        return blocksOnFilledLines;
    }

    private void AddHorizontalBlocks(HashSet<Block> blocksOnFilledLines, Cell[,] cellMatrix)
    {
        AddBlocks(blocksOnFilledLines, cellMatrix, true);
    }

    private void AddVerticalBlocks(HashSet<Block> blocksOnFilledLines, Cell[,] cellMatrix)
    {
        AddBlocks(blocksOnFilledLines, cellMatrix, false);
    }

    private void AddBlocks(HashSet<Block> blocksOnFilledLines, Cell[,] cellMatrix, bool isHorizontal)
    {
        for (int i = 0; i < cellMatrix.GetLength(0); i++)
        {
            List<Block> blocks = new List<Block>();
            for (int j = 0; j < cellMatrix.GetLength(1); j++)
            {
                if (cellMatrix[i, j].IsEmpty && !isHorizontal || cellMatrix[j, i].IsEmpty && isHorizontal)
                {
                    blocks.Clear();
                    break;
                }

                if (!isHorizontal)
                    blocks.Add(cellMatrix[i, j].OwnedBlock);

                if (isHorizontal)
                    blocks.Add(cellMatrix[j, i].OwnedBlock);
            }

            blocksOnFilledLines.UnionWith(blocks);
        }
    }
}
