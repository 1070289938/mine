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

    Dictionary<ResourceType, double> baseReserves = new()
    {
        [ResourceType.Stone] = 800,//石矿基础储量
        [ResourceType.Copper] = 680,//铜矿基础储量
        [ResourceType.Iron] = 500,//铁矿基础储量
        [ResourceType.Cement] = 200,//水泥基础储量
        [ResourceType.Colliery] = 500,//煤矿基础储量
        [ResourceType.silver] = 300,//银矿基础储量
        [ResourceType.GeocentricRock] = 500,//地心岩基础储量

        [ResourceType.MetallicHydrogen] = 900,//金属氢基础储量

    }; //仓库基础储量

    readonly FacilityType type = FacilityType.Stash;

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
        facilityPanelManager.SetResource(resourceName, resourceDescription, resourceQuantity, btnText, type);

        //基础消耗=10石头
        resources[ResourceType.Stone] = 30;
        facilityPanelManager.SetOnClickedResource(resources);  //设置基础消耗
        facilityPanelManager.press = OnMineButtonClicked;




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
        if (facilityPanelManager == null)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }

        reserves *= facilityPanelManager.GetCount();//数量相乘
        reserves *= up;//储存倍率相乘
        reserves *= ResourceAdditionManager.Instance.GetStashUp();
        //如果研究了多维仓库就根据四维宝石提升储量（每个提升1%）
        if (TechManager.Instance.GetTechFlag(TechType.MultidimensionalWarehouse))
        {
            double magnification = (ResourceManager.Instance.GetResource(ResourceType.DimensionalStone) * 0.01) + 1;
            reserves *= magnification;//储存倍率相乘
        }

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
