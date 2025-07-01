using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 光脉干扰控制台
public class InterferenceManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "光脉干扰控制台";
    string resourceDescription = "进行钻野这种神奇现象的控制台,(需要150段工程进行建造)";
    int resourceQuantity = 0;

    string btnText = "建造一段工程";
    readonly FacilityType type = FacilityType.Interference;

    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //需要软妹币1G 秘银1000 中子 900
        [ResourceType.Currency] = AssetsUtil.ParseNumber("8G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("20k"),
        [ResourceType.MudCrystal] = AssetsUtil.ParseNumber("5k"),
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
        if (facilityPanelManager.GetCount() >= 150)
        {
            gameObject.SetActive(false);//隐藏
            TechManager.Instance.techTypeStudyFlag[TechType.CompleteInterference] = true;
        }
    }





    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        LogManager.Instance.AddLog("搭建了一段光脉干扰控制台");
        facilityPanelManager.AddQuantityUI();//数量+1

        if (facilityPanelManager.GetCount() >= 150)
        {
            LogManager.Instance.AddLog("光脉干扰控制台搭建完成!");

        }

    }



}
