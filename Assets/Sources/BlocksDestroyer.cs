using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksDestroyer
{
    public void Destroy(HashSet<Cell> cells)
    {
        foreach (Cell cell in cells)
        {
            if (cell == null) continue;

            cell.OwnedBlock.StartDestroy();
            cell.OwnedBlock = null;
        }
    }
}
