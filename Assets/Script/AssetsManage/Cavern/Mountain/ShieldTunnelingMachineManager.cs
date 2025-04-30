using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 盾构机
public class ShieldTunnelingMachineManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "盾构机";
    string resourceDescription = "盾构机可以产出十分大量的基础资源,每个石矿工人,铜矿工人,铁矿工人都会提升盾构机对应资源的产量";
    int resourceQuantity = 0;

    string btnText = "建造一部分";

    Dictionary<ResourceType, double> baseYield = new Dictionary<ResourceType, double>()
    {
        //每个资源对应的基础产量
        [ResourceType.Stone] = 50,
        [ResourceType.Copper] = 20,
        [ResourceType.Iron] = 10,
    };

    readonly FacilityType type = FacilityType.ShieldTunnelingMachine;

    //建造需要的资源 
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=100k软妹币,500合金
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1000k"),
        [ResourceType.Alloy] = 500,
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
        facilityPanelManager.InstallSchedule();
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
            [ResourceType.Stone] = 0,
            [ResourceType.Copper] = 0,
            [ResourceType.Iron] = 0,
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);


    }


    // Update is called once per frame
    void Update()
    {
        Output();

    }



    /// <summary>
    /// 盾构机产出
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {
            foreach (var res in baseYield)
            {
                ResourceType type = res.Key;
                double count = 0;//工人数量
                switch (type)
                {
                    case ResourceType.Stone:
                        count = ResourceCountManager.Instance.GetStoneMinerCount();
                        break;
                    case ResourceType.Copper:
                        count = ResourceCountManager.Instance.GetCopperMinerCount();
                        break;
                    case ResourceType.Iron:
                        count = ResourceCountManager.Instance.GetIronMinerCount();
                        break;
                }



                double yield = count * res.Value * facilityPanelManager.GetCount();//每秒产出资源量




                //每帧增加资源
                IncrementReturn increment = ResourceManager.Instance.AddResource(type, yield * Time.deltaTime,true);

                //计算出每秒产出多少资源
                double secondCount = increment.Count / Time.deltaTime;
                facilityPanelManager.UpdateOutPut(type, secondCount,true);
            }


        }


    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        facilityPanelManager.AddSchedule(5);//进度+5%

    }


}
