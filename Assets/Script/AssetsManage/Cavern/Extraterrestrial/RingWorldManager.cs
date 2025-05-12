using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 环世界
public class RingWorldManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;



    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "环世界";
    string resourceDescription = "建立于恒星轨道的环世界,可以居住堪比100颗星球的人口数量,环境舒适甚至可以自由控制气候,每个环世界都可以大幅提升所有资源的产量";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.RingWorld;
    double up = 0.2;//每个祭坛提升的产量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=5M软妹币,科技点2000 石头 1m
        [ResourceType.Currency] = AssetsUtil.ParseNumber("50G"),
         [ResourceType.Stone] = AssetsUtil.ParseNumber("1M"),
        [ResourceType.Alloy] = AssetsUtil.ParseNumber("200k"),
       
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
    FacilityPanelManager temple;//寺庙
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

        facilityPanelManager.AddSchedule(5);
    }


}
