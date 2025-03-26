using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 兵营
public class BarracksManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "兵营";
    string resourceDescription = "可以提供士兵来保护你的一切,每个兵营都可以提升士兵的上限";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.Barracks;
    int count = 1;//每个兵营提供的士兵数量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=5M 软妹币, 铁 1K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2M"),
        [ResourceType.Iron] = AssetsUtil.ParseNumber("1K"),

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

    public void AddCount(int count){
        this.count+=count;
    }

    /// <summary>
    /// 获得士兵数量
    /// </summary>
    public int GetCount()
    {
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        int basics = 0;//基础值

        

        basics += count * facilityPanelManager.GetCount();
       
       
        return basics;

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
        BattlePanelManager.Instance.UpdateSoldierMax();
    }


}
