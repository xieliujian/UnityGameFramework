using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppConst
{
    public const int SocketPort = 3563;                                             // Socket服务器端口

    public const string SocketAddress = "127.0.0.1";                                // Socket服务器地址
    //public static string SocketAddress = "45.76.76.36";                           // Socket服务器地址

    public const string AppName = "UnityGameFramework";                             // 游戏名字

    public const bool UpdateMode = false;                                           //更新模式-默认关闭 

    public const bool IsEmptyResBundle = false;                                     // 是否空的资源包

    public const string WebUrl = "http://45.76.76.36:3000/";                        //测试更新地址

    public const string ExtName = ".unity3d";                                       //素材扩展名

    public const string AssetDir = "StreamingAssets";                               //素材目录 
}

public class AppPlatform
{
    public static string DataPath
    {
        get
        {
            string appname = AppConst.AppName.ToLower();
            if (Application.isMobilePlatform)
            {
                return Application.persistentDataPath + "/" + appname + "/";
            }

            return "c:/" + appname + "/";
        }
    }

    public static string StreamingAssetsPath
    {
        get
        {
            string appname = AppConst.AppName.ToLower();
            string path = Application.streamingAssetsPath + "/" + appname + "/";
            return path;
        }
    }

    public static string GetRelativePath()
    {
        return "file:///" + DataPath;
    }
}
