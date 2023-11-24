using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefeatChecker : MonoBehaviour
{
    public static UnityEvent Defeated = new UnityEvent();

    private void Start()
    {
        FigureSelectionPanel.AvailabilityChecked.AddListener(Check);
    }

    private void Check()
    {
        if (!HasPlace())
            Defeated.Invoke();
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
