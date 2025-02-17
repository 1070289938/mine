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
    readonly FacilityType type = FacilityType.StoneMiner;
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

        //基础消耗=20软妹币
        resources[ResourceType.Currency] = 20;
        facilityPanelManager.SetOnClickedResource(resources);  //设置基础消耗
        facilityPanelManager.press = OnMineButtonClicked;


        InstallOutPut();
    }
    ExcavatorManager excavatorManager;//挖掘机管理
    void Start()
    {
        Debug.Log("初始化");
        excavatorManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.Excavator).GetComponent<ExcavatorManager>();//初始化挖掘机管理
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
        int MinerCount = facilityPanelManager.GetCount();//获取工人数量
        if (MinerCount != 0)
        {
            double output = baseYield * MinerCount;//每秒产出石头

            output = CalculateImpactExcavator(output);//计算挖机的影响

            output *= ResourceAdditionManager.Instance.GetMinerStoneUp(); //加上大锤对石矿工人的提升

            output *= ResourceAdditionManager.Instance.GetMiningWorkerUp(); //加上采矿工人的提升

            output *= ResourceAdditionManager.Instance.GetToolUp();//加上工具对石矿工人的提升

            output *= ResourceAdditionManager.Instance.GetWorkerUp();//加上矿洞员工加成的提升

            //每帧增加石矿
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Stone, output * Time.deltaTime);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Stone, secondCount);

        }

    }

    /// <summary>
    /// 计算挖机的影响
    /// </summary>
    /// <returns>受影响后的值</returns>
    double CalculateImpactExcavator(double output)
    {
        int MinerCount = facilityPanelManager.GetCount();//获取工人数量
    

        int upCount = excavatorManager.GetCount();//获取挖机数量

        //如果挖掘机的数量不低于0,那就先根据挖掘机提升一下对应的工人效率
        if (upCount != 0)
        {
            //如果挖机数量大于工人数量那就提升所有工人
            if (upCount > MinerCount)
            {
                upCount = MinerCount;
            }
            //计算拥有挖机的工人产量
            double upOutput = baseYield * upCount * excavatorManager.GetUp();//每秒产出石头

            //计算没有挖机的工人产量  没有挖机的工人数量=总工人数量-挖机数量
            double normalOutput = baseYield * (MinerCount - upCount);//每秒产出石头
            output = upOutput + normalOutput;
            return output;
        }
        //返回默认
        return output;
    }


    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//石矿工人数量+1
    }


}
