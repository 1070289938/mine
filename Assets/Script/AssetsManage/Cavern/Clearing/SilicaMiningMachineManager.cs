using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 硅石采矿器
public class SilicaMiningMachineManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;


    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "硅石采矿器";
    string resourceDescription = "坚硬的硅石需要专业的硅石挖掘机器(由2个人进行操作)";
    int resourceQuantity = 0;

    string btnText = "招募";

    readonly FacilityType type = FacilityType.SilicaMiningMachine;
    double baseYield = 0.05;//硅石基础产量

    //建造需要的资源
    Dictionary<ResourceType, double> resources = new()
    {
        //基础消耗=200软妹币，80铜,50钢
        [ResourceType.Currency] = 500,
        [ResourceType.Copper] = 80,
        [ResourceType.Steel] = 50,
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

    SiliconRefinerManager siliconRefinerManager;//硅石精炼器管理
    void Start()
    {
        siliconRefinerManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.SiliconRefiner).GetComponent<SiliconRefinerManager>();//初始化挖掘机管理
    }



    /// <summary>
    /// 产出的初始化
    /// </summary>
    void InstallOutPut()
    {

        Dictionary<ResourceType, double> outPutResources = new Dictionary<ResourceType, double>
        {
            [ResourceType.Silicon] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }




    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 硅石采矿机产出硅石
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {
            double count = baseYield * facilityPanelManager.GetCount();//每秒产出硅石

            count *= siliconRefinerManager.GetUp();//硅石精炼器的加成

            count *= ResourceAdditionManager.Instance.GetToolUp();//采矿工具加成

            count *= ResourceAdditionManager.Instance.GetMiningWorkerUp();//采矿工人加成
            //每帧增加硅石
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Silicon, count * Time.deltaTime);



            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Silicon, secondCount);
        }



    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
