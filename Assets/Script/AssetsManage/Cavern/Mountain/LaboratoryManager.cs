using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 实验室
public class LaboratoryManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "实验室";
    string resourceDescription = "每个实验室都可以产生少量的科技点";
    int resourceQuantity = 0;

    string btnText = "建造";

    double baseYield = 0.1;//科技点基础产量
    readonly FacilityType type = FacilityType.Laboratory;

    //建造需要的资源 
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=10k软妹币,500硅
        [ResourceType.Currency] = AssetsUtil.ParseNumber("10k"),
        [ResourceType.Silicon] = AssetsUtil.ParseNumber("500"),

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
            [ResourceType.Science] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);


    }


    // Update is called once per frame
    void Update()
    {
        Output();

    }



    /// <summary>
    /// 科技点产出
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {


            double count = facilityPanelManager.GetCount() * baseYield;//每秒产出科技点
            count *= ResourceAdditionManager.Instance.GetScienceUp();//加上科技点的提升

            //每帧增加科技点
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Science, count * Time.deltaTime);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Science, secondCount, true);
        }


    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1

    }


}
