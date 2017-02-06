using UnityEngine.Networking;

namespace Nox7atra.Networking
{
    public class PlayerIDMessage : MessageBase
    {
        public int PlayerID;
    }
    public class CaptureCellMessage : MessageBase
    {
        public int[] Position;
    }

    public enum MyMsgType : short
    {
        PlayerID    = MsgType.Highest + 1,
        CaptureCell = MsgType.Highest + 2,
    }
}