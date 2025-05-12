using System;
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

  /// <summary>
  /// 寺庙
  /// </summary>
  Temple,



  ///////////////太空////////////////


  /// <summary>
  /// 太空矿船
  /// </summary>
  SpaceMiningShip,


  /// <summary>
  /// 人造卫星
  /// </summary>
  ArtificialSatellite,


  /// <summary>
  /// 铱矿采集器
  /// </summary>
  IridiumCollector,


  /// <summary>
  /// 月球物资站
  /// </summary>
  LunarMaterialStation,

  /// <summary>
  /// 秘银锻造厂
  /// </summary>
  MithrilForge,

  /// <summary>
  /// 月球资料站
  /// </summary>
  LunarDataStation,

  /// <summary>
  /// 火星殖民地
  /// </summary>
  MarsColony,

  /// <summary>
  /// 火星旅游站
  /// </summary>
  MarsTouristStation,

  /// <summary>
  /// 火星空间站
  /// </summary>
  MarsSpaceStation,


  /// <summary>
  /// 火星研究站
  /// </summary>
  MarsResearchStation,


  /// <summary>
  /// 中子采集器
  /// </summary>
  NeutronCollector,


  /// <summary>
  /// 太空船坞
  /// </summary>
  SpaceDock,

  /// <summary>
  /// 实验型探索者
  /// </summary>
  ExperimentalExplorer,

  /// <summary>
  /// 太空电梯
  /// </summary>
  SpaceElevator,

  /// <summary>
  /// 奥德修斯号
  /// </summary>
  Odysseus,

  /// <summary>
  /// 金属氢采集器
  /// </summary>
  MetallicHydrogenCollector,

  /// <summary>
  /// 深空信标
  /// </summary>
  DeepSpaceBeacon,

  /// <summary>
  /// 子空间信标
  /// </summary>
  SubspaceStarMarker,

  /// <summary>
  /// 耀斑矿采集器
  /// </summary>
  FlareMineralCollector,

  /// <summary>
  /// 外星材料实验室
  /// </summary>
  ExtraterrestrialMaterialLaboratory,
  /// <summary>
  /// 精金采集器
  /// </summary>
  AdamantiteCollector,

  /// <summary>
  /// 分布式仓储网络
  /// </summary>
  DistributedWarehousingNetwork,

  /// <summary>
  /// 星际交易枢纽
  /// </summary>
  InterstellarTradeHub,


  /// <summary>
  /// 星际研究所
  /// </summary>
  InterstellarResearchInstitute,

  /// <summary>
  /// 环世界
  /// </summary>
  RingWorld,
  /// <summary>
  /// 星际之门
  /// </summary>
  Stargate,

  /// <summary>
  /// 搭建星渊号
  /// </summary>
  ConstructingXingyuanShip,


}


public static class FacilityTypeHelper
{
  // 将字符串转换为 TechType 枚举值
  public static FacilityType StringToFacilityType(string value)
  {
    if (Enum.TryParse(value, out FacilityType techType))
    {
      return techType;
    }
    // 如果解析失败，返回默认值 empty
    return FacilityType.none;
  }

  // 将 TechType 枚举值转换为字符串
  public static string FacilityTypeToString(FacilityType techType)
  {
    return techType.ToString();
  }
}