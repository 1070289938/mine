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


    ///////////////矿山////////////////

    /// <summary>
    /// 石料加工厂
    /// </summary>
    StoneMill,

    /// <summary>
    /// 铜矿加工厂
    /// </summary>
    CopperMill,

    /// <summary>
    /// 水泥加工厂
    /// </summary>
    CementMill,

    /// <summary>
    /// 酒馆
    /// </summary>
    Tavern,
    /// <summary>
    /// 铝矿采集器
    /// </summary>
    AluminiumHarvester,

    /// <summary>
    /// 金属精炼厂
    /// </summary>
    MetalRefinery,


    /// <summary>
    /// 钛矿采集器
    /// </summary>
    TitaniumCollector,



    /// <summary>
    /// 合金工厂
    /// </summary>
    AlloyFactory,
    /// <summary>
    /// 盾构机
    /// </summary>
    ShieldTunnelingMachine,

    /// <summary>
    /// 实验室
    /// </summary>
    Laboratory,

    /// <summary>
    /// 科技探索塔
    /// </summary>
    DiscoveryTower,

    /// <summary>
    /// 佐里旬矿探测器
    /// </summary>
    ZoriMineDetector,

    /// <summary>
    /// 压缩增幅器
    /// </summary>
    MaterialCompressor,

    /// <summary>
    /// 证券交易所
    /// </summary>
    StockExchange,
    /// <summary>
    /// 深井电梯
    /// </summary>
    DeepShaftElevator,





    ///////////////地核////////////////
    /// <summary>
    /// 银矿工人
    /// </summary>
    SilverMiner,


    /// <summary>
    /// 镍矿采集器
    /// </summary>
    NickelHarvester,

    /// <summary>
    /// 钨矿采集器
    /// </summary>
    TungstenHarvester,

    /// <summary>
    /// 人造矿井
    /// </summary>
    ArtificialMine,

    /// <summary>
    /// 纳米工厂
    /// </summary>
    Nanofactory,

    /// <summary>
    /// 地心研究所
    /// </summary>
    GeocentricStudy,

    /// <summary>
    /// 祭坛
    /// </summary>
    Altar,
    /// <summary>
    /// 巨型纪念碑
    /// </summary>
    Monument,
    /// <summary>
    /// 兵营
    /// </summary>
    Barracks,


    /// <summary>
    /// 探测队
    /// </summary>
    ExplorationTeam,

    /// <summary>
    /// 自动炮塔
    /// </summary>
    AutomaticTurret,

    /// <summary>
    /// 激光炮塔
    /// </summary>
    LaserTurret,
    /// <summary>
    /// 新兵训练营
    /// </summary>
    BootCamp,


}

