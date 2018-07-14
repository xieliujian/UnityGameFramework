
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using UnityDebuger;

public static class XLuaGenConfig
{
    [LuaCallCSharp]
    public static List<Type> LC_UnityDebuger = new List<Type>()
    {
        typeof(UnityDebuger.Debuger)
    };

}
