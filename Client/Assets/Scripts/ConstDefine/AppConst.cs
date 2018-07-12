using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AppConst
{
    public const int SocketPort = 3563;                                             // Socket服务器端口

    //public const string SocketAddress = "127.0.0.1";                              // Socket服务器地址
    public static string SocketAddress = "45.76.76.36";                             // Socket服务器地址

    public const string AppName = "UnityGameFramework";                             // 游戏名字

    public const bool UpdateMode = false;                                           // 更新模式-默认关闭 

    public const bool IsEmptyResBundle = false;                                     // 是否空的资源包

    public const string WebUrl = "http://45.76.76.36:3000/";                        // 测试更新地址

    public const string ExtName = ".unity3d";                                       // 素材扩展名

    public const string AssetDir = "AssetBundle";                                   // 打包目录 

    public const string ArtPath = "Art";                                            // 素材目录

    public const string ConfigDirName = "Config";                                   // 配置文件夹名字
}

public class AppPlatform
{
    public static string DataPath
    {
        get
        {
            string assetdir = AppConst.AssetDir;
            string appname = AppConst.AppName.ToLower();

            // 编辑器
            if (Application.isEditor)
            {
                return "c:/" + appname + "/";
            }

            // windows程序
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                return Application.dataPath + "/" + assetdir + "/" + appname + "/";
            }

            // mobile程序
            return Application.persistentDataPath + "/" + appname + "/";
        }
    }

    public static string ConfigPath
    {
        get
        {
            return DataPath + "/" + AppConst.ConfigDirName + "/";
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
        // 这里不能用 "file:///" 需要用 "file://", 不然在移动平台www无法读取资源
        // return "file:///" + DataPath;
        return "file://" + DataPath;
    }

    public static string GetCurPlatform()
    {
        string platformname = RuntimePlatform.WindowsPlayer.ToString().ToLower();

#if UNITY_ANDROID
        platformname = RuntimePlatform.Android.ToString().ToLower();
#endif

#if UNITY_IOS
        platformname = RuntimePlatform.IPhonePlayer.ToString().ToLower();
#endif

#if UNITY_STANDALONE_WIN
        platformname = RuntimePlatform.WindowsPlayer.ToString().ToLower();
#endif
        
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
#if UNITY_ANDROID
        return BuildTarget.Android;
#endif

#if UNITY_IOS
        return BuildTarget.iOS;
#endif

#if UNITY_STANDALONE_WIN
        return BuildTarget.StandaloneWindows;
#endif
    }

    public static BuildTargetGroup GetCurBuildTargetGroup()
    {
#if UNITY_ANDROID
        return BuildTargetGroup.Android;
#endif

#if UNITY_IOS
        return BuildTargetGroup.iOS;
#endif

#if UNITY_STANDALONE_WIN
        return BuildTargetGroup.Standalone;
#endif
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
        string respath = Application.dataPath + "/../" + assetdir + "/" + platformpath + "/" + appname + "/";
        return respath;
    }

#endif
}
