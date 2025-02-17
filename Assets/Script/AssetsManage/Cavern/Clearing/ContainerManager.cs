using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 集装箱
public class ContainerManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "集装箱";
    string resourceDescription = "每个集装箱都可以提升仓库的最大储量";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.Container;
    double up = 0.05;//每个集装箱提升的储量


    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=500软妹币，20钢
        [ResourceType.Currency] = 500,
        [ResourceType.Steel] = 20,
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

    /// <summary>
    /// 获取集装箱储量的提升
    /// </summary>
    public double GetUp()
    {
        double basics = 0;//基础值
        if (facilityPanelManager == null)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        basics += up * facilityPanelManager.GetCount();

        basics *= ResourceAdditionManager.Instance.GetContainerUp();//集装箱的加成的加成

        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
          ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新所有储存上限
    }


}
