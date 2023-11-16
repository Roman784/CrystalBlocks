using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSelectionPanel : MonoBehaviour
{
    public static FigureSelectionPanel Instance;

    [SerializeField] private Figure[] _figurePrefabs;
    [SerializeField] private Transform[] _points; // �����, �� ������� ����� ��������� ������.

    [SerializeField]private List<Figure> _spawnedFigures = new List<Figure>();

    private float[] _rotateAngles = new float[4] { 0f, 90f , 180f, 270f };

    private void Awake()
    {
        Instance = Singleton.Get<FigureSelectionPanel>();

        CheckAvailability();
    }

    public void RemoveFigure(Figure figure)
    {
        _spawnedFigures.Remove(figure);
    }

    // ��������� ���������� ����� �� ������, ���� �� �� �������� - ������ �����.
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
            newFigure.Init(_points[i].position);

            float angle = _rotateAngles[Random.Range(0, _rotateAngles.Length)];

            newFigure.transform.position = _points[i].position;
            newFigure.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            _spawnedFigures.Add(newFigure);
        }
    }

    public List<Figure> GetFigures() => _spawnedFigures;
}
