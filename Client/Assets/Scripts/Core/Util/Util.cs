using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityDebuger;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Util
{
    public static class Utility
    {
        public static void Reset(this Transform trans)
        {
            trans.transform.localPosition = Vector3.zero;
            trans.transform.localRotation = Quaternion.identity;
            trans.transform.localScale = Vector3.one;
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        public static string Md5file(string file)
        {
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fs);
                fs.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("md5file() fail, error:" + ex.Message);
            }
        }

        /// <summary>
        /// 防止初学者不按步骤来操作
        /// </summary>
        /// <returns></returns>
        public static int CheckRuntimeFile()
        {
            if (!Application.isEditor)
                return 0;

            string streamDir = AppPlatform.StreamingAssetsPath;
            if (!Directory.Exists(streamDir))
            {
                return -1;
            }
            else
            {
                string[] files = Directory.GetFiles(streamDir);
                if (files.Length == 0)
                    return -1;

                if (!File.Exists(streamDir + "files.txt"))
                {
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// 检查运行环境
        /// </summary>
        public static bool CheckEnvironment()
        {
#if UNITY_EDITOR
            int resultId = CheckRuntimeFile();
            if (resultId == -1)
            {
                Debuger.LogError("没有找到框架所需要的资源，单击Game菜单下Build xxx Resource生成！！");
                EditorApplication.isPlaying = false;
                return false;
            }
#endif
            return true;
        }
    }
}

