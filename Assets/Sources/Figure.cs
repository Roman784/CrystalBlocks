using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField] private Block[] _blocks;

    private bool _isMousePressed;
    private Vector2 _startMousePosition;

    [Space]

    [SerializeField] private float _revertSpeed;
    private Vector2 _initialPosition;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void Init(Vector2 initialPosition)
    {
        _initialPosition = initialPosition;
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

        bool placementResult = Field.Instance.TryToPlaceFigure(this);

        if (!placementResult)
        {
            StartCoroutine(RevertToInitialPosition());
        }
    }

    public void FollowMouse()
    {
        if (!_isMousePressed) return;

        Vector2 position = (Vector2)(_camera.ScreenToWorldPoint(Input.mousePosition)) - _startMousePosition;
        transform.position = position;
    }

    private IEnumerator RevertToInitialPosition()
    {
        while ((Vector2)transform.position != _initialPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, _initialPosition, _revertSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void Destroy()
    {
        FigureSelectionPanel.Instance.RemoveFigure(this);
        Destroy(gameObject);
    }

    public Block[] GetBlocks() => _blocks;
}
