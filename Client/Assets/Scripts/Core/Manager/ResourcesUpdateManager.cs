using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using System;
using System.IO;

public class ResourcesUpdateManager : SingletonMonoBehaviour<ResourcesUpdateManager>
{
    #region 变量

    private Action OnResourceUpdateEndAction = null;

    #endregion

    #region 内置函数

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #endregion

    #region 函数

    public void StartResourceUpdate(Action func)
    {
        OnResourceUpdateEndAction = func;

        CheckExtractResource();
    }

    private void CheckExtractResource()
    {
        bool existdir = Directory.Exists(AppPlatform.DataPath);
        bool existfile = File.Exists(AppPlatform.DataPath + "files.txt");
        bool exist = existdir & existfile;

        if (exist)
        {
            StartCoroutine(OnUpdateResource());
            return;
        }

        StartCoroutine(OnExtractResource());
    }

    IEnumerator OnUpdateResource()
    {
        LoadingLayer.Show();

        if (!AppConst.UpdateMode)
        {
            ResourceUpdateEnd();
            yield break;
        }

        string dataPath = AppPlatform.DataPath;  //数据目录
        string url = AppConst.WebUrl + AppPlatform.GetCurPackageResPath();
        string listUrl = url + "files.txt";

        Debug.Log("LoadUpdate---->>>" + listUrl);

        WWW www = new WWW(listUrl);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        //if (www.error != null)
        {
            Debug.Log(www.error);
            LoadingLayer.Hide();
            yield break;
        }

        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }

        File.WriteAllBytes(dataPath + "files.txt", www.bytes);

        string filesText = www.text;
        string[] files = filesText.Split('\n');
        for (int i = 0; i < files.Length; i++)
        {
            float percent = (float)i / (files.Length - 1);
            LoadingLayer.SetProgressbarValue(percent);

            if (string.IsNullOrEmpty(files[i]))
                continue;

            string[] keyValue = files[i].Split('|');
            string f = keyValue[0];
            string localfile = (dataPath + f).Trim();

            string path = Path.GetDirectoryName(localfile);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileUrl = url + f;
            bool canUpdate = !File.Exists(localfile);
            if (!canUpdate)
            {
                string remoteMd5 = keyValue[1].Trim();
                string localMd5 = Utility.Md5file(localfile);
                canUpdate = !remoteMd5.Equals(localMd5);
                if (canUpdate)
                    File.Delete(localfile);
            }

            if (canUpdate)
            {   
                //本地缺少文件
                Debug.Log(fileUrl);

                www = new WWW(fileUrl);
                yield return www;

                if (!string.IsNullOrEmpty(www.error))
                 //   if (www.error != null)
                {
                    Debug.Log(www.error);
                    LoadingLayer.Hide();
                    yield break;
                }

                File.WriteAllBytes(localfile, www.bytes);
            }
        }

        yield return new WaitForEndOfFrame();

        ResourceUpdateEnd();
    }

    IEnumerator OnExtractResource()
    {
        string streampath = AppPlatform.StreamingAssetsPath;
        string datapath = AppPlatform.DataPath;
        string infile = streampath + "files.txt";
        string outfile = datapath + "files.txt";

        if (File.Exists(outfile))
            File.Delete(outfile);

        if (Directory.Exists(datapath))
            Directory.Delete(datapath);

        Directory.CreateDirectory(datapath);

        LoadingLayer.Show();

        if (Application.platform == RuntimePlatform.Android)
        {
            WWW www = new WWW(infile);
            yield return www;

            if (www.isDone)
            {
                File.WriteAllBytes(outfile, www.bytes);
            }

            yield return 0;
        }
        else
        {
            File.Copy(infile, outfile, true);
        }

        yield return new WaitForEndOfFrame();

        //释放所有文件到数据目录
        string[] files = File.ReadAllLines(outfile);
        for (int i = 0; i < files.Length; i++)
        {
            float percent = (float)i / (files.Length - 1);
            string file = files[i];
            string[] fs = file.Split('|');
            infile = streampath + fs[0];  //
            outfile = datapath + fs[0];

            Debug.Log("正在解包文件:>" + infile);
            LoadingLayer.SetProgressbarValue(percent);

            string dir = Path.GetDirectoryName(outfile);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (Application.platform == RuntimePlatform.Android)
            {
                WWW www = new WWW(infile);
                yield return www;

                if (www.isDone)
                {
                    File.WriteAllBytes(outfile, www.bytes);
                }

                yield return 0;
            }
            else
            {
                if (File.Exists(outfile))
                {
                    File.Delete(outfile);
                }

                File.Copy(infile, outfile, true);
            }

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(OnUpdateResource());
    }

    private void ResourceUpdateEnd()
    {
        LoadingLayer.Hide();

        Gate.ResMgr.Initialize(AppConst.AppName.ToLower(), ()=>
        {
            if (OnResourceUpdateEndAction != null)
                OnResourceUpdateEndAction();
        });   
    }

    #endregion
}
