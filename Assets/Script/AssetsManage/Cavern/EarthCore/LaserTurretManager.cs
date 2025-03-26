using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 激光炮塔
public class LaserTurretManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "激光炮塔";
    string resourceDescription = "激光炮塔完全弥补了自动炮塔的缺点,但是火力却远远不足自动炮塔,可以给自动炮塔作为辅佐的左右";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.LaserTurret;
    double combatPower = 250;//自动炮塔提供的战斗力

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=1M软妹币,地心岩100 铁矿 5K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("12k"),
        [ResourceType.GeocentricRock] = AssetsUtil.ParseNumber("2k"),
        [ResourceType.Silicon] = AssetsUtil.ParseNumber("5k"),
        [ResourceType.Nanomaterials] = 10

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
        basics += combatPower * facilityPanelManager.GetCount();

        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
