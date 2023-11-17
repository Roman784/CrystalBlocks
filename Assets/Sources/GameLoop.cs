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
        HashSet<Block> blocksOnFilledLines = _lineChecker.GetBlocksOnFilledLines();
        _blocksDestroyer.Destroy(blocksOnFilledLines);
        _figureSelectionPanel.CheckAvailability();
        Debug.Log(_figurePlacementChecker.IsTherePlace());
    }
}
