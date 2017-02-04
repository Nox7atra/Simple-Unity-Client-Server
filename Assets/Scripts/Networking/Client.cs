using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


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

    #endregion
    //Инициализируем клиент
    public void Start(string serverIP, int port)
    {
        _CurrentClient = new NetworkClient();
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

    }

    #endregion
    public void SendMessage(MyMsgType type)
    {
        switch (type)
        {

        }
    }
}
