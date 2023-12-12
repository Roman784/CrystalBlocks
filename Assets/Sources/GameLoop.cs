using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private FigureSelectionPanel _figureSelectionPanel;
    [SerializeField] private LineCleaner _lineCleaner;
    [SerializeField] private DefeatChecker _defeatChecker;

    private void Start()
    {
        EventBus.Instance.FigurePlaced.AddListener(StartLoop);
    }

    private void StartLoop(Figure placedFigure)
    {
        _figureSelectionPanel.DestroyFigure(placedFigure);
        _lineCleaner.DestroyBlocks();
        _defeatChecker.Check();
    }
}
