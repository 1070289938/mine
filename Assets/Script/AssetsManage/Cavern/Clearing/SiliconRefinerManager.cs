using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 硅石精炼器
public class SiliconRefinerManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "硅石精炼器";
    string resourceDescription = "硅石精炼器可以提升从硅石中提取的硅矿,每个硅石精炼器都可以提升硅矿的产量";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.SiliconRefiner;
    double up = 0.05;//每个硅石精炼器提升的产量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=10k软妹币,800煤矿，500钢
        [ResourceType.Currency] = AssetsUtil.ParseNumber("10k"),
        [ResourceType.Colliery] = 800,
        [ResourceType.Steel] = 200,
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
    /// 获得硅石精炼器的提升
    /// </summary>
    public double GetUp()
    {
        double basics = 1;//基础值
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        basics += up * facilityPanelManager.GetCount();

        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
