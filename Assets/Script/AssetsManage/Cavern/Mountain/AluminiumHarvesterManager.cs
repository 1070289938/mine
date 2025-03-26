using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 铝矿采集器
public class AluminiumHarvesterManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "铝矿采集器";
    string resourceDescription = "专业采集铝矿的设备,采集器需要由2个人进行操作";
    int resourceQuantity = 0;

    string btnText = "建造";

    double baseYield = 0.01;//铝矿基础产量
    readonly FacilityType type = FacilityType.AluminiumHarvester;

    //建造需要的资源 
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=18k软妹币,200硅
        [ResourceType.Currency] = AssetsUtil.ParseNumber("30k"),
        [ResourceType.Silicon] = 200,
    };

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
            [ResourceType.Aluminum] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);


    }


    // Update is called once per frame
    void Update()
    {
        Output();

    }



    /// <summary>
    /// 铝矿采集器产出
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {


            double count = facilityPanelManager.GetCount() * baseYield;//每秒产出铝矿


            count *= ResourceAdditionManager.Instance.GetAluminumUp();//铝矿产量专项加成   


            count *= ResourceAdditionManager.Instance.GetToolUp();//采矿工具加成

            count *= ResourceAdditionManager.Instance.GetMiningWorkerUp();//采矿工人加成

            count *= ResourceAdditionManager.Instance.GetCollectorMarkUp();//加上采集器的提升
            //每帧增加软妹币
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Aluminum, count * Time.deltaTime);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Aluminum, secondCount,true);
        }


    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1

    }


}
