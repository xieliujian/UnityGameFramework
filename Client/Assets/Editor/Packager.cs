using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using Util;

public class Packager
{
    #region 菜单

    [MenuItem("UnityGameFramework/Build iPhone Resource", false, 100)]
    public static void BuildiPhoneResource()
    {
        BuildAssetResource(BuildTarget.iOS);
    }

    [MenuItem("UnityGameFramework/Build Android Resource", false, 101)]
    public static void BuildAndroidResource()
    {
        BuildAssetResource(BuildTarget.Android);
    }

    [MenuItem("UnityGameFramework/Build Windows Resource", false, 102)]
    public static void BuildWindowsResource()
    {
        BuildAssetResource(BuildTarget.StandaloneWindows);
    }

    #endregion

    #region 函数

    /// <summary>
    /// 生成绑定素材
    /// </summary>
    private static void BuildAssetResource(BuildTarget target)
    {
        if (Directory.Exists(AppPlatform.DataPath))
        {
            Directory.Delete(AppPlatform.DataPath, true);
        }

        string streamPath = AppPlatform.StreamingAssetsPath;
        if (Directory.Exists(streamPath))
        {
            Directory.Delete(streamPath, true);
        }

        Directory.CreateDirectory(streamPath);
        AssetDatabase.Refresh();
    }

    #endregion
}
