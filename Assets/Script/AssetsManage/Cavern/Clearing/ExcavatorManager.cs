using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 挖掘机
public class ExcavatorManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "挖掘机";
    string resourceDescription = "给石矿工人配置挖掘机,每个挖掘机都可以大幅的提升一个石矿工人的产量";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.Excavator;
    double up = 3;//挖掘机提升的产量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=100软妹币,200铜矿，5钢
        [ResourceType.Currency] = 100,
        [ResourceType.Copper] = 200,
        [ResourceType.Steel] = 5,
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
    /// 获得挖掘机的提升
    /// </summary>
    public double GetUp()
    {
        double basics = 0;//基础值
        basics += up;

        basics *= ResourceAdditionManager.Instance.GetExcavatorUp();
        return basics;
    }

    /// <summary>
    /// 获得挖掘机的数量
    /// </summary>
    public int GetCount()
    {
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        return facilityPanelManager.GetCount();
    }

    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
