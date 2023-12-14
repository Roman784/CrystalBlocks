using UnityEngine;

public class Cell : MonoBehaviour
{
    public Block OwnedBlock;
    public bool IsEmpty => OwnedBlock == null;

    public Vector2Int Coordinates { get; private set; }

    public void Init(Vector2Int coordinates, Vector2 position)
    {
        Coordinates = coordinates;
        transform.position = position;
    }
}
