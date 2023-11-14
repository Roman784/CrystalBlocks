using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private LineChecker _lineChecker;
    [SerializeField] private BlocksDestroyer _blocksDestroyer;

    private void Start()
    {
        Field.Instance.FigurePlaced.AddListener(StartLoop);
    }

    private void StartLoop()
    {
        HashSet<Block> blocksOnFilledLines = _lineChecker.Check();
        _blocksDestroyer.Destroy(blocksOnFilledLines);
    }
}
