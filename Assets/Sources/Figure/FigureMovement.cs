using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Figure))]
public class FigureMovement : MonoBehaviour
{
    private bool _isMousePressed;
    private Vector2 _startMousePosition;
    [SerializeField] private Vector2 _movementOffset; // �������� ������������ �����, �� ������� �������� ������.

    [Space]

    [SerializeField] private float _revertSpeed; // �������� ����������� ������ �� ��������� �������, ���� ���������� �� �������.
    private Vector2 _initialPosition;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        FollowMouse();
    }

    public void SetInitialPosition(Vector2 newPosition)
    {
        _initialPosition = newPosition;
    }

    private void OnMouseDown()
    {
        _isMousePressed = true;
        _startMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnMouseUp()
    {
        _isMousePressed = false;
    }

    private void FollowMouse()
    {
        if (!_isMousePressed) return;

        Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition) - (Vector3)_startMousePosition + (Vector3)_movementOffset;
        position.z = -1f; // ��� �� �� ����� ����������� ������ ������ ���� ���� ���������.

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
