using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 一层祭坛试炼
public class AltarTrial1Manager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "一层祭坛试炼";
    string resourceDescription = "第一层试炼为“记忆扰动模拟”，需同步应对系统对过往事件的失真重建。(需要150段工程进行建造)";
    int resourceQuantity = 0;

    string btnText = "输入记忆";
    readonly FacilityType type = FacilityType.AltarTrial1;

    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
      
        [ResourceType.memoryAlloy] = AssetsUtil.ParseNumber("50k"),
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
            TechManager.Instance.techTypeStudyFlag[TechType.CompleteAltarTrialLevelOne] = true;
        }
    }





    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        LogManager.Instance.AddLog("输入了一段记忆");
        facilityPanelManager.AddQuantityUI();//数量+1

        if (facilityPanelManager.GetCount() >= 150)
        {
            LogManager.Instance.AddLog("一层祭坛试炼完成!");

        }

    }



}
