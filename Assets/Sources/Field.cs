using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field Instance; 

    [SerializeField] private Vector3Int _fieldSize;
    private Cell[,] _cells;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;

        _cells = new Cell[_fieldSize.x, _fieldSize.y];
    }

    public bool TryToPlace(Block[] blocks)
    {
        return false;
    }
}
