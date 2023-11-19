using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLoop : MonoBehaviour
{
    public static UnityEvent Defeated = new UnityEvent();

    [SerializeField] private LineChecker _lineChecker;
    [SerializeField] private BlocksDestroyer _blocksDestroyer;
    [SerializeField] private FigureSelectionPanel _figureSelectionPanel;
    [SerializeField] private FigurePlacementChecker _figurePlacementChecker;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void Awake()
    {
        _lineChecker = new LineChecker();
        _blocksDestroyer = new BlocksDestroyer();
        _figurePlacementChecker = new FigurePlacementChecker();
    }

    private void Start()
    {
       Figure.Placed.AddListener(StartLoop);
    }

    private void StartLoop(Figure figure)
    {
        figure.Destroy();

        HashSet<Cell> cellsOnFilledLines = _lineChecker.GetCellsOnFilledLines();
        _blocksDestroyer.Destroy(cellsOnFilledLines);

        _scoreCounter.Increase(cellsOnFilledLines.Count);

        _figureSelectionPanel.CheckAvailability();

        bool isGameOver = !_figurePlacementChecker.HasPlace();
        if (isGameOver) Defeated.Invoke();
    }
}
