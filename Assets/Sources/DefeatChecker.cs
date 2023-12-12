using System.Collections.Generic;
using UnityEngine;

public class DefeatChecker : MonoBehaviour
{
    public void Check()
    {
        if (!HasPlace())
            EventBus.Instance.GameDefeated.Invoke();
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
