using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 矿车
public class OreCarManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "矿车";
    string resourceDescription = "建造一个矿车,每个矿车可以提升2%采矿工人加成";
    int resourceQuantity = 0;

    string btnText = "建造";

    double up = 0.02;//每个矿车提升的产量

    readonly FacilityType type = FacilityType.OreCar;
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

        //基础消耗=2000软妹币,1000铁矿
        resources[ResourceType.Currency] = 2000;
        resources[ResourceType.Iron] = 900;
        facilityPanelManager.SetOnClickedResource(resources);  //设置基础消耗
        facilityPanelManager.press = OnMineButtonClicked;

    }

    /// <summary>
    /// 获取矿车的提升
    /// </summary>
    public double GetOreCarUp()
    {
        double basics = 0;//基础值
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        basics += up * facilityPanelManager.GetCount();

        //总提升再乘矿车效率的提升
        basics *= ResourceAdditionManager.Instance.GetMineBonusUp();
        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//矿车数量+1
    }


}
