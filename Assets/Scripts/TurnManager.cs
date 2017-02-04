using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager 
{
    private const int PLAYERS_COUNT = 2;
    #region singleton
    private TurnManager()
    {
        _CurrentPlayerIndex = 0;
    }
    private static TurnManager _Instance;
    public static TurnManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new TurnManager();
            }
            return _Instance;
        }
    }
    #endregion
    private int _CurrentPlayerIndex;
    public int CurrentPlayerIndex
    {
        get
        {
            return _CurrentPlayerIndex;
        }
    }
    public void NextPlayerTurn()
    {
        _CurrentPlayerIndex = (_CurrentPlayerIndex + 1) % PLAYERS_COUNT;
    }
}
