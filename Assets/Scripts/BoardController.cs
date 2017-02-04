using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : Singleton<BoardController>
{
    [SerializeField]
    private int _RowsCount;
    [SerializeField]
    private int _ColumnsCount;
    [SerializeField]
    private int _MarkedCellsToWin;
    [SerializeField]
    private GameObject _CellPrefab;
    [SerializeField]
    private RectTransform _BoardPanel;
    [SerializeField]
    private GridLayoutGroup _Grid;
    private Board _Board;

	void Awake ()
    {
        _Grid.constraintCount = _ColumnsCount;
        _Grid.cellSize = new Vector2(
            (_BoardPanel.sizeDelta.x - _Grid.padding.left * (_ColumnsCount + 1)) / _ColumnsCount,
            (_BoardPanel.sizeDelta.y - _Grid.padding.top  * (_RowsCount + 1)) / _RowsCount);
        _Board = new Board();
        _Board.CreateCells(_CellPrefab, _RowsCount, _ColumnsCount, _Grid.transform);
	}

    public bool CheckWin()
    {
        bool isWin = false;
        isWin |= IsHorizontalWin();
        isWin |= IsVerticalWin();
        return isWin;
    }
    private bool IsHorizontalWin()
    {

        for (int i = 0; i < _RowsCount; i++)
        {
            int sameMarkedCount = 1;
            for (int j = 0; j < _ColumnsCount - 1; j++)
            {
                if (_Board.GetCell(j, i).PlayerIndex == _Board.GetCell(j + 1, i).PlayerIndex && _Board.GetCell(j, i).PlayerIndex != -1)
                {
                    sameMarkedCount++;
                }
                else
                {
                    sameMarkedCount = 1;
                }

                if(sameMarkedCount == _MarkedCellsToWin)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool IsVerticalWin()
    {
        for (int j = 0; j < _ColumnsCount; j++)
        {
            for (int i = 0; i < _RowsCount - 1; i++)
            {
                int sameMarkedCount = 1;

                if (_Board.GetCell(j, i).PlayerIndex == _Board.GetCell(j, i + 1).PlayerIndex && _Board.GetCell(j, i).PlayerIndex != -1)
                {
                    sameMarkedCount++;
                }
                else
                {
                    sameMarkedCount = 1;
                }

                if (sameMarkedCount == _MarkedCellsToWin)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
