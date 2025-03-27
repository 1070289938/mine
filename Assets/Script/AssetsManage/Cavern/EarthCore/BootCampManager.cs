using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 新兵训练营
public class BootCampManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "新兵训练营";
    string resourceDescription = "新兵训练营可以提升兵营的产兵效率,更快的投放新的士兵到达战场";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.BootCamp;
    double up = 0.05;//每个新兵训练营提升的加成

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=1M 软妹币, 铜 2K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1M"),
        [ResourceType.Copper] = AssetsUtil.ParseNumber("200K"),

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
        basics *= ResourceAdditionManager.Instance.GetTrainingCampEfficiencyUp();

        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
