using UnityEngine;

public class Block : MonoBehaviour
{
    public void Place(Cell cell)
    {
        cell.OwnedBlock = this;

        transform.SetParent(cell.transform);
        transform.localPosition = Vector3.zero;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
