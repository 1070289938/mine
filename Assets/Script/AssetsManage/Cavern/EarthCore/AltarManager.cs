using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 祭坛
public class AltarManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;



    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "祭坛";
    string resourceDescription = "在这里搭建祭坛貌似有神奇的效果,每个祭坛都会提升所有资源的生产效率";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.Altar;
    double up = 0.02;//每个祭坛提升的产量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=5M软妹币,科技点2000 石头 1m
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5M"),
        [ResourceType.Science] = 2000,
        [ResourceType.Stone] = AssetsUtil.ParseNumber("1M"),

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
        basics *= ResourceAdditionManager.Instance.GetAltarUp();
        if (TechManager.Instance.GetTechFlag(TechType.fanatic))
        {
            if (!temple)
            {
                temple = FacilityManager.Instance.GetFacilityPanel(FacilityType.Temple);
            }

            basics *= (temple.GetCount() * 0.05) + 1;
        }
        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
