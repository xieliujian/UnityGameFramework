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
        
        ResourceUpdateEnd();
        yield break;
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
        foreach (var file in files)
        {
            string[] fs = file.Split('|');
            infile = streampath + fs[0];  //
            outfile = datapath + fs[0];

            Debug.Log("正在解包文件:>" + infile);

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
        if (OnResourceUpdateEndAction != null)
            OnResourceUpdateEndAction();
    }

    #endregion
}
