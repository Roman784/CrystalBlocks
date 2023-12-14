using System.Collections.Generic;
using UnityEngine;

public class FigureSelectionPanel : MonoBehaviour
{
    public static FigureSelectionPanel Instance;

    [SerializeField] private Figure[] _figurePrefabs;
    [SerializeField] private Transform[] _points; // Точки, на которых будут размещены фигуры.

    private List<Figure> _spawnedFigures = new List<Figure>();

    private float[] _rotateAngles = new float[4] { 0f, 90f , 180f, 270f };

    private void Awake()
    {
        Instance = Singleton.Get<FigureSelectionPanel>();

        CheckAvailability();
    }

    // Уничтожает переданную фигуру и проверяет необходимость создания новых.
    public void DestroyFigure(Figure figure)
    {
        _spawnedFigures.Remove(figure);
        figure.Destroy();

        CheckAvailability();
    }

    // Проверяет количество фигур на панели, если их не осталось - создаёт новые.
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

        EventBus.Instance.FigureAvailabilityChecked.Invoke();
    }

    // Создаёт новые фигуры фигуры на каждой точке.
    private void CreateFigures()
    {
        _spawnedFigures.Clear();

        // Проходит по всем точкам и создаёт на них фигуры.
        foreach(Transform point in _points)
        {
            Figure newFigure = Instantiate(_figurePrefabs[Random.Range(0, _figurePrefabs.Length)]);

            // Определение поворота фигуры.
            float angle = _rotateAngles[Random.Range(0, _rotateAngles.Length)];
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            newFigure.Init(point.position, rotation);

            _spawnedFigures.Add(newFigure);
        }
    }

    public List<Figure> GetFigures() => _spawnedFigures;
}
