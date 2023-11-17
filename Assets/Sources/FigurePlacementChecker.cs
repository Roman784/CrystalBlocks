using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FigurePlacementChecker : MonoBehaviour
{
    // Есть ли на поле место для размещения хотя бы одной имеющейся фигуры.
    public bool HasPlace()
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
