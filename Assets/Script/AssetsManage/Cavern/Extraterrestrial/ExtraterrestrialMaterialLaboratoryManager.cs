using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 外星材料实验室
public class ExtraterrestrialMaterialLaboratoryManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "外星材料实验室";
    string resourceDescription = "用于研究深空的外星材料,每个都可以提升大量的科技点上限";
    int resourceQuantity = 0;

    string btnText = "建造";

    Dictionary<ResourceType, double> baseReserves = new()
    {
        [ResourceType.Science] = 10000,//科技点储量


    }; //仓库基础储量

    readonly FacilityType type = FacilityType.ExtraterrestrialMaterialLaboratory;





    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {

        //基础消耗=15k软妹币,合金10 科技点80
        [ResourceType.Currency] = AssetsUtil.ParseNumber("10G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("100k"),
        [ResourceType.Flare] = AssetsUtil.ParseNumber("300"),

    }; //建造需要的资源

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


        facilityPanelManager.InstallDemandPoints(1, GetRemainingDemand, AddThisCount);

    }

    int GetRemainingDemand()
    {
        return DeepSpacePanelManager.Instance.GetRemainingDemand();

    }

    void AddThisCount(int count)
    {
        DeepSpacePanelManager.Instance.AddThisCount(count);
    }



    /// <summary>
    /// 获取储量
    /// </summary>
    /// <returns></returns>
    public double GetReserves(ResourceType type)
    {
        double reserves = baseReserves[type];
        if (facilityPanelManager == null)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }

        reserves *= facilityPanelManager.GetCount();//数量相乘

        reserves *= ResourceAdditionManager.Instance.GetTechnologicalUp();//科技上限加成


        return reserves;
    }


    // Update is called once per frame
    void Update()
    {

    }


    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新所有储存上限

    }


}
