using Nox7atra.Core;
using Nox7atra.Networking;
using UnityEngine;
using UnityEngine.UI;
namespace Nox7atra.UI
{
    public class StatusText : MonoBehaviour
    {
        [SerializeField]
        private Text _Status;

        void Update()
        {
            
            if (NetManager.Instance.CurrentUser.PlayerID >= 0)
            {
                bool isYourTurn
                    = NetManager.Instance.CurrentUser.PlayerID
                    == TurnManager.Instance.CurrentPlayerIndex;
                _Status.text = isYourTurn ? "Твой ход" : "Ход противника";
            }
            else
            {
                _Status.text = "Вы зритель";
            }
        }
    }
    
}