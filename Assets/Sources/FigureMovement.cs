using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Figure))]
public class FigureMovement : MonoBehaviour
{
    private bool _isMousePressed;
    private Vector2 _startMousePosition;

    [Space]

    [SerializeField] private float _revertSpeed; // Скорость возвращения фигуры на начальную позицию, если размещение не удалось.
    private Vector2 _initialPosition;

    private Figure _figure;
    private Camera _camera;

    public void SetInitialPosition(Vector2 newPosition) => _initialPosition = newPosition;

    private void Awake()
    {
        _figure = GetComponent<Figure>();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        FollowMouse();
    }

    public void OnMouseDown()
    {
        _isMousePressed = true;
        _startMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    public void OnMouseUp()
    {
        _isMousePressed = false;

        _figure.MouseUp();
    }

    public void FollowMouse()
    {
        if (!_isMousePressed) return;

        Vector2 position = (Vector2)(_camera.ScreenToWorldPoint(Input.mousePosition)) - _startMousePosition;
        transform.position = position;
    }

    public IEnumerator RevertToInitialPosition()
    {
        while ((Vector2)transform.position != _initialPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, _initialPosition, _revertSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
