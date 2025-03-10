using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 深井电梯
public class DeepShaftElevatorManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "深井电梯";
    string resourceDescription = "建造一段深井电梯,具科学家的判断,大概需要建造200段才可以到达地心深处";
    int resourceQuantity = 0;

    string btnText = "建造一段电梯";
    readonly FacilityType type = FacilityType.DeepShaftElevator;

    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //需要软妹币1M 合金100 佐里旬 20
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1M"),
        [ResourceType.Alloy] = AssetsUtil.ParseNumber("100"),
        [ResourceType.Zorizun] = AssetsUtil.ParseNumber("20"),
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
        if (facilityPanelManager.GetCount() >= 200)
        {
            gameObject.SetActive(false);//隐藏深井电梯
        }
    }





    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        LogManager.Instance.AddLog("搭建了一段深井电梯");
        facilityPanelManager.AddQuantityUI();//数量+1

        if (facilityPanelManager.GetCount() >= 200)
        {
            LogManager.Instance.AddLog("深井电梯搭建完成!");

        }

    }



}
