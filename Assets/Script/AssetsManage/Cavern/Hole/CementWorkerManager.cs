using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 水泥搅拌工
public class CementWorkerManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "水泥搅拌工";
    string resourceDescription = "招募一个水泥搅拌工,他会不断的消耗石矿来生产水泥";
    int resourceQuantity = 0;

    string btnText = "招募";


    double baseYield = 0.2;//水泥矿基础产量

    double depletedOre = 10;//每个水泥消耗的石矿

    readonly FacilityType type = FacilityType.CementWorker;


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

        //基础消耗=1k软妹币，600铜矿
        resources[ResourceType.Currency] = 1000;
        resources[ResourceType.Copper] = 600;
        facilityPanelManager.SetOnClickedResource(resources);  //设置基础消耗
        facilityPanelManager.press = OnMineButtonClicked;
        InstallOutPut();
        InstallInput();


        facilityPanelManager.InstallButton();//初始化增加减少的按钮
    }

    void Start()
    {
       
    }


    /// <summary>
    /// 产出的初始化
    /// </summary>
    void InstallOutPut()
    {

        Dictionary<ResourceType, double> outPutResources = new Dictionary<ResourceType, double>
        {
            [ResourceType.Cement] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }
    /// <summary>
    /// 消耗的初始化
    /// </summary>
    void InstallInput()
    {
         //每个水泥消耗的石矿
        depletedOre = ResourceManager.Instance.formula[ResourceType.Cement][ResourceType.Stone];


        Dictionary<ResourceType, double> inPutResources = new Dictionary<ResourceType, double>
        {
            [ResourceType.Stone] = 0
        }; //初始化消耗资源
        facilityPanelManager.InputInstall(inPutResources);
    }




    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 水泥搅拌工产出水泥
    /// </summary>
    void Output()
    {

        double rmb = baseYield * facilityPanelManager.GetCount();//每秒产出水泥

        rmb *= ResourceAdditionManager.Instance.GetWorkerUp();//加上矿洞员工加成的提升

        rmb *= ResourceAdditionManager.Instance.GetCementUp();//加上水泥专属的提升

        rmb *= ResourceAdditionManager.Instance.GetFabricatorUp();//加上制造工人的提升

        double thisYield = rmb * Time.deltaTime;//计算出当前帧产出的水泥
        double expendableStone = thisYield * depletedOre;//计算出当前帧需要消耗的石头       每个水泥需要10个石头，所以每一帧产出的水泥需要水泥倍数X10的石头
        double thisStone = ResourceManager.Instance.GetResource(ResourceType.Stone);

        if (thisStone < expendableStone)
        {
            //如果石矿不足就不进行生产
            return;
        }

        //每帧增加水泥
        IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Cement, thisYield);
        double actualDeductionStone = increment.ActualCount * depletedOre;//计算实际上需要扣除的石头

        //进行石矿的消耗
        ResourceManager.Instance.SpendResource(ResourceType.Stone, actualDeductionStone);//根据实际的产出来消耗石头

        //计算出每秒产出多少资源
        double secondCount = increment.Count / Time.deltaTime;
        facilityPanelManager.UpdateOutPut(ResourceType.Cement, secondCount, false);//更新产出资源
        facilityPanelManager.UpdateInPut(ResourceType.Stone, secondCount * 10);//更新消耗资源,石矿消耗=水泥产出的10倍

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
