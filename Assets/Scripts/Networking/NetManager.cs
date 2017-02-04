using UnityEngine;

public class NetManager : Singleton<NetManager>
{
    [SerializeField]
    private string _ServerIP;
    [SerializeField]
    private int _Port;
    [SerializeField]
    private BuildType _BuildType;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        switch (_BuildType)
        {
            case BuildType.Client:
                Client.Instance.Start(_ServerIP, _Port);
                break;
            case BuildType.Server:
                Server.Instance.Start(_Port);
                break;
        }
    }
    public void SendMessage(MyMsgType msgType)
    {
        switch (_BuildType)
        {
            case BuildType.Client:
                Client.Instance.SendMessage(msgType);
                break;
            case BuildType.Server:
                Server.Instance.SendMessageToAllClients(msgType);
                break;
        }
    }
    private void OnDestroy()
    {
        switch (_BuildType)
        {
            case BuildType.Client:
                Client.Instance.Shutdown();
                break;
            case BuildType.Server:
                Server.Instance.Shutdown();
                break;
        }
    }
}
public enum BuildType
{
    Client,
    Server
    }
