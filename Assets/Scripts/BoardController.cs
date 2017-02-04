using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : Singleton<BoardController>
{
    [SerializeField]
    private int _BoardSize;
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
        _Grid.constraintCount = _BoardSize;
        _Grid.cellSize = new Vector2(
            (_BoardPanel.sizeDelta.x - _Grid.padding.left * (_BoardSize + 1)) / _BoardSize,
            (_BoardPanel.sizeDelta.y - _Grid.padding.top  * (_BoardSize + 1)) / _BoardSize);
        _Board = new Board();
        _Board.CreateCells(_CellPrefab, _BoardSize, _BoardSize, _Grid.transform);
	}
    //Проверка условия победы
    public bool CheckWin()
    {
        bool isWin = false;
        isWin |= IsHorizontalWin();
        isWin |= IsVerticalWin();
        isWin |= IsDiagonalWin();
        return isWin;
    }
    private bool IsHorizontalWin()
    {
        for (int i = 0; i < _BoardSize; i++)
        {
            int sameMarkedCount = 1;
            for (int j = 0; j < _BoardSize - 1; j++)
            {
                var firstIndex = _Board.GetCell(j, i).PlayerIndex;
                var secondIndex = _Board.GetCell(j + 1, i).PlayerIndex;
                if (firstIndex != -1
                     && firstIndex == secondIndex)
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
        for (int j = 0; j < _BoardSize; j++)
        {
            int sameMarkedCount = 1;
            for (int i = 0; i < _BoardSize - 1; i++)
            {
                var firstIndex = _Board.GetCell(j, i).PlayerIndex;
                var secondIndex = _Board.GetCell(j, i + 1).PlayerIndex;
                if (firstIndex != -1
                     && firstIndex == secondIndex)
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
    private bool IsDiagonalWin()
    {
        bool isWin = false;
        isWin |= IsMainDiagonalWin();
        isWin |= IsSubDiagonalWin();
        return isWin;
    }
    private bool IsMainDiagonalWin()
    {
        for (int i = 0; i < _BoardSize - 1; i++)
        {
            int sameMarkedCount = 1;
            for (int j = 0; j < _BoardSize - i - 1; j++)
            {
                var firstIndex = _Board.GetCell(j, j + i).PlayerIndex;
                var secondIndex = _Board.GetCell(j + 1, j + i + 1).PlayerIndex;
                if (firstIndex != -1
                    && firstIndex == secondIndex)
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
            sameMarkedCount = 1;
            for (int j = i; j < _BoardSize - 1; j++)
            {
                var firstIndex = _Board.GetCell(j, j - i).PlayerIndex;
                var secondIndex = _Board.GetCell(j + 1, j - i + 1).PlayerIndex;
                if (firstIndex != -1
                    && firstIndex == secondIndex)
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
    private bool IsSubDiagonalWin()
    {
        //Up main diagonal
        for (int i = 0; i < _BoardSize; i++)
        {
            int sameMarkedCount = 1;
            int k = i;
            for (int j = _BoardSize - 1; j > i; j--)
            {
                var firstIndex = _Board.GetCell(j, k).PlayerIndex;
                var secondIndex = _Board.GetCell(j - 1, k + 1).PlayerIndex;
                if (firstIndex != -1
                    && firstIndex == secondIndex)
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
                k++;
            }
            sameMarkedCount = 1;
            k = 0;
            for (int j = _BoardSize - i - 1; j > 0; j--)
            {
                var firstIndex = _Board.GetCell(j, k).PlayerIndex;
                var secondIndex = _Board.GetCell(j - 1, k + 1).PlayerIndex;
                if (firstIndex != -1
                    && firstIndex == secondIndex)
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
                k++;
            }
        }
        return false;
    }
}
