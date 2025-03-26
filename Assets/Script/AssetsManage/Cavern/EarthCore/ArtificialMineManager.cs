using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 人造矿井
public class ArtificialMineManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "人造矿井";
    string resourceDescription = "人工搭建的矿井,每人造矿井都会提升所有采集器的生产效率";
    int resourceQuantity = 0;

    string btnText = "建造一小部分";
    readonly FacilityType type = FacilityType.ArtificialMine;
    double up = 0.2;//每个人造矿井提升的产量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=5M软妹币,200钨
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5M"),
        [ResourceType.Tungsten] = 200,
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

    /// <summary>
    /// 获得提升
    /// </summary>
    public double GetUp()
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

        facilityPanelManager.AddSchedule(5);//进度+5%
    }


}
