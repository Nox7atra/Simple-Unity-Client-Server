using Nox7atra.Networking;
using Nox7atra.UI;
using UnityEngine;
using UnityEngine.UI;
namespace Nox7atra.Core.Gameplay
{
    public class Cell : MonoBehaviour
    {
        [SerializeField]
        private Button _Button;
        [SerializeField]
        private Image _Image;
        [SerializeField]
        private Sprite[] _PlayersSprites;

        private int _PlayerIndex;
        private Board _Board;
        public int PlayerIndex
        {
            get
            {
                return _PlayerIndex;
            }
        }

        void Awake()
        {
            _PlayerIndex = -1;
        }
        public void OnClick()
        {
            if(TurnManager.Instance.CurrentPlayerIndex 
                != NetManager.Instance.CurrentUser.PlayerID)
            {
                return;
            }
            CaptureCell();
            SendCaptureMessage();
        }
        public void CaptureCell()
        {
            _PlayerIndex = TurnManager.Instance.CurrentPlayerIndex;
            _Button.interactable = false;
            _Image.sprite = _PlayersSprites[_PlayerIndex];
            BoardController.Instance.CheckBoardState();
            GameState state = BoardController.Instance.CurrentState;
            switch(state)
            {
                case GameState.Process:
                    TurnManager.Instance.NextPlayerTurn();
                    break;
                case GameState.SomeoneWin:
                    if(NetManager.Instance.CurrentUser.PlayerID 
                        == TurnManager.Instance.CurrentPlayerIndex)
                    {
                        ModulesController.Instance.Modules.GetModule<DialogWindow>()
                            .Show(
                            "Вы выиграли!",
                            "Поздравляю вы выиграли в данном матче :)",
                            new System.Action[] { Application.Quit });
                    }
                    else
                    {
                        ModulesController.Instance.Modules.GetModule<DialogWindow>()
                            .Show(
                            "Вы проиграли!",
                            "К сожалению вы проиграли в данном матче :(",
                            new System.Action[] { Application.Quit });
                    }
                    break;
                case GameState.Tie:
                    ModulesController.Instance.Modules.GetModule<DialogWindow>()
                            .Show(
                            "Ничья!",
                            "Вы сыграли в ничью :|",
                            new System.Action[] { Application.Quit });
                    break;

            }
        }

        public static Cell CreateCell(GameObject prefab, Transform parent, Board board)
        {
            Cell cell = GameObject.Instantiate(prefab, parent, false).GetComponent<Cell>();
            cell._Board = board;
            return cell;
        }
        private void SendCaptureMessage()
        {
            var pos = _Board.GetCellPosition(this);
            CaptureCellMessage msg = new CaptureCellMessage();
            msg.Position = _Board.GetCellPosition(this);
            NetManager.Instance.SendCaptureCellMessage(msg);
        }
    }
}