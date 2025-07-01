using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 计数管理
/// </summary>
public class ResourceCountManager : MonoBehaviour
{
    public static ResourceCountManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    FacilityPanelManager stoneMinerManager;//石矿工人
    FacilityPanelManager copperMinerManager;//铜矿工人
    FacilityPanelManager ironMinerManager;//铁矿工人
    FacilityPanelManager cementWorkerManager;//水泥工人

    FacilityPanelManager coalWorkerManager;//煤矿工人
    FacilityPanelManager ironSteelFoundryManager;//钢铁铸造工
    FacilityPanelManager silicaMiningMachineManager;//硅石采矿机

    FacilityPanelManager aluminiumHarvester;//铝矿采集器

    FacilityPanelManager titaniumCollector;//钛矿采集器


    FacilityPanelManager silverMiner;//银矿工人
    FacilityPanelManager mickelHarvester;//镍矿采集器
    FacilityPanelManager tungstenHarvester;//钨矿采集器

    FacilityPanelManager spaceMiningShip;//太空铁矿船

    FacilityPanelManager iridiumCollector;//铱矿采集器

    FacilityPanelManager neutronCollector;//中子采集器

    FacilityPanelManager MetallicHydrogenCollector;//金属氢采集器
    FacilityPanelManager FlareMineralCollector;//耀斑矿采集器
    FacilityPanelManager AdamantiteCollector;//精金采集器
    FacilityPanelManager tenementManager;//房屋
    void Start()
    {
        //初始化影响到的工人
        stoneMinerManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.StoneMiner);
        copperMinerManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.CopperMiner);
        ironMinerManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.IronMiner);
        cementWorkerManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.CementWorker);

        coalWorkerManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.CoalWorker);
        ironSteelFoundryManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.IronSteelFoundry);
        silicaMiningMachineManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.SilicaMiningMachine);


        aluminiumHarvester = FacilityManager.Instance.GetFacilityPanel(FacilityType.AluminiumHarvester);
        titaniumCollector = FacilityManager.Instance.GetFacilityPanel(FacilityType.TitaniumCollector);

        silverMiner = FacilityManager.Instance.GetFacilityPanel(FacilityType.SilverMiner);
        mickelHarvester = FacilityManager.Instance.GetFacilityPanel(FacilityType.NickelHarvester);
        tungstenHarvester = FacilityManager.Instance.GetFacilityPanel(FacilityType.TungstenHarvester);


        tenementManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.Tenement);

        spaceMiningShip = FacilityManager.Instance.GetFacilityPanel(FacilityType.SpaceMiningShip);
        iridiumCollector = FacilityManager.Instance.GetFacilityPanel(FacilityType.IridiumCollector);
        neutronCollector = FacilityManager.Instance.GetFacilityPanel(FacilityType.NeutronCollector);

        MetallicHydrogenCollector = FacilityManager.Instance.GetFacilityPanel(FacilityType.MetallicHydrogenCollector);
        FlareMineralCollector = FacilityManager.Instance.GetFacilityPanel(FacilityType.FlareMineralCollector);
        AdamantiteCollector = FacilityManager.Instance.GetFacilityPanel(FacilityType.AdamantiteCollector);
    }

    /// <summary>
    /// 获取房屋数量
    /// </summary>
    /// <returns></returns>
    public int GetTenementCount()
    {
        return tenementManager.GetCount();
    }

    /// <summary>
    /// 获取石矿工人数量
    /// </summary>
    /// <returns></returns>
    public int GetStoneMinerCount()
    {
        return stoneMinerManager.GetCount();
    }
    /// <summary>
    /// 获取铜矿工人数量
    /// </summary>
    /// <returns></returns>
    public int GetCopperMinerCount()
    {
        return copperMinerManager.GetCount();
    }
    /// <summary>
    /// 获取铁矿工人数量
    /// </summary>
    /// <returns></returns>
    public int GetIronMinerCount()
    {
        return ironMinerManager.GetCount();
    }



    /// <summary>
    /// 统计一下工人总数
    /// </summary>
    /// <returns></returns>
    public int GetMinerCount()
    {
        int count = 0;
        count += stoneMinerManager.GetMaxCount();
        count += copperMinerManager.GetMaxCount();
        count += ironMinerManager.GetMaxCount();
        count += cementWorkerManager.GetMaxCount();

        count += coalWorkerManager.GetMaxCount();
        count += ironSteelFoundryManager.GetMaxCount();
        count += silicaMiningMachineManager.GetMaxCount() * 2;
        count += aluminiumHarvester.GetMaxCount() * 2;
        count += titaniumCollector.GetMaxCount() * 2;

        count += silverMiner.GetMaxCount();
        count += mickelHarvester.GetMaxCount()* 2;
        count += tungstenHarvester.GetMaxCount()* 2;

        count += spaceMiningShip.GetMaxCount();
        count += iridiumCollector.GetMaxCount() * 2;
        count += neutronCollector.GetMaxCount() * 2;

        count += MetallicHydrogenCollector.GetMaxCount();
        count += FlareMineralCollector.GetMaxCount() * 2;
        count += AdamantiteCollector.GetMaxCount() * 2;




        return count;
    }

}
