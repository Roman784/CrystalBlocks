using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksDestroyer : MonoBehaviour
{
    public void Destroy(HashSet<Cell> cells)
    {
        foreach (Cell cell in cells)
        {
            if (cell == null) continue;

            StartCoroutine(cell.OwnedBlock.Destroy());
            cell.OwnedBlock = null;
        }
    }
}
