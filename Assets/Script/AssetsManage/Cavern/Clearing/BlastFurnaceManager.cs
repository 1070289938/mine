using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 高炉
public class BlastFurnaceManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "高炉";
    string resourceDescription = "高炉可以更加有效的将铁矿从石头子分离,从而提升铁矿的总产量";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.BlastFurnace;
    double up = 0.05;//每个高炉提升的产量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=5000软妹币,1000铜矿，50钢
        [ResourceType.Currency] = 5000,
        [ResourceType.Copper] = 1000,
        [ResourceType.Steel] = 50,
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
    /// 获得高炉的提升
    /// </summary>
    public double GetBlastFurnaceUp()
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

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
