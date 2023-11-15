using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSelectionPanel : MonoBehaviour
{
    public static FigureSelectionPanel Instance;

    [SerializeField] private Figure[] _figurePrefabs;
    [SerializeField] private Transform[] _points; // “очки, на которых будут размещены фигуры.

    [SerializeField]private List<Figure> _spawnedFigures = new List<Figure>();

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;

        CheckAvailability();
    }

    public void RemoveFigure(Figure figure)
    {
        _spawnedFigures.Remove(figure);
    }

    // ѕровер€ет количество фигур на панели, если их не осталось - создаЄт новые.
    public void CheckAvailability()
    {
        bool hasFigure = false;
        foreach (Figure figure in _spawnedFigures)
        {
            if (figure != null)
            {
                hasFigure = true;
                break;
            }
        }

        if (!hasFigure)
            CreateFigures();
    }

    private void CreateFigures()
    {
        _spawnedFigures.Clear();

        for (int i = 0; i < _points.Length; i++)
        {
            Figure newFigure = Instantiate(_figurePrefabs[Random.Range(0, _figurePrefabs.Length)]);
            newFigure.transform.position = _points[i].position;

            _spawnedFigures.Add(newFigure);
        }
    }
}
