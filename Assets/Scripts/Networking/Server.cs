using System.Net;
using UnityEngine;
using UnityEngine.Networking;

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
    public void Start(int port)
    {
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
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
    }
    private void OnConnected(NetworkMessage msg)
    {
        Debug.Log(string.Concat("Connected: ", msg.conn.address));
    }

    public void SendMessageToAllClients(MyMsgType type)
    {
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
    #endregion

}
