using Nox7atra.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace Nox7atra.Networking
{
    public class NetManager : Singleton<NetManager>
    {
        [SerializeField]
        private string _ServerIP;
        [SerializeField]
        private int _Port;
        [SerializeField]
        private BuildType _BuildType;

        public User CurrentUser
        {
            get
            {
                switch (_BuildType)
                {
                    default:
                    case BuildType.Client:
                        return Client.Instance.CurrentUser;
                    case BuildType.Server:
                        return Server.Instance.CurrentUser;
                }
            }
        }
        void Awake()
        {
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
        public void SendCaptureCellMessage(CaptureCellMessage msg)
        {
            switch (_BuildType)
            {
                case BuildType.Client:
                    Client.Instance.SendMessage(msg, MyMsgType.CaptureCell);
                    break;
                case BuildType.Server:
                    Server.Instance.SendMessageToAllClients(msg, MyMsgType.CaptureCell);
                    break;
            }
        }
        protected void OnDestroy()
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
}