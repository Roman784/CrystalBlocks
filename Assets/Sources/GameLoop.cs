using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private LineChecker _lineChecker;
    [SerializeField] private BlocksDestroyer _blocksDestroyer;
    [SerializeField] private FigureSelectionPanel _figureSelectionPanel;

    private void Start()
    {
        Field.Instance.FigurePlaced.AddListener(StartLoop);
    }

    private void StartLoop()
    {
        HashSet<Block> blocksOnFilledLines = _lineChecker.Check();
        _blocksDestroyer.Destroy(blocksOnFilledLines);
        _figureSelectionPanel.CheckAvailability();
    }
}
