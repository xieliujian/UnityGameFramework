using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Net;
using Msg;

public class LoginMsgHandle : BaseMsgHandle
{
    public void Register()
    {
        NetManager.Instance.AddHandler(typeof(TocNotifyConnect), OnMsgNotifyConnect);
    }

    public void UnRegister()
    {
        NetManager.Instance.RemoveHandle(typeof(TocNotifyConnect), OnMsgNotifyConnect);
    }

    private void OnMsgNotifyConnect(object obj)
    {
        TocNotifyConnect msg = (TocNotifyConnect)obj;
        if (msg == null)
            return;

        Debug.Log("OnMsgNotifyConnect");
    }
}
