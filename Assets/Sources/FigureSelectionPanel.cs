using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FigureSelectionPanel : MonoBehaviour
{
    public static FigureSelectionPanel Instance;

    public static UnityEvent AvailabilityChecked = new UnityEvent();

    [SerializeField] private Figure[] _figurePrefabs;
    [SerializeField] private Transform[] _points; // “очки, на которых будут размещены фигуры.

    private List<Figure> _spawnedFigures = new List<Figure>();

    private float[] _rotateAngles = new float[4] { 0f, 90f , 180f, 270f };

    private void Awake()
    {
        Instance = Singleton.Get<FigureSelectionPanel>();

        CheckAvailability();
    }

    private void Start()
    {
        Figure.Placed.AddListener(DestroyFigure);
    }

    public void DestroyFigure(Figure figure)
    {
        _spawnedFigures.Remove(figure);
        figure.Destroy();

        CheckAvailability();
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

        AvailabilityChecked.Invoke();
    }

    private void CreateFigures()
    {
        _spawnedFigures.Clear();

        for (int i = 0; i < _points.Length; i++)
        {
            Figure newFigure = Instantiate(_figurePrefabs[Random.Range(0, _figurePrefabs.Length)]);

            float angle = _rotateAngles[Random.Range(0, _rotateAngles.Length)];
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            newFigure.Init(_points[i].position, rotation);

            _spawnedFigures.Add(newFigure);
        }
    }

    public List<Figure> GetFigures() => _spawnedFigures;
}
