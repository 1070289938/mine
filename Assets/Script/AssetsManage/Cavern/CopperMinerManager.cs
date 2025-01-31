using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 铜矿工人
public class CopperMinerManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "铜矿工人";
    string resourceDescription = "招募一个铜矿工人,铜矿工人会在一堆石矿里面找铜矿";
    int resourceQuantity = 0;

    string btnText = "招募";


    double baseYield = 0.2;//铜矿基础产量

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

        //基础消耗=50软妹币，30石矿
        resources[ResourceType.Currency] = 50;
        resources[ResourceType.Stone] = 30;
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
            [ResourceType.Copper] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);


    }

    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 铜矿工人产出铜矿
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {
            double rmb = baseYield * facilityPanelManager.GetCount();//每秒产出铜矿

           
            rmb *= ResourceAdditionManager.Instance.GetMiningWorkerUp(); //加上采矿工人的提升


            rmb *= ResourceAdditionManager.Instance.GetToolUp();//加上工具对铜矿工人的提升

            rmb *= ResourceAdditionManager.Instance.GetWorkerUp();//加上矿洞员工加成的提升
            //每帧增加软妹币
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Copper, rmb * Time.deltaTime);


            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Copper, secondCount);
        }



    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
