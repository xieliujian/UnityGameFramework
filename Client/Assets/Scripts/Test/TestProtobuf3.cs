using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Net;
using Msg;

public class TestProtobuf3 : MonoBehaviour
{
    #region 变量

    public GameObject mSendMsgGo;

    #endregion

    #region 内置函数

    // Use this for initialization
    void Start ()
    {
        UIEventListener.Get(mSendMsgGo).onClick = OnSendMsgGoClick;

        NetManager.Instance.AddHandler(typeof(TocChat), TocChatCallback);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    #endregion

    #region 回调函数

    private void OnSendMsgGoClick(GameObject go)
    {
        TosChat tos = new TosChat();
        tos.Name = name;
        tos.Content = "xieliujian";

        NetManager.Instance.SendMessage(tos);
    }

    private void TocChatCallback(object obj)
    {
        TocChat msg = (TocChat)obj;
        if (msg == null)
            return;

        Debug.Log(msg.ToString());
    }

    #endregion
}
