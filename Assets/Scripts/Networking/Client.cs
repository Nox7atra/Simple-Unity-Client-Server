using Nox7atra.Core;
using Nox7atra.Core.Gameplay;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

namespace Nox7atra.Networking
{
    public class Client
    {

        #region singleton
        private Client()
        {

        }
        private static Client _Instance;
        public static Client Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Client();
                }
                return _Instance;
            }
        }
        #endregion

        #region private fields
        private NetworkClient _CurrentClient;
        private string _SeverIP;
        private int _ServerPort;
        private User _CurrentUser;
        #endregion
        public User CurrentUser
        {
            get
            {
                return _CurrentUser;
            }
        }
        //Инициализируем клиент
        public void Start(string serverIP, int port)
        {
            _CurrentClient = new NetworkClient();
            _CurrentUser = new User();
            RegisterHandles();
            _SeverIP = serverIP;
            _ServerPort = port;
            _CurrentClient.Connect(_SeverIP, _ServerPort);
        }
        public void Shutdown()
        {
            _CurrentClient.Shutdown();
        }
        #region handlers
        private void RegisterHandles()
        {
            _CurrentClient.RegisterHandler((short)MyMsgType.PlayerID, OnPlayerID);
            _CurrentClient.RegisterHandler((short)MyMsgType.CaptureCell, OnCaptureCell);
        }
        private void OnPlayerID(NetworkMessage msg)
        {
            PlayerIDMessage message = msg.reader.ReadMessage<PlayerIDMessage>();
            _CurrentUser.PlayerID = message.PlayerID;
            SceneManager.LoadScene(1);
        }
        private void OnCaptureCell(NetworkMessage msg)
        {
            CaptureCellMessage message = msg.reader.ReadMessage<CaptureCellMessage>();
            BoardController.Instance.CaptureCellByPos(message.Position);
        }
        #endregion
        public void SendMessage(MessageBase msg, MyMsgType type)
        {
            _CurrentClient.Send((short)type, msg);
        }
    }
}