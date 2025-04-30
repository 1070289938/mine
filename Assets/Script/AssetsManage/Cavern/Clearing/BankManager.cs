using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 银行
public class BankManager : MonoBehaviour
{

    Button miningButton; // 按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "银行";
    string resourceDescription = "银行可以比房屋更加方便的储存软妹币,可以大幅提升软妹币储量上限";
    int resourceQuantity = 0;

    string btnText = "建造";

    double baseReserves = AssetsUtil.ParseNumber("20k");//基础储量


    readonly FacilityType type = FacilityType.Bank;

    //建造需要的资源 
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //基础消耗=1k软妹币,80水泥,10钢
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1k"),
        [ResourceType.Cement] = 80,
        [ResourceType.Steel] = 10,
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


    }

    /// <summary>
    /// 提升软妹币的产量
    /// </summary>
    /// <returns></returns>
    public double GetUp()
    {
        if (!TechManager.Instance.GetTechFlag(TechType.Invest))
        {
            return 0;//0=不提升
        }
        //每个银行提升1%
        if (facilityPanelManager == null)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        double up = 0.01;

        //对冲基金提升至2%
        if (TechManager.Instance.GetTechFlag(TechType.HedgeFund))
        {
            up = 0.02;
        }


        return up * facilityPanelManager.GetCount();


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


    /// <summary>
    /// 获取银行的储量
    /// </summary>
    /// <returns></returns>
    public double GetReserves()
    {
        double reserves = baseReserves;

        reserves *= ResourceAdditionManager.Instance.GetBankReserveSurchargeUp();

        if (!facilityPanelManager)
        {
            facilityPanelManager = GetComponent<FacilityPanelManager>();
        }
        reserves *= facilityPanelManager.GetCount();//乘以数量




        return reserves;
    }


    // Update is called once per frame
    void Update()
    {
        if (interestFlag)
        {

            Output();//利息自动产出
        }

    }
    //利息研究完成事件
    public void OnInterest()
    {
        interestFlag = true;
        InstallOutPut();
    }

    bool interestFlag = false;
    /// <summary>
    /// 银行产出利息
    /// </summary>
    void Output()
    {
        if (facilityPanelManager.GetCount() != 0)
        {
            int tCount = ResourceCountManager.Instance.GetTenementCount();

            double count = tCount * facilityPanelManager.GetCount();//每秒产出软妹币 = 房屋数量X银行数量




            count *= ResourceAdditionManager.Instance.GetRMBboostUp();//软妹币产量加成   

            //每帧增加软妹币
            IncrementReturn increment = ResourceManager.Instance.AddResource(ResourceType.Currency, count * Time.deltaTime,true);

            //计算出每秒产出多少资源
            double secondCount = increment.Count / Time.deltaTime;
            facilityPanelManager.UpdateOutPut(ResourceType.Currency, secondCount,true);
        }


    }



    // 点击按钮时触发
    private void OnMineButtonClicked()
    {

        facilityPanelManager.AddQuantityUI();//数量+1
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新所有储存上限

    }


}
