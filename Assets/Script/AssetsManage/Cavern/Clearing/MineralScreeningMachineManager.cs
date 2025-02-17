using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 矿物筛选机
public class MineralScreeningMachineManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "矿物筛选机";
    string resourceDescription = "每个矿物筛选机都可以提升铜矿和铁矿的产量";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.MineralScreeningMachine;
    double up = 0.05;//每个矿物筛选机提升的产量


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

        //基础消耗=1000软妹币,300铜矿，200铁矿
        resources[ResourceType.Currency] = 1000;
        resources[ResourceType.Copper] = 300;
        resources[ResourceType.Iron] = 200;
        facilityPanelManager.SetOnClickedResource(resources);  //设置基础消耗
        facilityPanelManager.press = OnMineButtonClicked;

    }

    /// <summary>
    /// 获取筛选器的提升
    /// </summary>
    public double GetscreenUp()
    {
        double basics = 0;//基础值
        if (facilityPanelManager == null)
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
