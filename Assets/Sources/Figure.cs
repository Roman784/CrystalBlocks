using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField] private Block[] _blocks;

    private bool _isMouseDown;
    private Vector2 _startMousePosition;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Move();
    }

    public void OnMouseDown()
    {
        _isMouseDown = true;
        _startMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    public void OnMouseUp()
    {
        _isMouseDown = false;

        Field.Instance.TryToPlace(_blocks);
    }

    public void Move()
    {
        if (_isMouseDown == false) return;

        Vector2 position = (Vector2)(_camera.ScreenToWorldPoint(Input.mousePosition)) - _startMousePosition;
        transform.position = position;
    }
}
