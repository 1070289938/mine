using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 太空矿船
public class SpaceMiningShipManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;


    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "太空矿船";
    string resourceDescription = "在太空采集的矿船,可以采集陨石中的铁矿(提供1人口)";
    int resourceQuantity = 0;

    string btnText = "建造";

    readonly FacilityType type = FacilityType.SpaceMiningShip;
    double baseYield = 20;//铁矿基础产量


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

        //基础消耗=500合金，58纳米材料
        resources[ResourceType.Alloy] = 500;
        resources[ResourceType.Nanomaterials] = 58;
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
            [ResourceType.Iron] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }




    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 铁矿工人产出铁矿
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {
            double count = baseYield * facilityPanelManager.GetCount();//每秒产出铁矿


            count *= ResourceAdditionManager.Instance.GetMiningWorkerUp();  //加上采矿工人的提升


            count *= ResourceAdditionManager.Instance.GetToolUp();//加上工具对铁矿工人的提升

            count *= ResourceAdditionManager.Instance.GetWorkerUp();//加上矿洞员工加成的提升


            count *= ResourceAdditionManager.Instance.GetIronMineUp();//加上铁矿专项加成

            //每帧增加软妹币
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Iron, count * Time.deltaTime);



            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Iron, secondCount,true);
        }



    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
