using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 手动捕捉暮晶
public class CatchPanelManager : MonoBehaviour
{

    Button miningButton; // 挖矿按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "捕捉暮晶";
    string resourceDescription = "手动进行捕捉附着在各个角落的暮晶";
    int resourceQuantity = 1;

    string btnText = "捕捉";
    readonly FacilityType type = FacilityType.CatchPanel;
    // Start is called before the first frame update
    void Awake()
    {
        facilityPanelManager = GetComponent<FacilityPanelManager>();
        //绑定按钮
        miningButton = facilityPanelManager.miningButton;
        //全局工具管理
        utilManager = FindObjectOfType<UtilManager>();
        facilityPanelManager.SetResource(resourceName, resourceDescription, resourceQuantity, btnText, type);
        facilityPanelManager.SetOnClickedResource(new Dictionary<ResourceType, double>());//挖矿不需要资源
        facilityPanelManager.press = OnMineButtonClicked;
        facilityPanelManager.hideRequirement();//隐藏价格

    }


    // 点击挖矿按钮时触发
    private void OnMineButtonClicked()
    {
        double count = 1;
        count *= ResourceAdditionManager.Instance.GetPowerUp();//加上力量的提升
        ResourceManager.Instance.AddResource(ResourceType.MudCrystal, count, true); // 增加暮晶
    }

}
