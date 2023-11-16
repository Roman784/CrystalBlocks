using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FigurePlacementChecker : MonoBehaviour
{
    // ���� �� �� ���� ����� ��� ���������� ���� �� ����� ��������� ������.
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
                    Vector2Int originCellCoordinates = new Vector2Int(x, y); // ���������� ������, ������������ ������� ����� ���� ��������.

                    if (CanPlaceFigure(figure, originCellCoordinates, cellMatrix)) return true;
                }
            }
        }

        return false;
    }

    // ����� �� ���������� ������ ������ ���������� ������.
    private bool CanPlaceFigure(Figure figure, Vector2Int originCellCoordinates, Cell[,] cellMatrix)
    {
        if (figure == null) return false;

        Vector2 originBlockPosition = figure.GetBlocks()[0].transform.position; // ������� �����, ������������ �������� ����� ���� ��������.

        foreach (Block block in figure.GetBlocks())
        {
            Vector2 blockCoords = (Vector2)block.transform.position - originBlockPosition; // ���������� ������� �����, ������������ ���������.
            Vector2Int cellCoordinates = new Vector2Int(originCellCoordinates.x + (int)blockCoords.x, originCellCoordinates.y + (int)blockCoords.y); // ���������� ������, ������������ ��������.

            // �������� �� ����� �� ������� ����.
            if (cellCoordinates.x < 0 || cellCoordinates.x >= cellMatrix.GetLength(0)) return false;
            if (cellCoordinates.y < 0 || cellCoordinates.y >= cellMatrix.GetLength(1)) return false;

            // ����� �� ���������� ������ ����.
            if (!cellMatrix[cellCoordinates.x, cellCoordinates.y].IsEmpty) return false;
        }

        return true;
    }
}
