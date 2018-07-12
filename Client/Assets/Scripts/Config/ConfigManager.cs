using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager
{
	public static void Load()
	{
		HeroCfgManager.Instance.InitTable();
		ShipCfgManager.Instance.InitTable();
		TaskCfgManager.Instance.InitTable();
		TestTableCfgManager.Instance.InitTable();
	}
}
