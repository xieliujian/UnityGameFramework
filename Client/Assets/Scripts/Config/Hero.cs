using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class HeroCfg
{
	public int ID;			//		数字索引
	public string Name;			//		字符串索引
	public float HP;			//		数字类型
	public string AP_Name;			//		字符串类型
	public List<int> LevelAP = new List<int>();			//		整型数组
	public List<float> LevelAP1 = new List<float>();			//		整型数组
	public List<string> LevelAP2 = new List<string>();			//		整型数组
	public Dictionary<int, int> Map1 = new Dictionary<int, int>();			//		整型数组
	public Dictionary<int, float> Map2 = new Dictionary<int, float>();			//		整型数组
	public Dictionary<int, string> Map3 = new Dictionary<int, string>();			//		整型数组
	public Dictionary<string, int> Map4 = new Dictionary<string, int>();			//		整型数组
	public Dictionary<string, float> Map5 = new Dictionary<string, float>();			//		整型数组
	public Dictionary<string, string> Map6 = new Dictionary<string, string>();			//		整型数组

	public HeroCfg(string line)
	{
		string []fields = line.Split('	');
		ID = int.Parse(fields[0]);
		Name = fields[1];
		HP = float.Parse(fields[2]);
		AP_Name = fields[3];
		LevelAP = ConfigUtil.ParseListInt(fields[4]);
		LevelAP1 = ConfigUtil.ParseListFloat(fields[5]);
		LevelAP2 = ConfigUtil.ParseListString(fields[6]);
		Map1 = ConfigUtil.ParseDictIntInt(fields[7]);
		Map2 = ConfigUtil.ParseDictIntFloat(fields[8]);
		Map3 = ConfigUtil.ParseDictIntString(fields[9]);
		Map4 = ConfigUtil.ParseDictStringInt(fields[10]);
		Map5 = ConfigUtil.ParseDictStringFloat(fields[11]);
		Map6 = ConfigUtil.ParseDictStringString(fields[12]);
	}
}

public class HeroCfgManager
{
	private Dictionary<int, HeroCfg> mDict = new Dictionary<int, HeroCfg>();

	public void InitTable()
	{
		string data = ConfigUtil.GetConfigData("/Hero.txt");
		string[] splits = data.Split('\n');
		foreach (string split in splits)
		{
			string line = split.Trim();
			if (line.Length > 0)
			{
				HeroCfg rowdata = new HeroCfg(line);
				mDict.Add(rowdata.ID, rowdata);
			}
			else
			{
				continue;
			}
		}
	}

	public HeroCfg GetDataByID(int id)
	{
		HeroCfg rowdata = null;
		mDict.TryGetValue(id, out rowdata);
		return rowdata;
	}

	private HeroCfgManager() { }

	public static readonly HeroCfgManager Instance = new HeroCfgManager();
}
