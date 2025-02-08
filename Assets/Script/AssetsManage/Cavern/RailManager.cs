using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 铁轨
public class RailManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "铁轨";
    string resourceDescription = "用铁和石头搭建铁轨,搭建了50个铁轨之后就可以制造矿车啦!";
    int resourceQuantity = 0;

    string btnText = "搭建";


    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //建造需要的资源

    // Start is called before the first frame update
    void Awake()
    {
        facilityPanelManager = GetComponent<FacilityPanelManager>();
        //绑定按钮
        miningButton = facilityPanelManager.miningButton;
        //全局工具管理
        utilManager = FindObjectOfType<UtilManager>();
        facilityPanelManager.SetResource(resourceName, resourceDescription, resourceQuantity, btnText);

        //基础消耗=1000石头 ,500铁
        resources[ResourceType.Stone] = 1000;
        resources[ResourceType.Iron] = 500;
        facilityPanelManager.SetOnClickedResource(resources);  //设置基础消耗
        facilityPanelManager.press = OnMineButtonClicked;
    }




    void Update()
    {
        if (facilityPanelManager.GetCount() >= 50)
        {
            gameObject.SetActive(false);//隐藏铁轨搭建
        }
    }





    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        LogManager.Instance.AddLog("搭建了一段铁轨");
        facilityPanelManager.AddQuantityUI();//数量+1

        if (facilityPanelManager.GetCount() >= 50)
        {
            gameObject.SetActive(false);//隐藏铁轨搭建
            LogManager.Instance.AddLog("铁轨搭建完成!");

        }

    }



}
