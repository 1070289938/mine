using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 时空同步装置
public class TimeSpaceSynchronizationManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;



    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "时空同步装置";
    string resourceDescription = "这是一个极其精密的装置,用于同步扰乱的时空,同时提升记忆提炼炉的产量（至少需要1个时空同步装置才可以进行解析原初装置）";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.TimeSpaceSynchronization;
    double up = 0.2;//每个提升的产量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=5M软妹币,科技点2000 石头 1m
        [ResourceType.Currency] = AssetsUtil.ParseNumber("50G"),
         [ResourceType.Science] = AssetsUtil.ParseNumber("1M"),
        [ResourceType.memoryAlloy] = AssetsUtil.ParseNumber("5k"),
       
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
    public double GetUp()
    {
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        double basics = 1;//基础值
        basics += up * facilityPanelManager.GetCount();
       
        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddSchedule(2);
    }


}
