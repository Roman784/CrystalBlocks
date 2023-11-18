using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject _Shadow;

    public void Place(Cell cell)
    {
        cell.OwnedBlock = this;

        transform.SetParent(cell.transform);
        transform.localPosition = Vector3.zero;

        SetActiveShadow(false);
    }

    public void SetActiveShadow(bool value)
    {
        _Shadow.SetActive(value);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
