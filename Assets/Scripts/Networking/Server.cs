using Nox7atra.Core;
using Nox7atra.Core.Gameplay;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Nox7atra.Networking
{
    public class Server
    {
        #region singleton
        private Server()
        {
        }
        private static Server _Instance;
        public static Server Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Server();
                }
                return _Instance;
            }
        }
        #endregion

        private User _CurrentUser;
        public User CurrentUser
        {
            get
            {
                return _CurrentUser;
            }
        }
        public void Start(int port)
        {
            _CurrentUser = new User();
            NetworkServer.Listen(IPAddress.Any.ToString(), port);
            NetworkServer.maxDelay = 0;
            RegisterServerHandlers();
        }

        public void Shutdown()
        {
            NetworkServer.Shutdown();
        }


        #region handlers
        private void RegisterServerHandlers()
        {
            NetworkServer.RegisterHandler(MsgType.Connect, OnConnect);
            NetworkServer.RegisterHandler((short) MyMsgType.CaptureCell, OnCaptureCell);
        }
        private void OnConnect(NetworkMessage msg)
        {
            
            Debug.Log(string.Concat("Connected: ", msg.conn.address));
            var connId = msg.conn.connectionId;
            if (NetworkServer.connections.Count > Constants.PLAYERS_COUNT)
            {
                SendPlayerID(connId, -1);
            }
            else
            {
                int index = Random.Range(0, Constants.PLAYERS_IDS.Length);
                SendPlayerID(connId, Constants.PLAYERS_IDS[index]);
                _CurrentUser.PlayerID = Constants.PLAYERS_IDS[(index + 1) % Constants.PLAYERS_COUNT];
            }
            SceneManager.LoadScene(1);
        }
        private void OnCaptureCell(NetworkMessage msg)
        {
            CaptureCellMessage message = msg.reader.ReadMessage<CaptureCellMessage>();
            BoardController.Instance.CaptureCellByPos(message.Position);
        }
        #endregion
        public void SendMessageToAllClients(MessageBase msg, MyMsgType type)
        {
            NetworkServer.SendToAll((short)type, msg);
        }
        private void BroadcastMessage(int connectionId, short msgType, MessageBase message)
        {
            var connections = NetworkServer.connections;
            foreach (var connection in connections)
            {
                if (connection != null && connection.connectionId != connectionId)
                {
                    NetworkServer.SendToClient(
                        connection.connectionId,
                        msgType,
                        message);
                }
            }
        }
        private void SendPlayerID(int connId, int playerId)
        {
            PlayerIDMessage message = new PlayerIDMessage();
            message.PlayerID = playerId;
            NetworkServer.SendToClient(connId, (short)MyMsgType.PlayerID, message);
        }
    }
}