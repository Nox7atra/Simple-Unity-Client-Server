using System.Collections.Generic;
using UnityEngine;

public class Board
{

    private int _RowsCount;
    private int _ColumnsCount;
    private List<Cell> _Cells;

    public void CreateCells(GameObject prefab, int columnsCount, int rowsCount, Transform parent)
    {
        _Cells = new List<Cell>();
        _ColumnsCount = columnsCount;
        _RowsCount = rowsCount;
        for (int i = 0; i < rowsCount * columnsCount; i++)
        {
            _Cells.Add(GameObject.Instantiate(prefab, parent, false).GetComponent<Cell>());
        }

    }
    public Cell GetCell(int columnIndex, int rowIndex)
    {
        if(columnIndex > _ColumnsCount || columnIndex < 0)
        {
            Debug.Log(string.Concat("Column index more than capacity: ", columnIndex, " > ", _ColumnsCount));
            return null;
        }
        if(rowIndex > _RowsCount || rowIndex < 0)
        {
            Debug.Log(string.Concat("Row index more than capacity: ", rowIndex, " > ", _RowsCount));
            return null;
        }
        return _Cells[columnIndex + rowIndex * _ColumnsCount];
    }
}
