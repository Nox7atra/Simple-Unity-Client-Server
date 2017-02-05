using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nox7atra.Core
{
    public class TurnManager
    {

        #region singleton
        private TurnManager()
        {
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
        public void Restart()
        {
            _CurrentPlayerIndex = 0;
        }
        public void NextPlayerTurn()
        {
            _CurrentPlayerIndex = (_CurrentPlayerIndex + 1) % Constants.PLAYERS_COUNT;
        }
    }
}