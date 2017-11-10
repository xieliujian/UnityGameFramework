using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Net;

public class GameController : MonoBehaviour
{
    #region 内置函数

	void Awake()
	{
		gameObject.AddComponent<NetManager>();

		NetManager.Instance.SendConnect();
	}

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #endregion
}
