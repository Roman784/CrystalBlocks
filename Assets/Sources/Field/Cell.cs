using UnityEngine;

public class Cell : MonoBehaviour
{
    public Block OwnedBlock;
    public bool IsEmpty => OwnedBlock == null;

    // ���������� ������, �������������� ��� ������������� � ���������� �������.
    public Vector2Int Coordinate
    {
        get
        {
            Vector2 position = transform.localPosition;
            return new Vector2Int(Mathf.RoundToInt(position.x + position.y), Mathf.RoundToInt(position.y - position.x));
        }
    }
}
