using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 酒馆
public class TavernManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "酒馆";
    string resourceDescription = "每个酒馆都可以产出中量的软妹币并且每个酒馆都可以提升房屋的总产量";
    int resourceQuantity = 0;

    string btnText = "建造";


    readonly FacilityType type = FacilityType.Tavern;

    //建造需要的资源 
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=18k软妹币,100钢
        [ResourceType.Currency] = AssetsUtil.ParseNumber("18k"),
        [ResourceType.Steel] = 100,
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

    /// <summary>
    /// 提升房屋的产量
    /// </summary>
    /// <returns></returns>
    public double GetUp()
    {

        //每个酒馆提升1%
        if (facilityPanelManager == null)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }

        return 0.01 * facilityPanelManager.GetCount();


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
            [ResourceType.Currency] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);


    }


    // Update is called once per frame
    void Update()
    {
        Output();//酒馆产出

    }



    /// <summary>
    /// 酒馆产出
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {


            double count = facilityPanelManager.GetCount();//每秒产出软妹币 = 酒馆数量
            count *= 20;//每个酒馆产出20基础

            count *= ResourceAdditionManager.Instance.GetTavernrmbUp();//酒馆软妹币产量加成   
            count *= ResourceAdditionManager.Instance.GetRMBboostUp();//软妹币产量加成   

            //每帧增加软妹币
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Currency, count * Time.deltaTime);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Currency, secondCount,true);
        }


    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1

    }


}
