using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefeatChecker : MonoBehaviour
{
    public static UnityEvent Defeated = new UnityEvent();

    private void Start()
    {
        LineCleaner.BlocksDestroyed.AddListener(Check);
    }

    private void Check(int _)
    {
        if (!HasPlace())
        {
            Defeated.Invoke();
            SoundPlayer.Instance?.PlayDefeatSound();
        }
    }

    // Есть ли на поле место для размещения хотя бы одной имеющейся фигуры.
    private bool HasPlace()
    {
        Cell[] cells = Field.Instance.GetAllCells();
        List<Figure> figures = FigureSelectionPanel.Instance.GetFigures();

        foreach (Figure figure in figures)
        {
            FigurePlacement figurePlacement = figure.GetComponent<FigurePlacement>();
            foreach (Cell cell in cells)
            {
                bool canPlace = figurePlacement.CanPlace(cell, out Dictionary<Cell, Block> blocksByCell);
                if (canPlace) return true;
            }
        }

        return false;
    }
}
