using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 佐里旬矿探测器
public class ZoriMineDetectorManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "佐里旬矿探测器";
    string resourceDescription = "探测器会尽可能的探测佐里旬矿,可以产生极少的佐里旬矿";
    int resourceQuantity = 0;

    string btnText = "建造";

    double baseYield = 0.1;//佐里旬基础产量
    readonly FacilityType type = FacilityType.ZoriMineDetector;

    //建造需要的资源 
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=5k软妹币,200科技点 10 合金
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5k"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("200"),
        [ResourceType.Alloy] = AssetsUtil.ParseNumber("10"),

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
            [ResourceType.Zorizun] = 0
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
        if (facilityPanelManager.GetCount() != 0)
        {


            double count = facilityPanelManager.GetCount() * baseYield;//每秒产出佐里旬



            //每帧增加佐里旬
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Zorizun, count * Time.deltaTime,true);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Zorizun, secondCount,true);
        }


    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1

    }


}
