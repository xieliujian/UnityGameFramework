using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using XLua;

public class LuaManager : SingletonMonoBehaviour<LuaManager>
{
    #region 变量

    /// <summary>
    /// lua环境
    /// </summary>
    private LuaEnv mLuaEnv = null;

    #endregion

    #region 内置函数

    private void Start()
    {
        mLuaEnv = new LuaEnv();
    }

    private void Update()
    {
        if (mLuaEnv != null)
        {
            mLuaEnv.Tick();
        }
    }

    private void OnDestroy()
    {
        if (mLuaEnv != null)
            mLuaEnv.Dispose();
    }

    #endregion
}
