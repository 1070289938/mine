using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 实验型探索者
public class ExperimentalExplorerManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "实验型探索者";
    string resourceDescription = "实验型探索者是一个科学家提出来的很疯狂的想法,据说如果实验成功就可以用它来星际旅行（需要250段工程）";
    int resourceQuantity = 0;

    string btnText = "建造一段工程";
    readonly FacilityType type = FacilityType.ExperimentalExplorer;

    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //需要软妹币1G 秘银1000 中子 900
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1.5G"),
        [ResourceType.Mithril] = AssetsUtil.ParseNumber("2500"),
        [ResourceType.Neutron] = AssetsUtil.ParseNumber("2400"),
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
        if (facilityPanelManager.GetCount() >= 250)
        {
            gameObject.SetActive(false);//隐藏
            TechManager.Instance.techTypeStudyFlag[TechType.CompleteExperimentalExplorer] = true;
        }
    }





    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        LogManager.Instance.AddLog("搭建了一段实验型探索者工程");
        facilityPanelManager.AddQuantityUI();//数量+1

        if (facilityPanelManager.GetCount() >= 250)
        {
            LogManager.Instance.AddLog("实验型探索者搭建完成!");

        }

    }



}
