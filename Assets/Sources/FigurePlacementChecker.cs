using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FigurePlacementChecker : MonoBehaviour
{
    // ≈сть ли на поле место дл€ размещени€ хот€ бы одной имеющейс€ фигуры.
    public bool IsTherePlace()
    {
        Cell[,] cellMatrix = Field.Instance.GetCellMatrix();
        List<Figure> figures = FigureSelectionPanel.Instance.GetFigures();

        foreach (Figure figure in figures)
        {
            for (int x = 0; x < cellMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < cellMatrix.GetLength(1); y++)
                {
                    Vector2Int originCellCoordinates = new Vector2Int(x, y); //  оординаты клетки, относительно которой будет идти проверка.

                    if (CanPlaceFigure(figure, originCellCoordinates, cellMatrix)) return true;
                }
            }
        }

        return false;
    }

    // ћожем ли разместить фигуру поверх конкретной клетки.
    private bool CanPlaceFigure(Figure figure, Vector2Int originCellCoordinates, Cell[,] cellMatrix)
    {
        if (figure == null) return false;

        Vector2 originBlockPosition = figure.GetBlocks()[0].transform.position; // ѕозици€ блока, относительно которого будет идти проверка.

        foreach (Block block in figure.GetBlocks())
        {
            Vector2 blockCoords = (Vector2)block.transform.position - originBlockPosition; //  оординаты данного блока, относительно основного.
            Vector2Int cellCoordinates = new Vector2Int(originCellCoordinates.x + (int)blockCoords.x, originCellCoordinates.y + (int)blockCoords.y); //  оординаты клетки, относительно основной.

            // ѕроверка на выход за пределы пол€.
            if (cellCoordinates.x < 0 || cellCoordinates.x >= cellMatrix.GetLength(0)) return false;
            if (cellCoordinates.y < 0 || cellCoordinates.y >= cellMatrix.GetLength(1)) return false;

            // ћожем ли разместить данный блок.
            if (!cellMatrix[cellCoordinates.x, cellCoordinates.y].IsEmpty) return false;
        }

        return true;
    }
}
