using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AppConst
{
    public const int SocketPort = 3563;                                             // Socket服务器端口

    //public const string SocketAddress = "127.0.0.1";                                // Socket服务器地址
    public static string SocketAddress = "45.76.76.36";                           // Socket服务器地址

    public const string AppName = "UnityGameFramework";                             // 游戏名字

    public const bool UpdateMode = true;                                           //更新模式-默认关闭 

    public const bool IsEmptyResBundle = true;                                     // 是否空的资源包

    public const string WebUrl = "http://45.76.76.36:3000/";                        //测试更新地址

    public const string ExtName = ".unity3d";                                       //素材扩展名

    public const string AssetDir = "AssetBundle";                                   //素材目录 
}

public class AppPlatform
{
    public static string DataPath
    {
        get
        {
            string assetdir = AppConst.AssetDir;
            string appname = AppConst.AppName.ToLower();
            if (Application.isEditor)
            {
                return "c:/" + appname + "/";
            }

            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                return Application.dataPath + "/" + assetdir + "/" + appname + "/";
            }

            return Application.persistentDataPath + "/" + appname + "/";
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
        Debug.Log(DataPath);
        return "file:///" + DataPath;
    }

    public static string GetCurPlatform()
    {
        string platformname = RuntimePlatform.WindowsPlayer.ToString().ToLower();
        if (Application.platform == RuntimePlatform.WindowsPlayer || 
            Application.platform == RuntimePlatform.WindowsEditor)
        {
            platformname = RuntimePlatform.WindowsPlayer.ToString().ToLower();
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            platformname = RuntimePlatform.Android.ToString().ToLower();
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer || 
                 Application.platform == RuntimePlatform.OSXEditor ||
                 Application.platform == RuntimePlatform.OSXPlayer)
        {
            platformname = RuntimePlatform.IPhonePlayer.ToString().ToLower();
        }

        return platformname;
    }

    public static string GetCurPackageResPath()
    {
        string assetdir = AppConst.AssetDir;
        string curplatform = GetCurPlatform();
        string appname = AppConst.AppName.ToLower();
        string path = assetdir + "/" + curplatform + "/" + appname + "/";
        return path;
    }

#if UNITY_EDITOR

    public static BuildTarget GetCurBuildTarget()
    {
        if ( Application.platform == RuntimePlatform.WindowsEditor || 
             Application.platform == RuntimePlatform.WindowsPlayer)
        {
            return BuildTarget.StandaloneWindows;
        }
        else if ( Application.platform == RuntimePlatform.Android)
        {
            return BuildTarget.Android;
        }
        else if ( Application.platform == RuntimePlatform.OSXEditor ||
                  Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return BuildTarget.iOS;
        }

        return BuildTarget.StandaloneWindows;
    }

    public static BuildTargetGroup GetCurBuildTargetGroup()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.WindowsPlayer)
        {
            return BuildTargetGroup.Standalone;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            return BuildTargetGroup.Android;
        }
        else if (Application.platform == RuntimePlatform.OSXEditor ||
                 Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return BuildTargetGroup.iOS;
        }

        return BuildTargetGroup.Standalone;
    }

    public static string GetPackageResPath(BuildTarget target)
    {
        string platformpath = "";
        if (target == BuildTarget.StandaloneWindows)
        {
            platformpath = RuntimePlatform.WindowsPlayer.ToString().ToLower();
        }
        else if (target == BuildTarget.Android)
        {
            platformpath = RuntimePlatform.Android.ToString().ToLower();
        }
        else if (target == BuildTarget.iOS)
        {
            platformpath = RuntimePlatform.IPhonePlayer.ToString().ToLower();
        }

        string assetdir = AppConst.AssetDir;
        string appname = AppConst.AppName.ToLower();
        string respath = Application.dataPath + "/" + assetdir + "/" + platformpath + "/" + appname + "/";
        return respath;
    }

    #endif
}
