using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Figure))]
public class FigureTransformation : MonoBehaviour
{
    [SerializeField] private Vector2 _reducedScale; // Размер фигуры на старте.

    private Figure _figure;

    private void Awake()
    {
        _figure = GetComponent<Figure>();
    }

    // Поворачивает блоки в зависимости от поворота фигуры (чтобы блики были направлены в одну сторону).
    public void RotateBlocks()
    {
        float angle = -transform.rotation.eulerAngles.z;
        foreach (Block block in _figure.GetBlocks())
        {
            block.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }

    public void ReduceScale()
    {
        transform.localScale = (Vector3)_reducedScale + Vector3.forward;
    }

    public void NormalizeScale()
    {
        transform.localScale = Vector3.one;
    }
}
