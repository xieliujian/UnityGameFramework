using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public static class Gate
{
    public static ResourcesUpdateManager ResUpdateMgr
    {
        get
        {
            return ResourcesUpdateManager.Instance;
        }      
    }

    public static ResourceManager ResMgr
    {
        get
        {
            return ResourceManager.Instance;
        }
    }

    public static MsgManager MsgMgr
    {
        get
        {
            return MsgManager.Instance;
        }
    }

    public static LuaManager LuaMgr
    {
        get
        {
            return LuaManager.Instance;
        }
    }

    public static Transform GUIRoot
    {
        get
        {
            GameObject go = GameObject.FindWithTag("GUIRoot");
            if (go == null)
                return null;

            return go.transform;
        }
    }
}
