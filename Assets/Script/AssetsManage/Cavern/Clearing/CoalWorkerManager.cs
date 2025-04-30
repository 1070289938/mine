using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 煤矿工人
public class CoalWorkerManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;


    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "煤矿工人";
    string resourceDescription = "招募一个煤矿工人,勤奋的煤矿工人会不断的帮你寻找并挖取煤矿";
    int resourceQuantity = 0;

    string btnText = "招募";

    readonly FacilityType type = FacilityType.CoalWorker;
    double baseYield = 0.3;//煤矿基础产量


    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //建造需要的资源

    // Start is called before the first frame update
    void Awake()
    {
        facilityPanelManager = GetComponent<FacilityPanelManager>();
        //绑定按钮
        miningButton = facilityPanelManager.miningButton;
        //全局工具管理
        utilManager = FindObjectOfType<UtilManager>();
        facilityPanelManager.SetResource(resourceName, resourceDescription, resourceQuantity, btnText, type);

        //基础消耗=200软妹币，50铁矿
        resources[ResourceType.Currency] = 200;
        resources[ResourceType.Iron] = 50;
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
            [ResourceType.Colliery] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }




    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 煤矿工人产出煤矿
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {
            double count = baseYield * facilityPanelManager.GetCount();//每秒产出煤矿


            count *= ResourceAdditionManager.Instance.GetMiningWorkerUp();  //加上采矿工人的提升


            count *= ResourceAdditionManager.Instance.GetToolUp();//加上工具对煤矿工人的提升

            count *= ResourceAdditionManager.Instance.GetWorkerUp();//加上矿洞员工加成的提升


            //每帧增加煤矿
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Colliery, count * Time.deltaTime,true);
            Debug.Log(count);
            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Colliery, secondCount,true);
        }



    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
