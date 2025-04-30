using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 铱矿采集器
public class IridiumCollectorManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;


    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "铱矿采集器";
    string resourceDescription = "采集在月球发现的稀有矿物,铱(提供2人口)";
    int resourceQuantity = 0;

    string btnText = "建造";

    readonly FacilityType type = FacilityType.IridiumCollector;
    double baseYield = 0.1;//铱矿基础产量


    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(){
        [ResourceType.Currency] = AssetsUtil.ParseNumber("100k"),
        [ResourceType.Nanomaterials] = 58,
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
            [ResourceType.Iridium] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }




    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 产出铱
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {
            double count = baseYield * facilityPanelManager.GetCount();//每秒产出铱矿

            count *= ResourceAdditionManager.Instance.GetMiningWorkerUp();  //加上采矿工人的提升

            count *= ResourceAdditionManager.Instance.GetToolUp();//加上工具对铱矿工人的提升

            count *= ResourceAdditionManager.Instance.GetCollectorMarkUp();//加上采集器加成

            //每帧增加软妹币
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Iridium, count * Time.deltaTime,true);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Iridium, secondCount, true);
        }



    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
