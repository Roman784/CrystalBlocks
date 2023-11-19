using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private LineChecker _lineChecker;
    [SerializeField] private BlocksDestroyer _blocksDestroyer;
    [SerializeField] private FigureSelectionPanel _figureSelectionPanel;
    [SerializeField] private FigurePlacementChecker _figurePlacementChecker;

    private void Start()
    {
       Figure.Placed.AddListener(StartLoop);
    }

    private void StartLoop(Figure figure)
    {
        figure.Destroy();
        HashSet<Cell> cellsOnFilledLines = _lineChecker.GetCellsOnFilledLines();
        _blocksDestroyer.Destroy(cellsOnFilledLines);
        _figureSelectionPanel.CheckAvailability();
        Debug.Log(_figurePlacementChecker.HasPlace());
    }
}
