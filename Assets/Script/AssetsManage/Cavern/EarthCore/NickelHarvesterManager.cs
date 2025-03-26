using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 镍矿采集器
public class NickelHarvesterManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;//节点管理


    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "镍矿采集器";
    string resourceDescription = "只有地心才拥有极少的镍矿,只有采集器可以采集镍矿（提升2名工人）";
    int resourceQuantity = 0;

    string btnText = "建造";

    double baseYield = 0.02;//镍矿基础产量
    readonly FacilityType type = FacilityType.NickelHarvester;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //软妹币 6k 银 100 
        [ResourceType.Currency] = 6000,
        [ResourceType.silver] = 100,

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
            [ResourceType.Nickel] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }


    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 产出镍矿
    /// </summary>
    void Output()
    {
        int MinerCount = facilityPanelManager.GetCount();//获取工人数量
        if (MinerCount != 0)
        {
            double output = baseYield * MinerCount;//每秒产出镍矿


            output *= ResourceAdditionManager.Instance.GetMiningWorkerUp(); //加上采矿工人的提升

            output *= ResourceAdditionManager.Instance.GetToolUp();//加上工具的提升

            output *= ResourceAdditionManager.Instance.GetCollectorMarkUp();//加上采集器的提升
            //每帧增加镍矿
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Nickel, output * Time.deltaTime);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Nickel, secondCount, true);

        }

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
