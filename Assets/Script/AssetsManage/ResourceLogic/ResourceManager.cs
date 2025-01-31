using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }


    private Dictionary<ResourceType, double> resources;  //各个资源的数量
    private Dictionary<ResourceType, double> resourcesMax;//各个资源的上限
    public Dictionary<ResourceType, bool> resourceUnlocks;//各个资源是否显示

    public Dictionary<ResourceType, ResourceShowManager> resourceManager = new Dictionary<ResourceType, ResourceShowManager>();//各个资源的管理节点
    public ResourceAdditionManager resourceAdditionManager;//资源加成管理


    public ResourceContentManager resourceContentManager;//资源内容管理
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeResources();
    }

    /// <summary>
    /// 初始化资源
    /// </summary>
    private void InitializeResources()
    {

        resources = new Dictionary<ResourceType, double>();
        resourcesMax = new Dictionary<ResourceType, double>();
        resourceUnlocks = new Dictionary<ResourceType, bool>();
        foreach (ResourceType type in System.Enum.GetValues(typeof(ResourceType)))
        {
            resources[type] = 0; // 所有资源初始为0
            resourcesMax[type] = 0; //所有资源上限初始为0
            resourceUnlocks[type] = false; // 所有资源初始不显示
        }



    }

    public void Start()
    {
        ResourceShowManager[] resourceShowManagers = resourceContentManager.GetComponentsInChildren<ResourceShowManager>(true);
        foreach (ResourceShowManager resourceShow in resourceShowManagers)
        {
            resourceManager[resourceShow.getResourceType()] = resourceShow;
        }
    }

    // 解锁资源
    public void UnlockResource(ResourceType resourceType)
    {

        if (!resourceUnlocks[resourceType])
        {
            resourceUnlocks[resourceType] = true;
            if (resourceType == ResourceType.Stone)
            {
                LogManager.Instance.AddLog("你发现了石矿!");
                //开启监听
                TechChecker.Instance.AddCheckTech(TechType.pickaxe);//石镐
                TechChecker.Instance.AddCheckTech(TechType.warehouse);//仓库
                TechChecker.Instance.AddCheckTech(TechType.house);//房屋

            }
            if (resourceType == ResourceType.Copper)
            {
                LogManager.Instance.AddLog("你发现了铜矿!");
                TechChecker.Instance.AddCheckTech(TechType.minerlamp);//矿灯
                TechChecker.Instance.AddCheckTech(TechType.BrassPick);//铜镐
                TechChecker.Instance.AddCheckTech(TechType.CopperWarehouse);//铜制仓库


            }
            if (resourceType == ResourceType.Iron)
            {
                LogManager.Instance.AddLog("你发现了铁矿!");
                TechChecker.Instance.AddCheckTech(TechType.minerlamp);//矿灯
                TechChecker.Instance.AddCheckTech(TechType.rail);//铁轨
                TechChecker.Instance.AddCheckTech(TechType.ironPickaxe);//铁镐
                TechChecker.Instance.AddCheckTech(TechType.IronWarehouse);//铁仓库

            }
            if (resourceType == ResourceType.Currency)
            {
                LogManager.Instance.AddLog("你赚到了人生中第一桶金!");



                TechChecker.Instance.AddCheckTech(TechType.StoneMiner);//石矿工人
                TechChecker.Instance.AddCheckTech(TechType.CopperMiner);//铜矿工人
                TechChecker.Instance.AddCheckTech(TechType.IronMiner);//铁矿工人
                TechChecker.Instance.AddCheckTech(TechType.CementManufacture);//水泥工人
            }
        }
    }

    // 检查资源是否解锁
    public bool IsResourceUnlocked(ResourceType resourceType)
    {
        return resourceUnlocks[resourceType];
    }

    /// <summary>
    /// 增加资源
    /// </summary>
    public IncrementReturn AddResource(ResourceType type, double amount)
    {


        IncrementReturn increment = new IncrementReturn
        {
            Count = 0,
            ActualCount = 0
        };
        if (!resources.ContainsKey(type)) return increment;
        if (amount == 0) return increment;
        UnlockResource(type);


        double count = amount;
        double resourcesSum = resources[type] + count;

        increment.Count = count;
        //如果超过上限就以上限来计算
        if (resourcesSum > resourcesMax[type])
        {
            increment.ActualCount = resourcesMax[type] - resources[type];
            resources[type] = resourcesMax[type];

        }
        else
        {
            resources[type] = resourcesSum;
            increment.ActualCount = count;
        }
        return increment;//返回这次增加了多少资源
    }

    /// <summary>
    /// 消耗资源
    /// </summary>
    public bool SpendResource(ResourceType type, double amountStr)
    {
        if (!resources.ContainsKey(type)) return false;

        double amount = amountStr;

        if (resources[type] >= amount)
        {
            resources[type] -= amount;

            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 获取当前资源数量
    /// </summary>
    public double GetResource(ResourceType type)
    {
        if (!resources.ContainsKey(type)) return 0;
        return resources[type];
    }

    /// <summary>
    /// 设置资源上限
    /// </summary>
    /// <param name="type"></param>
    /// <param name="max"></param>
    public void SetResourceMax(ResourceType type, double max)
    {
        resourcesMax[type] = max;
    }



    //判断资源是否足够
    public JudgmentResult JudgmentResource(Dictionary<ResourceType, double> resources)
    {
        foreach (var res in resources)
        {
            //判断资源是否足够
            double count = GetResource(res.Key);
            double value = res.Value;
            if (count < value)
            {
                return new JudgmentResult(false, res.Key);
            }
        }
        //如果资源充足就直接扣除资源
        foreach (var res in resources)
        {
            SpendResource(res.Key, res.Value);
        }
        return new JudgmentResult(true);
    }



}


public class JudgmentResult
{
    public bool flag { get; set; }
    public ResourceType type { get; set; }

    // 主构造方法
    public JudgmentResult(bool flag, ResourceType type)
    {
        this.flag = flag;
        this.type = type;
    }
    public JudgmentResult(bool flag)
    {
        this.flag = flag;
    }



}