using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 证券交易所
public class StockExchangeManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "证券交易所";
    string resourceDescription = "每个证券交易所都可以大幅度的提升软妹币的总储量和少量的产量";
    int resourceQuantity = 0;

    string btnText = "建造一部分";

    readonly FacilityType type = FacilityType.StockExchange;

    //建造需要的资源 
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //每个资源对应的基础产量 软妹币10k 合金10 科技点100
        [ResourceType.Currency] = AssetsUtil.ParseNumber("300k"),
        [ResourceType.Alloy] = 200,
        [ResourceType.Science] = 100,
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
    /// 获得软妹币储量的提升
    /// </summary>
    public double GetReservesUp()
    {
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        double basics = 1;//基础值
        basics += 0.2 * facilityPanelManager.GetCount();
        return basics;
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
        basics += 0.05 * facilityPanelManager.GetCount();
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
