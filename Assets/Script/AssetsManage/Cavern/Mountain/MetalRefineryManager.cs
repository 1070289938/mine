using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 金属精炼厂
public class MetalRefineryManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "金属精炼厂";
    string resourceDescription = "每个金属精炼厂都可以大幅度提升铝矿的采集效率";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.MetalRefinery;
    double up = 0.05;//每个金属精炼厂提升的产量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=30k软妹币,50铝
        [ResourceType.Currency] = AssetsUtil.ParseNumber("30k"),
        [ResourceType.Aluminum] = 50,
    };

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
    /// 获得金属精炼厂的提升
    /// </summary>
    public double GetMetalRefineryUp()
    {
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        double basics = 0;//基础值
        basics += up * facilityPanelManager.GetCount();

        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
