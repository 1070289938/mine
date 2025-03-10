using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 压缩增幅器
public class MaterialCompressorManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "压缩增幅器";
    string resourceDescription = "为物质压缩器额外装配一个压缩增幅器,每个压缩增幅器都可以提升集装箱对储量的增幅的效果";
    int resourceQuantity = 0;

    string btnText = "建造";
    readonly FacilityType type = FacilityType.MaterialCompressor;
    double up = 0.02;//每个物质压缩器提升的储量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=50k软妹币, 佐里旬 10
        [ResourceType.Currency] = AssetsUtil.ParseNumber("50k"),
        [ResourceType.Zorizun] = 10,
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
    public double GeUp()
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
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新储量上限
    }


}
