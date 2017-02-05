using System.Collections.Generic;
using UnityEngine;

namespace Nox7atra.Core.Gameplay
{
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
                _Cells.Add(Cell.CreateCell(prefab, parent, this));
            }

        }
        public Cell GetCell(int columnIndex, int rowIndex)
        {
            if (columnIndex > _ColumnsCount || columnIndex < 0)
            {
                Debug.Log(string.Concat("Column index more than capacity: ", columnIndex, " > ", _ColumnsCount));
                return null;
            }
            if (rowIndex > _RowsCount || rowIndex < 0)
            {
                Debug.Log(string.Concat("Row index more than capacity: ", rowIndex, " > ", _RowsCount));
                return null;
            }
            return _Cells[columnIndex + rowIndex * _ColumnsCount];
        }
        public int[] GetCellPosition(Cell cell)
        {
            int[] pos = new int[2];
            int index = _Cells.FindIndex(x => x == cell);
            pos[0] = index % _ColumnsCount;
            pos[1] = index / _ColumnsCount;
            return pos;
        }
        public bool IsBoardEnded()
        {
            int freeCellsCount = _Cells.FindAll(x => x.PlayerIndex == -1).Count;
            return freeCellsCount == 0;
        }
    }
}