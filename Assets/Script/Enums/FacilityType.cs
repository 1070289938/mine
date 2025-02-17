using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public enum FacilityType
{


    none, //没有东西
    /// <summary>
    /// 一堆矿石
    /// </summary>
    Ore,
    /// <summary>
    /// 仓库
    /// </summary>
    Stash,
    /// <summary>
    /// 房屋
    /// </summary>
    Tenement,
    /// <summary>
    /// 石矿工人
    /// </summary>
    StoneMiner,
    /// <summary>
    /// 铜矿工人
    /// </summary>
    CopperMiner,
    /// <summary>
    /// 铁矿工人
    /// </summary>
    IronMiner,
    /// <summary>
    /// 水泥搅拌工
    /// </summary>
    CementWorker,
    /// <summary>
    /// 铁轨
    /// </summary>
    Rail,
    /// <summary>
    /// 矿车
    /// </summary>
    OreCar,

    ///////////////矿场////////////////

    /// <summary>
    /// 矿物筛选机
    /// </summary>
    MineralScreeningMachine,

    /// <summary>
    /// 煤矿工人
    /// </summary>
    CoalWorker,
    /// <summary>
    /// 钢铁铸造工
    /// </summary>
    IronSteelFoundry,
    /// <summary>
    /// 高炉
    /// </summary>
    BlastFurnace,

    /// <summary>
    /// 银行
    /// </summary>
    Bank,
    /// <summary>
    /// 集装箱
    /// </summary>
    Container,
    /// <summary>
    /// 工业储备站
    /// </summary>
    IndustrialReserveStation,
    /// <summary>
    /// 挖掘机
    /// </summary>
    Excavator,
    /// <summary>
    /// 硅石采矿机
    /// </summary>
    SilicaMiningMachine,

    /// <summary>
    /// 硅矿精炼器
    /// </summary>
    SiliconRefiner,
}

