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


        tenementManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.Tenement);


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
        return count;
    }

}
