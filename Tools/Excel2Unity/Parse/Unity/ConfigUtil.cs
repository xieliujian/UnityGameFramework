
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class ConfigUtil
{
    #region 常量

    /// <summary>
    /// 分隔符1
    /// </summary>
    public const char SPLIT1 = ';';

    /// <summary>
    /// 分隔符2
    /// </summary>
    public const char SPLIT2 = ',';

    #endregion

    #region 函数

    /// <summary>
    /// 得到数据路径
    /// </summary>
    /// <returns></returns>
    public static string GetConfigDataPath()
    {
        string path = "";

    #if UNITY_EDITOR
        path = Application.dataPath;
#else
        //or sandbox dir.
        path = Application.persistentDataPath;
#endif

        return path;
    }

    /// <summary>
    /// 得到配置数据
    /// </summary>
    /// <param name="path"></param>
    /// <param name="userespath"></param>
    /// <returns></returns>
    public static string GetConfigData(string path, bool userespath = true)
    {
        string data = "";
        if (userespath)
        {
            data = Resources.Load(path).ToString();
        }
        else
        {
            path = GetConfigDataPath() + "/" + path;
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            data = sr.ReadToEnd();
        }

        return data;
    }

    public static List<int> ParseListInt(string data)
    {
        string[] splits = data.Split(SPLIT1);

        List<int> list = new List<int>();
        foreach (string val in splits)
            list.Add(int.Parse(val));

        return list;
    }

    public static List<float> ParseListFloat(string data)
    {
        string[] splits = data.Split(SPLIT1);

        List<float> list = new List<float>();
        foreach (string val in splits)
            list.Add(float.Parse(val));

        return list;
    }

    public static List<string> ParseListString(string data)
    {
        string[] splits = data.Split(SPLIT1);

        List<string> list = new List<string>();
        list.AddRange(splits);

        return list;
    }

    public static Dictionary<int, int> ParseDictIntInt(string data)
    {
        string[] split1s = data.Split(SPLIT1);

        Dictionary<int, int> dict = new Dictionary<int, int>();
        foreach (string str in split1s)
        {
            string[] split2s = str.Split(SPLIT2);
            int key = int.Parse(split2s[0]);
            int value = int.Parse(split2s[1]);
            dict.Add(key, value);
        }

        return dict;
    }

    public static Dictionary<int, float> ParseDictIntFloat(string data)
    {
        string[] split1s = data.Split(SPLIT1);

        Dictionary<int, float> dict = new Dictionary<int, float>();
        foreach (string str in split1s)
        {
            string[] split2s = str.Split(SPLIT2);
            int key = int.Parse(split2s[0]);
            float value = float.Parse(split2s[1]);
            dict.Add(key, value);
        }

        return dict;
    }

    public static Dictionary<int, string> ParseDictIntString(string data)
    {
        string[] split1s = data.Split(SPLIT1);

        Dictionary<int, string> dict = new Dictionary<int, string>();
        foreach (string str in split1s)
        {
            string[] split2s = str.Split(SPLIT2);
            int key = int.Parse(split2s[0]);
            string value = split2s[1];
            dict.Add(key, value);
        }

        return dict;
    }

    public static Dictionary<string, int> ParseDictStringInt(string data)
    {
        string[] split1s = data.Split(SPLIT1);

        Dictionary<string, int> dict = new Dictionary<string, int>();
        foreach (string str in split1s)
        {
            string[] split2s = str.Split(SPLIT2);
            string key = split2s[0];
            int value = int.Parse(split2s[1]);
            dict.Add(key, value);
        }

        return dict;
    }

    public static Dictionary<string, float> ParseDictStringFloat(string data)
    {
        string[] split1s = data.Split(SPLIT1);

        Dictionary<string, float> dict = new Dictionary<string, float>();
        foreach (string str in split1s)
        {
            string[] split2s = str.Split(SPLIT2);
            string key = split2s[0];
            float value = float.Parse(split2s[1]);
            dict.Add(key, value);
        }

        return dict;
    }

    public static Dictionary<string, string> ParseDictStringString(string data)
    {
        string[] split1s = data.Split(SPLIT1);

        Dictionary<string, string> dict = new Dictionary<string, string>();
        foreach (string str in split1s)
        {
            string[] split2s = str.Split(SPLIT2);
            string key = split2s[0];
            string value = split2s[1];
            dict.Add(key, value);
        }

        return dict;
    }

    #endregion
}
