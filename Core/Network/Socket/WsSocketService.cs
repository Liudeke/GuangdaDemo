using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if !UNITY_EDITOR && UNITY_WEBGL
//using YLWebSocket;
public class WsSocketService : SocketBase
{
    //private WebSocket m_scoket;

    public override void Close()
    {
        
    }

    public override void Connect()
    {
       
    }

    public override void Send(byte[] sendbytes)
    {
        
    }
}

#endif
