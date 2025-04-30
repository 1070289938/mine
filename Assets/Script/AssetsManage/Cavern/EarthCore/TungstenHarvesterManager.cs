using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 钨矿采集器
public class TungstenHarvesterManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;//节点管理


    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "钨矿采集器";
    string resourceDescription = "钨矿十分的坚硬,而且飞船的好用,就是很好用（提升2名工人）";
    int resourceQuantity = 0;

    string btnText = "建造";

    double baseYield = 0.025;//钨矿基础产量
    readonly FacilityType type = FacilityType.TungstenHarvester;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //软妹币 6k 银 100 
        [ResourceType.Currency] = 8000,
        [ResourceType.silver] = 110,

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
            [ResourceType.Tungsten] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }


    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 产出
    /// </summary>
    void Output()
    {
        int MinerCount = facilityPanelManager.GetCount();//获取工人数量
        if (MinerCount != 0)
        {
            double output = baseYield * MinerCount;//每秒产出


            output *= ResourceAdditionManager.Instance.GetMiningWorkerUp(); //加上采矿工人的提升

            output *= ResourceAdditionManager.Instance.GetToolUp();//加上工具的提升
            output *= ResourceAdditionManager.Instance.GetCollectorMarkUp();//加上采集器的提升

            //每帧增加镍矿
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Tungsten, output * Time.deltaTime,true);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Tungsten, secondCount, true);

        }

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
