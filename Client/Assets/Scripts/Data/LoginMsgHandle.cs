using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Net;
using Msg;
using UnityDebuger;

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

        Debuger.Log("OnMsgNotifyConnect");
        ServerListUI.Instance.SetVisible(false);
        UIGenerate.CreateUI("testprotobuf3/testprotobuf3", "testprotobuf3", null);
    }
}
