using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 仓库
public class StashManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "仓库";
    string resourceDescription = "储存资源用的建筑,每一个仓库都可以提升基础资源的最大储量";
    int resourceQuantity = 0;

    string btnText = "建造";

    Dictionary<ResourceType, double> baseReserves = new Dictionary<ResourceType, double>(); //仓库基础储量



    double up = 1;


    // double baseReserves = 100000;//基础储量


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

        //基础消耗=10石头
        resources[ResourceType.Stone] = 30;
        facilityPanelManager.SetOnClickedResource(resources);  //设置基础消耗
        facilityPanelManager.press = OnMineButtonClicked;


        baseReserves[ResourceType.Stone] = 500;//石矿基础储量
        baseReserves[ResourceType.Copper] = 380;//铜矿基础储量
        baseReserves[ResourceType.Iron] = 200;//铁矿基础储量
        baseReserves[ResourceType.Cement] = 100;//水泥基础储量
    }
    /// <summary>
    /// 增加仓库储存倍率提升
    /// </summary>
    public void AddUp(double magnification)
    {
        up *= 1 + magnification;
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新所有储存上限
    }

    /// <summary>
    /// 获取仓库的储量
    /// </summary>
    /// <returns></returns>
    public double GetReserves(ResourceType type)
    {
        double reserves = baseReserves[type];
        reserves *= facilityPanelManager.GetCount();//数量相乘
        reserves *= up;//储存倍率相乘
        return reserves;
    }


    // Update is called once per frame
    void Update()
    {

    }


    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//仓库数量+1
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新所有储存上限

    }


}
