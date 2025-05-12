using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 奥德修斯号
public class OdysseusManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "奥德修斯号";
    string resourceDescription = "奥德修斯号是预订为太阳系内最大的工程舰,制造它的核心目的是为了去收集木星上的稀有资源（需要500段工程）";
    int resourceQuantity = 0;

    string btnText = "建造一段工程";
    readonly FacilityType type = FacilityType.Odysseus;

    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //需要软妹币1G 秘银1000 中子 900
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2.5G"),
        [ResourceType.Mithril] = AssetsUtil.ParseNumber("3500"),
        [ResourceType.Neutron] = AssetsUtil.ParseNumber("4000"),
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
            TechManager.Instance.techTypeStudyFlag[TechType.CompleteOdysseus] = true;
        }
    }





    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        LogManager.Instance.AddLog("搭建了一段奥德修斯号工程");
        facilityPanelManager.AddQuantityUI();//数量+1

        if (facilityPanelManager.GetCount() >= 500)
        {
            LogManager.Instance.AddLog("奥德修斯号搭建完成!");

        }

    }



}
