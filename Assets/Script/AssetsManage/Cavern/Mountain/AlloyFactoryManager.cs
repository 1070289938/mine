using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 合金工厂
public class AlloyFactoryManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "合金工厂";
    string resourceDescription = "合金工厂会不断的消耗铝矿和钛来合成十分坚硬的合金";
    int resourceQuantity = 0;

    string btnText = "建造";


    double baseYield = 0.01;//合金基础产量

    Dictionary<ResourceType, double> depleted;//每个合金消耗的资源
    readonly FacilityType type = FacilityType.AlloyFactory;


    Dictionary<ResourceType, double> resources = new() //建造需要的资源
    {
        //基础消耗=50k软妹币，50钛
        [ResourceType.Currency] = AssetsUtil.ParseNumber("50k"),
        [ResourceType.Titanium] = 50
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
        InstallInput();


        facilityPanelManager.InstallButton();//初始化增加减少的按钮
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
            [ResourceType.Alloy] = 0
        }; //初始化产出资源
        facilityPanelManager.OutPutInstall(outPutResources);
    }
    /// <summary>
    /// 消耗的初始化
    /// </summary>
    void InstallInput()
    {
        //获取到合金的制作配方
        depleted = ResourceManager.Instance.formula[ResourceType.Alloy];
        Dictionary<ResourceType, double> inPutResources = new Dictionary<ResourceType, double>
        {
            [ResourceType.Aluminum] = 0,
            [ResourceType.Titanium] = 0
        }; //初始化消耗资源
        facilityPanelManager.InputInstall(inPutResources);
    }




    // Update is called once per frame
    void Update()
    {
        Output();
    }
    /// <summary>
    /// 合金工厂产出合金
    /// </summary>
    void Output()
    {

        double rmb = baseYield * facilityPanelManager.GetCount();//每秒产出合金

        rmb *= ResourceAdditionManager.Instance.GetWorkerUp();//加上员工加成的提升

        rmb *= ResourceAdditionManager.Instance.GetAlloyFactoryUp();//加上合金工厂专项加成的提升

        rmb *= ResourceAdditionManager.Instance.GetFabricatorUp();//加上制造工人的提升

        rmb *= ResourceAdditionManager.Instance.GetFactoryUp();//加上工厂的提升

        double thisYield = rmb * Time.deltaTime;//计算出当前帧产出的合金


        //计算出当前帧需要消耗的资源,并且判断是否足够
        foreach (var expend in depleted)
        {
            double expendable = thisYield * expend.Value;
            expendable = ResourceManager.Instance.RebirthBonus(expend.Key, expendable);

            double thisResource = ResourceManager.Instance.GetResource(expend.Key);
           
            if (thisResource < expendable)
            {
                //如果资源不足就不进行生产
                return;
            }
        }

        //每帧增加合金
        IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Alloy, thisYield, true);



        //进行资源的消耗
        foreach (var expend in depleted)
        {
            double actualDeduction = increment.ActualCount * expend.Value;//计算实际上需要扣除的资源
            ResourceManager.Instance.SpendResource(expend.Key, actualDeduction);//根据实际的产出来消耗资源
        }


        //计算出每秒产出多少资源
        double secondCount = increment.Count / Time.deltaTime;
        facilityPanelManager.UpdateOutPut(ResourceType.Alloy, secondCount, false);//更新产出资源

        //更新消耗资源
        foreach (var expend in depleted)
        {
            facilityPanelManager.UpdateInPut(expend.Key, secondCount * expend.Value);
        }


    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
    }


}
