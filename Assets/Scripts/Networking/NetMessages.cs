using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CaptureCellMessage : MessageBase
{
    public int PlayerID;
    public int CellIndex;
}
public enum MyMsgType : short
{
    CaptureCell = MsgType.Highest + 1
}
