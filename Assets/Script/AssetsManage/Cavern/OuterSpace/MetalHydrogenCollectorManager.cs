using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 金属氢采集器
public class MetalHydrogenCollectorManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;


    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "金属氢采集器";
    string resourceDescription = "在木星上部署的轨道采集器,用于采集木星上的无穷无尽的金属氢";
    int resourceQuantity = 0;

    string btnText = "建造";

    readonly FacilityType type = FacilityType.MetallicHydrogenCollector;
    double baseYield = 5;//金属氢基础产量


    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.Science] = AssetsUtil.ParseNumber("50k"),
        [ResourceType.Neutron] = 5000,
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

        InstallOutPut();
     

    }




    /// <summary>
    /// 产出的初始化
    /// </summary>
    void InstallOutPut()
    {

        Dictionary<ResourceType, double> outPutResources = new Dictionary<ResourceType, double>
        {
            [ResourceType.MetallicHydrogen] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }




    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 产出资源
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {
            double count = baseYield * facilityPanelManager.GetCount();//每秒产出
            count *= ResourceAdditionManager.Instance.GetMiningWorkerUp();  //加上采矿工人的提升

            count *= ResourceAdditionManager.Instance.GetToolUp();//加上工具的提升

            count *= ResourceAdditionManager.Instance.GetCollectorMarkUp();//加上采集器加成

            // count *= ResourceAdditionManager.Instance.GetNeutronUp();//加上专属加成

            //每帧增加资源
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.MetallicHydrogen, count * Time.deltaTime,true);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.MetallicHydrogen, secondCount, true);
        }



    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
