using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 石矿工人
public class StoneMinerManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;//节点管理


    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "石矿工人";
    string resourceDescription = "招募一个石矿工人,每个石矿工人会自动挖取石矿";
    int resourceQuantity = 0;

    string btnText = "招募";

    double baseYield = 0.3;//石矿基础产量

    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //建造需要的资源

    // Start is called before the first frame update
    void Awake()
    {
        facilityPanelManager = GetComponent<FacilityPanelManager>();
        //绑定按钮
        miningButton = facilityPanelManager.miningButton;
        //全局工具管理
        utilManager = FindObjectOfType<UtilManager>();
        facilityPanelManager.SetResource(resourceName, resourceDescription, resourceQuantity, btnText);

        //基础消耗=20软妹币
        resources[ResourceType.Currency] = 20;
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
            [ResourceType.Stone] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }


    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 石矿工人产出石头
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {
            double rmb = baseYield * facilityPanelManager.GetCount();//每秒产出石头


            rmb *= ResourceAdditionManager.Instance.GetMinerStoneUp(); //加上大锤对石矿工人的提升


            rmb *= ResourceAdditionManager.Instance.GetMiningWorkerUp(); //加上采矿工人的提升

            rmb *= ResourceAdditionManager.Instance.GetToolUp();//加上工具对石矿工人的提升

            rmb *= ResourceAdditionManager.Instance.GetWorkerUp();//加上矿洞员工加成的提升

            //每帧增加石矿
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Stone, rmb * Time.deltaTime);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Stone, secondCount);

        }



    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//石矿工人数量+1
    }


}
