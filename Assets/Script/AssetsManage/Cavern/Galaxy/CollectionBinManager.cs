using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 晶体收集仓
public class CollectionBinManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "晶体收集仓";
    string resourceDescription = "附加在晶体捕捉装置的收集仓,可以使晶体捕捉装置的效率进一步的提高";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.CollectionBin;
    double up = 0.01;//每个晶体收集仓提升的产量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
       
        [ResourceType.Currency] = AssetsUtil.ParseNumber("10G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("50k"),
        [ResourceType.MudCrystal] = AssetsUtil.ParseNumber("1200"),
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
        double basics = 0;//基础值
        basics += up * facilityPanelManager.GetCount();
        basics *= ResourceAdditionManager.Instance.GetTempleUp();
        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
