using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 巨型纪念碑
public class GiantMonumentManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "巨型纪念碑";
    string resourceDescription = "给文明竖立高大的纪念碑,每个纪念碑都会提升祭坛的效果";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.Monument;
    double up = 0.1;//每个祭坛提升的产量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=5M软妹币,科技点2000 石头 2m
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1M"),
        [ResourceType.Stone] = AssetsUtil.ParseNumber("2M"),

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
        facilityPanelManager.InstallSchedule();
    }

    /// <summary>
    /// 获得提升
    /// </summary>
    public double GetUp()
    {
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        double basics = 0;//基础值
        basics += up * facilityPanelManager.GetCount();

        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddSchedule(5);
    }


}
