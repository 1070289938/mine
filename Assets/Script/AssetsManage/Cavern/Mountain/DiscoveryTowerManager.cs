using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 科技探索塔
public class DiscoveryTowerManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "科技探索塔";
    string resourceDescription = "科技探索塔可以大幅度提升科技点的上限";
    int resourceQuantity = 0;

    string btnText = "建造";

    Dictionary<ResourceType, double> baseReserves = new()
    {
        [ResourceType.Science] = 1000,//科技点储量


    }; //仓库基础储量

    readonly FacilityType type = FacilityType.DiscoveryTower;

    double up = 1;


    // double baseReserves = 100000;//基础储量


    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {

        //基础消耗=15k软妹币,合金10 科技点80
        [ResourceType.Currency] = AssetsUtil.ParseNumber("15k"),
        [ResourceType.Alloy] = AssetsUtil.ParseNumber("10"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("80"),
    }; //建造需要的资源

    // Start is called before the first frame update
    void Awake()
    {
        facilityPanelManager = GetComponent<FacilityPanelManager>();
        //绑定按钮
        miningButton = facilityPanelManager.miningButton;
        //全局工具管理
        utilManager = FindObjectOfType<UtilManager>();
        facilityPanelManager.SetResource(resourceName, resourceDescription, resourceQuantity, btnText, type);


        facilityPanelManager.SetOnClickedResource(resources);  //设置基础消耗
        facilityPanelManager.press = OnMineButtonClicked;




    }
    /// <summary>
    /// 增加储存倍率提升
    /// </summary>
    public void AddUp(double magnification)
    {
        up *= 1 + magnification;
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新所有储存上限
    }

    FacilityPanelManager geocentricStudy;

    /// <summary>
    /// 获取储量
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

        //地心资料库
        if (TechManager.Instance.GetTechFlag(TechType.DataBank))
        {
            if (!geocentricStudy)
            {
                geocentricStudy = FacilityManager.Instance.GetFacilityPanel(FacilityType.GeocentricStudy);
            }

            reserves *= (geocentricStudy.GetCount() * 0.08) + 1;
        }
        reserves *= ResourceAdditionManager.Instance.GetTechnologicalUp();//科技上限加成


        return reserves;
    }


    // Update is called once per frame
    void Update()
    {

    }


    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新所有储存上限

    }


}
