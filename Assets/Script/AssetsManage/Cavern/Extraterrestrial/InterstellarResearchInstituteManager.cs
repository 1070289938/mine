using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 星际研究所
public class InterstellarResearchInstituteManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "星际研究所";
    string resourceDescription = "我方与外星人共同建立的研究所,共同探讨世界的奥秘和更加高级的研究,每个星际研究所都会提升研究点的产量";
    int resourceQuantity = 0;

    string btnText = "建造";

    double up = 0.05;

    readonly FacilityType type = FacilityType.InterstellarResearchInstitute;





    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {

        //基础消耗=15k软妹币,合金10 科技点80
        [ResourceType.Currency] = AssetsUtil.ParseNumber("10G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("100k"),
        [ResourceType.Flare] = AssetsUtil.ParseNumber("300"),

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

        facilityPanelManager.InstallDemandPoints(1, GetRemainingDemand, AddThisCount);


    }

    int GetRemainingDemand()
    {
        return DeepSpacePanelManager.Instance.GetRemainingDemand();

    }

    void AddThisCount(int count)
    {
        DeepSpacePanelManager.Instance.AddThisCount(count);
    }



    /// <summary>
    /// 获取提升
    /// </summary>
    /// <returns></returns>
    public double GetUp()
    {
        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        return up * facilityPanelManager.GetCount();//数量相乘    
    }


    // Update is called once per frame
    void Update()
    {

    }


    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1

    }


}
