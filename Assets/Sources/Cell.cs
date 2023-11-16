using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Block OwnedBlock;
    public bool IsEmpty => OwnedBlock == null;

    // Координаты клетки, представленные для использования в квадратной матрице.
    public Vector2Int Coordinates
    {
        get
        {
            Vector2 position = transform.localPosition;
            return new Vector2Int((int)(position.x + position.y), (int)(position.y - position.x));
        }
    }
}
