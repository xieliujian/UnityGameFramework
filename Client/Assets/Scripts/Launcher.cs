using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDebuger;
using Util;
using Net;

public class Launcher : MonoBehaviour
{
    #region 内置函数

    // Use this for initialization
    void Start ()
    {
        DestroyObject(gameObject);
        Launch();        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    #endregion

    #region 函数

    private void Launch()
    {
        InitDebuger();
        LaunchUIRoot();
        LaunchGameControl();
    }

    private void InitDebuger()
    {
        //初始化Debuger的日志开关
        Debuger.Init(Application.persistentDataPath + "/DebugerLog/");
        Debuger.EnableLog = true;
        Debuger.EnableSave = false;
        Debuger.Log();
    }

    private void LaunchUIRoot()
    {
        string name = "UI Root";
        GameObject root = GameObject.Find(name);
        if (root == null)
        {
            var prefab = Resources.Load<GameObject>(name);
            root = GameObject.Instantiate(prefab);
        }

        root.transform.Reset();
        DontDestroyOnLoad(root);
    }

    private void LaunchGameControl()
    {
        string name = "GameController";
        GameObject root = GameObject.Find(name);
        if (root == null)
        {
            var prefab = Resources.Load<GameObject>(name);
            root = GameObject.Instantiate(prefab);
        }

        var gamecontrol = root.GetComponent<GameController>();
        gamecontrol.Initialize(GameStart);
    }

    private void GameStart()
    {
        ConfigManager.Load();

        UIGenerate.CreateUI("serverlist/serverlist", "serverlist", null);
    }

    #endregion
}
