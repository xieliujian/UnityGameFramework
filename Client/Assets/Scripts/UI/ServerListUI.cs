using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Net;

public class ServerListUI : MonoBehaviour
{
    public static ServerListUI Instance = null;

    public GameObject mOk;

    public UILabel mLabel;

	// Use this for initialization
	void Start ()
    {
        Instance = this;
        UIEventListener.Get(mOk).onClick = OnOkBtnClick;

        HeroCfg herocfg = HeroCfgManager.Instance.GetDataByID(1);
        if (herocfg != null)
        {
            mLabel.text = herocfg.Name + "  " + herocfg.AP_Name;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnOkBtnClick(GameObject go)
    {
        NetManager.Instance.SendConnect();
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }
}
