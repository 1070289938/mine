using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 钛矿采集器
public class TitaniumCollectorManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "钛矿采集器";
    string resourceDescription = "专业采集钛矿的设备,采集器需要由2个人进行操作";
    int resourceQuantity = 0;

    string btnText = "建造";

    double baseYield = 0.004;//钛矿基础产量
    readonly FacilityType type = FacilityType.TitaniumCollector;

    //建造需要的资源 
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=50k软妹币,200硅
        [ResourceType.Currency] = AssetsUtil.ParseNumber("10k"),
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
            [ResourceType.Titanium] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);


    }


    // Update is called once per frame
    void Update()
    {
        Output();

    }



    /// <summary>
    /// 钛矿采集器产出
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {


            double count = facilityPanelManager.GetCount() * baseYield;//每秒产出钛矿


            count *= ResourceAdditionManager.Instance.GetToolUp();//采矿工具加成

            count *= ResourceAdditionManager.Instance.GetMiningWorkerUp();//采矿工人加成

            count *= ResourceAdditionManager.Instance.GetTitaniumUp();//钛矿专属加成

            //每帧增加钛矿
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Titanium, count * Time.deltaTime);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Titanium, secondCount,true);
        }


    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1

    }


}
