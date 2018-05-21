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
}
