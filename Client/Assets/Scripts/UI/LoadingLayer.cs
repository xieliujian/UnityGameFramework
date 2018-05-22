using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class LoadingLayer : MonoBehaviour
{
    bool isTemplate = true;

    public bool IsTemplate
    {
        get { return isTemplate; }
        set { isTemplate = value; }
    }

    public static bool IsShow 
    {
        get { return isShow; }
    }

    public string TemplateName { get; set; }
    public static UISlider progressbar = null;
    public static UILabel textTips = null;

    static bool isShow = false;
    public static LoadingLayer layer = null;
    static string key = "LoadingTemplate";

    
    void Awake()
    {
        layer = this;
    }
     
    public static void Show()
    { 
        layer.gameObject.SetActive(true); 
        Transform trans = layer.GetComponent<Transform>();
        trans.localPosition = Vector3.zero;
        trans.localScale = new Vector3(1, 1, 1);

        if (progressbar == null)
        {
            progressbar = layer.gameObject.GetComponentInChildren<UISlider>();
        }
        if (textTips == null)
        {
            textTips = layer.gameObject.GetComponentInChildren<UILabel>();
        }
         

        isShow = true;
    }

    public static void Hide()
    {
        if (layer)
        {
            //Templates.ReturnCache(layer);
            //layer = null;
            layer.gameObject.SetActive(false);
        }
        
        isShow = false;
    }

    public static void SetProgressbarValue(float rProgress)
    {
        if (progressbar != null)
        {
            progressbar.value = rProgress;
        }
    }

    public static void SetProgressbarTips(string rText)
    {
        if (textTips != null)
        {
            textTips.text = rText;
        }
    }

    public static int GetRenderOrder()
    {
        if (layer != null)
        {
            return layer.transform.GetSiblingIndex();
        }
        else
        {
            throw new NullReferenceException("WaitingLayer is null");
        }
    }

    public static void SetRenderOrder(int order)
    {
        if (layer != null)
        {
            layer.transform.SetSiblingIndex(order);
        }
        else
        {
            throw new NullReferenceException("WaitingLayer is null");
        }
    }

    
    
}
