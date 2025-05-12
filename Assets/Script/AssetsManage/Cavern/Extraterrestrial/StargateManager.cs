using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 星际之门
public class StargateManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "星际之门";
    string resourceDescription = "星际之门简称星门,可以快速的进行超远距离的定向跃迁,每个星际之门都可以大幅提升深空部署量";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.Stargate;
    int up = 15;//每个星际之门提升的位置数量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=500科技点,200铱
        [ResourceType.Currency] = AssetsUtil.ParseNumber("6G"),
        [ResourceType.Copper] = AssetsUtil.ParseNumber("500G"),
        [ResourceType.Stone] = AssetsUtil.ParseNumber("500G"),
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
        facilityPanelManager.InstallSchedule();

    }


    /// <summary>
    /// 获得提升
    /// </summary>
    public int GetUp()
    {
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }

        int basics = up;//+ ResourceAdditionManager.Instance.GetColonizationUp();

        basics *= facilityPanelManager.GetCount();

        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddSchedule(5);
    }


}
