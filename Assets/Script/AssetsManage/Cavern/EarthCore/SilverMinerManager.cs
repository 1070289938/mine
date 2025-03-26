using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 银矿工人
public class SilverMinerManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;//节点管理


    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "银矿工人";
    string resourceDescription = "地心有着大量的银矿可供银矿工人挖掘";
    int resourceQuantity = 0;

    string btnText = "招募";

    double baseYield = 0.1;//银矿基础产量
    readonly FacilityType type = FacilityType.SilverMiner;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //软妹币 6k 合金5 
        [ResourceType.Currency] = 6000,
        [ResourceType.Alloy] = 5,

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
            [ResourceType.silver] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }


    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 银矿工人产出石头
    /// </summary>
    void Output()
    {
        int MinerCount = facilityPanelManager.GetCount();//获取工人数量
        if (MinerCount != 0)
        {
            double output = baseYield * MinerCount;//每秒产出银矿


            output *= ResourceAdditionManager.Instance.GetMiningWorkerUp(); //加上采矿工人的提升

            output *= ResourceAdditionManager.Instance.GetToolUp();//加上工具对银矿工人的提升

            output *= ResourceAdditionManager.Instance.GetWorkerUp();//加上矿洞员工加成的提升

            //每帧增加银矿
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.silver, output * Time.deltaTime);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.silver, secondCount, true);

        }

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//银矿工人数量+1
    }


}
