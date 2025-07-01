using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 记忆储存装置
public class MemoryStorageManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "记忆储存装置";
    string resourceDescription = "专门用于储存记忆合金的特殊装置,可以有效的防止记忆在合金内衰退,可以提升记忆合金的储量上限";
    int resourceQuantity = 0;

    string btnText = "建造";

    Dictionary<ResourceType, double> baseReserves = new()
    {
        [ResourceType.memoryAlloy] = 100,//记忆基础储量

    }; //仓库基础储量

    readonly FacilityType type = FacilityType.MemoryStorage;


    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=5M软妹币,科技点2000 石头 1m
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("20k"),
        [ResourceType.MudCrystal] = AssetsUtil.ParseNumber("2k"),
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




    }

    /// <summary>
    /// 获取记忆储存装置的储量
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

        reserves *= ResourceAdditionManager.Instance.GetMemoryCapacityUP();

        return reserves;
    }


    // Update is called once per frame
    void Update()
    {

    }


    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//仓库数量+1
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新所有储存上限

    }


}
