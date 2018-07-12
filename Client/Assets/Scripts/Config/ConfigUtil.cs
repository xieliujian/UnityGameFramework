
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class ConfigUtil
{
    #region 常量

    #region use json split

    /// <summary>
    /// 分隔符1
    /// </summary>
    public const char SPLIT1 = ',';

    /// <summary>
    /// 分隔符2
    /// </summary>
    public const char SPLIT2 = ':';

    /// <summary>
    /// json struct 1
    /// </summary>
    public const string JSONSTRUCT1 = "[";

    /// <summary>
    /// json struct 2
    /// </summary>
    public const string JSONSTRUCT2 = "]";

    /// <summary>
    /// json struct 3
    /// </summary>
    public const string JSONSTRUCT3 = "{";

    /// <summary>
    /// json struct 4
    /// </summary>
    public const string JSONSTRUCT4 = "}";

    /// <summary>
    /// json struct 5
    /// </summary>
    public const string JSONSTRUCT5 = "\"";

    #endregion

#if false

    #region use normal split

    /// <summary>
    /// 分隔符1
    /// </summary>
    public const char SPLIT1 = ';';

    /// <summary>
    /// 分隔符2
    /// </summary>
    public const char SPLIT2 = ',';

    #endregion

#endif

    #endregion

    #region 函数

    /// <summary>
    /// 得到配置数据
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetConfigData(string path)
    {
        string data = "";
        path = AppPlatform.ConfigPath + "/" + path;
        StreamReader sr = new StreamReader(path, Encoding.UTF8);
        data = sr.ReadToEnd();

        return data;
    }

#region use json split

    public static void RemoveJsonStruct(ref string data)
    {
        data = data.Replace(JSONSTRUCT1, "");
        data = data.Replace(JSONSTRUCT2, "");
        data = data.Replace(JSONSTRUCT3, "");
        data = data.Replace(JSONSTRUCT4, "");
        data = data.Replace(JSONSTRUCT5, "");
    }

    public static List<int> ParseListInt(string data)
    {
        RemoveJsonStruct(ref data);

        string[] splits = data.Split(SPLIT1);

        List<int> list = new List<int>();
        foreach (string val in splits)
            list.Add(int.Parse(val));

        return list;
    }

    public static List<float> ParseListFloat(string data)
    {
        RemoveJsonStruct(ref data);

        string[] splits = data.Split(SPLIT1);

        List<float> list = new List<float>();
        foreach (string val in splits)
            list.Add(float.Parse(val));

        return list;
    }

    public static List<string> ParseListString(string data)
    {
        RemoveJsonStruct(ref data);

        string[] splits = data.Split(SPLIT1);

        List<string> list = new List<string>();
        list.AddRange(splits);

        return list;
    }

    public static Dictionary<int, int> ParseDictIntInt(string data)
    {
        RemoveJsonStruct(ref data);

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
        RemoveJsonStruct(ref data);

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
        RemoveJsonStruct(ref data);

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
        RemoveJsonStruct(ref data);

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
        RemoveJsonStruct(ref data);

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
        RemoveJsonStruct(ref data);

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

#if false

#region use normal split

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

#endif

#endregion
}
