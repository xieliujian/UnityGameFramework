using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Net;
using Util;

public class GameController : MonoBehaviour
{
    #region 变量

    private Action OnGameStartAction = null;

    #endregion

    #region 内置函数

    void Awake()
	{
        DontDestroyOnLoad(this);
	}

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        NetManager.Instance.CloseSocket();
    }

    #endregion

    #region 函数

    public void Initialize(Action action)
    {
        if (!Utility.CheckEnvironment())
            return;

        OnGameStartAction = action;
        gameObject.AddComponent<NetManager>();
        gameObject.AddComponent<ResourcesUpdateManager>();
        gameObject.AddComponent<ResourceManager>();
        gameObject.AddComponent<MsgManager>();
        gameObject.AddComponent<LuaManager>();

        Gate.MsgMgr.Register();

        Gate.ResUpdateMgr.StartResourceUpdate(() =>
        {
            if (OnGameStartAction != null)
                OnGameStartAction();
        });
    }

    #endregion
}
