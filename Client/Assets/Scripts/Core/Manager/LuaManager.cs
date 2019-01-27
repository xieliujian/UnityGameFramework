using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using XLua;
using System;
using System.IO;

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

    public LuaTable NewLuaTable()
    {
        LuaTable luatable = mLuaEnv.NewTable();
        LuaTable meta = mLuaEnv.NewTable();
        meta.Set("__index", mLuaEnv.Global);
        luatable.SetMetaTable(meta);
        meta.Dispose();
        luatable.Set("self", this);

        return luatable;
    }

    public string GetLuaString(string luafile)
    {
        string file = AppPlatform.LuaPath + luafile;
        FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fileStream);
        string data = sr.ReadToEnd();
        return data;
    }

    public void Exec(string luafile)
    {
        LuaTable luatable = NewLuaTable();
        string luadata = GetLuaString(luafile);
        mLuaEnv.DoString(luadata, "main", luatable);

        Action func;
        luatable.Get("main", out func);
        if (func != null)
        {
            func();
        }
    }

    #endregion
}
