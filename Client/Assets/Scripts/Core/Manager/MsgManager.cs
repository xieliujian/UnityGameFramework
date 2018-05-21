using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class MsgManager : SingletonMonoBehaviour<MsgManager>, BaseMsgHandle
{
    private LoginMsgHandle mLoginMsgHandle = new LoginMsgHandle();

    public void Register()
    {
        mLoginMsgHandle.Register();
    }

    public void UnRegister()
    {
        mLoginMsgHandle.UnRegister();
    }
}
