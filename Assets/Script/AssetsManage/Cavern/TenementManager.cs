using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 房屋
public class TenementManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "房屋";
    string resourceDescription = "用石头搭的房子,可以提升软妹币储量,并且每秒产出少量软妹币";
    int resourceQuantity = 0;

    string btnText = "建造";

    double baseReserves = 500;//基础储量

    double baseYield = 0.6;//人民币基础产量

    double baseRent = 0.5;//每位工人的基础租金

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

        //基础消耗=10石头
        resources[ResourceType.Stone] = 20;
        facilityPanelManager.SetOnClickedResource(resources);  //设置基础消耗
        facilityPanelManager.press = OnMineButtonClicked;

        InstallOutPut();
    }

    FacilityPanelManager stoneMinerManager;//石矿工人
    FacilityPanelManager copperMinerManager;//铜矿工人
    FacilityPanelManager ironMinerManager;//铁矿工人
    FacilityPanelManager cementWorkerManager;//水泥工人

    void Start()
    {
        //初始化影响到的工人
        stoneMinerManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.StoneMiner);
        copperMinerManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.CopperMiner);
        ironMinerManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.IronMiner);
        cementWorkerManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.CementWorker);
    }
    /// <summary>
    /// 统计一下工人总数
    /// </summary>
    /// <returns></returns>
    int GetMinerCount()
    {
        int count = 0;
        count += stoneMinerManager.GetCount();
        count += copperMinerManager.GetCount();
        count += ironMinerManager.GetCount();
        count += cementWorkerManager.GetCount();
        return count;
    }

    /// <summary>
    /// 产出的初始化
    /// </summary>
    void InstallOutPut()
    {
        Dictionary<ResourceType, double> outPutResources = new Dictionary<ResourceType, double>
        {
            [ResourceType.Currency] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);


    }


    /// <summary>
    /// 获取房屋的储量
    /// </summary>
    /// <returns></returns>
    public double GetReserves()
    {
        double reserves = baseReserves;
        reserves *= ResourceAdditionManager.Instance.GetTenementSaveMoneyUp();
        reserves *= facilityPanelManager.GetCount();//乘以数量
        return reserves;
    }


    // Update is called once per frame
    void Update()
    {
        Output();//自动产出
    }
    /// <summary>
    /// 房屋产出软妹币
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {
            double rmb = baseYield * facilityPanelManager.GetCount();//每秒产出软妹币

            rmb *= ResourceAdditionManager.Instance.GetTenementBasicsUp();//房屋基本产量加成


            double rent = 0;//收租
            //如果研究了收租,就算上房租
            if (TechManager.Instance.GetTechFlag(TechType.collectRents))
            {
                rent = baseRent * GetMinerCount();//统计每秒房租
            }

            rent *= ResourceAdditionManager.Instance.GetTenementRentUp();//房屋房租产量加成


            double count = rent + rmb;//房租+房屋本身产出

            count *= ResourceAdditionManager.Instance.GetTenementComfortUp();//房屋坚固程度对房屋的产量加成

            //每帧增加软妹币
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Currency, count * Time.deltaTime);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Currency, secondCount);
        }



    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//房屋数量+1
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新所有储存上限

    }


}
