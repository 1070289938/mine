using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 深空信标
public class DeepSpaceBeaconManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "深空信标";
    string resourceDescription = "部署在深空的位置信标，可以引领远航的船只找到回家的路";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.DeepSpaceBeacon;
    int up = 1;//每个深空信标提升的位置数量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=500科技点,200铱
        [ResourceType.Science] = AssetsUtil.ParseNumber("50k"),
        [ResourceType.Neutron] = 500,
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

    public SubspaceStarMarkerManager subspaceStarMarkerManager;

    /// <summary>
    /// 获得提升
    /// </summary>
    public int GetUp()
    {
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }

        int basics = up+ subspaceStarMarkerManager.GetUp();

        basics *= facilityPanelManager.GetCount();

        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
