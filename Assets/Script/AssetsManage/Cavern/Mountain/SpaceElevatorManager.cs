using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 太空电梯
public class SpaceElevatorManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "太空电梯";
    string resourceDescription = "建造一段太空电梯,如果想要到达稳定的太空环境预计要250段工程";
    int resourceQuantity = 0;

    string btnText = "建造一段电梯";
    readonly FacilityType type = FacilityType.SpaceElevator;

    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //需要软妹币1M 合金100 佐里旬 20
        [ResourceType.Currency] = AssetsUtil.ParseNumber("50M"),
        [ResourceType.Nanomaterials] = AssetsUtil.ParseNumber("4000"),
        [ResourceType.silver] = AssetsUtil.ParseNumber("3M"),
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
            gameObject.SetActive(false);//隐藏太空电梯
            TechManager.Instance.techTypeStudyFlag[TechType.CompletionConstruction] = true;
        }
    }





    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        LogManager.Instance.AddLog("搭建了一段太空电梯");
        facilityPanelManager.AddQuantityUI();//数量+1

        if (facilityPanelManager.GetCount() >= 250)
        {
            LogManager.Instance.AddLog("太空电梯搭建完成!");

        }

    }



}
