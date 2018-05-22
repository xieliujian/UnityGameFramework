using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Net;

public class ServerListUI : MonoBehaviour
{
    public static ServerListUI Instance = null;

    public GameObject mOk;

	// Use this for initialization
	void Start ()
    {
        Instance = this;
        UIEventListener.Get(mOk).onClick = OnOkBtnClick;
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
