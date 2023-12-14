using System.Collections.Generic;
using UnityEngine;

public class DefeatChecker : MonoBehaviour
{
    public void Check()
    {
        if (!HasPlace())
            EventBus.Instance.GameDefeated.Invoke();
    }

    // ≈сть ли на поле место дл€ размещени€ хот€ бы одной имеющейс€ фигуры.
    private bool HasPlace()
    {
        Cell[] cells = Field.Instance.GetAllCells();
        List<Figure> figures = FigureSelectionPanel.Instance.GetFigures();

        // ѕроходим по каждой доступной фигуре.
        foreach (Figure figure in figures)
        {
            FigurePlacement figurePlacement = figure.GetComponent<FigurePlacement>();
            // ѕроверка, возможно ли на текущей клетке разместить данную фигуру.
            foreach (Cell cell in cells)
            {
                bool canPlace = figurePlacement.CanPlace(cell, out Dictionary<Cell, Block> blocksByCell);
                if (canPlace) return true;
            }
        }

        return false;
    }
}
