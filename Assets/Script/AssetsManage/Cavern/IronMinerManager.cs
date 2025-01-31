using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 铁矿工人
public class IronMinerManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;


    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "铁矿工人";
    string resourceDescription = "招募一个铁矿工人,铁矿工人会产出稳定的铁矿，虽然很少就是了";
    int resourceQuantity = 0;

    string btnText = "招募";


    double baseYield = 0.1;//铁矿基础产量


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

        //基础消耗=80软妹币，60石矿
        resources[ResourceType.Currency] = 80;
        resources[ResourceType.Stone] = 60;
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
            [ResourceType.Iron] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }




    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 铁矿工人产出铁矿
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {
            double rmb = baseYield * facilityPanelManager.GetCount();//每秒产出铁矿

            //加上采矿工人的提升
            rmb*=ResourceAdditionManager.Instance.GetMiningWorkerUp();


            rmb *= ResourceAdditionManager.Instance.GetToolUp();//加上工具对铁矿工人的提升

            rmb *= ResourceAdditionManager.Instance.GetWorkerUp();//加上矿洞员工加成的提升
            //每帧增加软妹币
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Iron, rmb * Time.deltaTime);



            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Iron, secondCount);
        }



    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
