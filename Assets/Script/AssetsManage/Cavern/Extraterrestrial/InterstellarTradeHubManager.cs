using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 星际交易枢纽
public class InterstellarTradeHubManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "星际交易枢纽";
    string resourceDescription = "用于与外星人交易的站点,每个都可以大幅度的提升软妹币的产量";
    int resourceQuantity = 0;

    string btnText = "建造一部分";

    readonly FacilityType type = FacilityType.InterstellarTradeHub;

    //建造需要的资源 
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //每个资源对应的基础产量 软妹币10k 合金10 科技点100
        [ResourceType.Currency] = AssetsUtil.ParseNumber("30G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("1M"),
        [ResourceType.Copper] = AssetsUtil.ParseNumber("60G"),

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


        facilityPanelManager.InstallSchedule();//初始化进度条

    }


    void Start()
    {

    }

    /// <summary>
    /// 获得软妹币产量的提升
    /// </summary>
    public double GetYieldUp()
    {
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        double basics = 0;//基础值
        basics += 0.2 * facilityPanelManager.GetCount();
        return basics;
    }

    // Update is called once per frame
    void Update()
    {


    }







    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        facilityPanelManager.AddSchedule(5);//进度+5%

    }


}
