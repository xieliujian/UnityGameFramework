using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using System;

public static class UIGenerate
{
    public static void CreateUI(string abname, string assetname, Action<GameObject> func)
    {
        Transform parent = Gate.GUIRoot;

        Gate.ResMgr.LoadPrefab(abname, assetname, (objs) =>
        {
            GameObject prefab = (GameObject)objs[0];
            GameObject go = GameObject.Instantiate(prefab) as GameObject;
            if (go == null)
                return;

            go.name = assetname;
            go.layer = LayerMask.NameToLayer("UI");
            go.transform.SetParent(parent);
            go.transform.Reset();

            if (func != null)
            {
                func(go);   //回传面板对象
            }
        });
    }
}
