using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 深渊号
public class ConstructingXingyuanShipManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "深渊号";
    string resourceDescription = "专门用于探险虫洞的深渊号,需要500段工程才可以制造好原型";
    int resourceQuantity = 0;

    string btnText = "建造一段工程";
    readonly FacilityType type = FacilityType.ConstructingXingyuanShip;

    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //需要软妹币1G 秘银1000 中子 900
        [ResourceType.Currency] = AssetsUtil.ParseNumber("80G"),
        [ResourceType.MetallicHydrogen] = AssetsUtil.ParseNumber("1G"),
        [ResourceType.Adamant] = AssetsUtil.ParseNumber("10k"),
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




    void Update()
    {
        if (facilityPanelManager.GetCount() >= 500)
        {
            gameObject.SetActive(false);//隐藏
            TechManager.Instance.techTypeStudyFlag[TechType.CompleteAbyss] = true;
        }
    }





    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        LogManager.Instance.AddLog("搭建了一段深渊号工程");
        facilityPanelManager.AddQuantityUI();//数量+1

        if (facilityPanelManager.GetCount() >= 500)
        {
            LogManager.Instance.AddLog("深渊号搭建完成!");

        }

    }



}
