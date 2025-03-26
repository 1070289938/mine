using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 探测队
public class ExplorationTeamManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;//节点管理


    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "探测队";
    string resourceDescription = "派遣探测队来看看这里面是怎么个事?";
    int resourceQuantity = 0;

    string btnText = "招募";

    double baseYield = 0.2;//地心岩基础产量
    readonly FacilityType type = FacilityType.ExplorationTeam;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //软妹币 20K 银 500
        [ResourceType.Currency] = AssetsUtil.ParseNumber("20k"),
        [ResourceType.silver] = 500,

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



    }


    /// <summary>
    /// 产出的初始化
    /// </summary>
    void InstallOutPut()
    {

        Dictionary<ResourceType, double> outPutResources = new Dictionary<ResourceType, double>
        {
            [ResourceType.GeocentricRock] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }

    // Update is called once per frame
    void Update()
    {
        if (discoverFlag)
        {

            Output();//自动产出
        }
        else
        {
            if (TechManager.Instance.GetTechFlag(TechType.Geocentric))
            {
                OnInterest();
            }
        }

    }
    //发现地心岩事件
    public void OnInterest()
    {
        discoverFlag = true;
        InstallOutPut();
    }

    bool discoverFlag = false;




    /// <summary>
    /// 产出
    /// </summary>
    void Output()
    {
        int MinerCount = facilityPanelManager.GetCount();//获取工人数量
        if (MinerCount != 0)
        {
            double output = baseYield * MinerCount;//每秒产出地心岩


            output *= ResourceAdditionManager.Instance.GetMiningWorkerUp(); //加上采矿工人的提升

            output *= ResourceAdditionManager.Instance.GetToolUp();//加上工具对银矿工人的提升

            output *= ResourceAdditionManager.Instance.GetWorkerUp();//加上矿洞员工加成的提升

            //每帧增加银矿
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.GeocentricRock, output * Time.deltaTime);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.GeocentricRock, secondCount, true);

        }

    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
