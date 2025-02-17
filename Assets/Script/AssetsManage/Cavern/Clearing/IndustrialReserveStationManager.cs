using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 工业储备站
public class IndustrialReserveStationManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "工业储备站";
    string resourceDescription = "可以提升工业材料的最大储量";
    int resourceQuantity = 0;

    string btnText = "建造";

    Dictionary<ResourceType, double> baseReserves = new Dictionary<ResourceType, double>()
    {//仓库基础储量
        [ResourceType.Steel] = AssetsUtil.ParseNumber("1k"),//钢
        [ResourceType.Silicon] = AssetsUtil.ParseNumber("500"),//硅
    };

    readonly FacilityType type = FacilityType.IndustrialReserveStation;



    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {//基础建造资源500软妹币，200水泥
        [ResourceType.Currency] = 500,
        [ResourceType.Cement] = 200,
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
    /// 获取工业储备站的最大储量
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
