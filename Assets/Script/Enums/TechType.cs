using System;
using System.ComponentModel;
using UnityEngine;
using UnityEditor;
using System.Reflection;


public enum TechType
{
    //=============================================矿洞====================================================
    /// <summary>
    /// 别管他,没有用
    /// </summary>
    empty, //无研究
    pickaxe, //石镐
    warehouse,  // 仓库
    house, // 房屋
    minerlamp,//矿灯
    BrassPick,//铜镐
    CopperWarehouse,//铜制仓库
    collectRents,//收租
    SimpleDecoration,//简易的家具
    SimpleFinish,//简陋的装修
    StoneMiner,//石矿工人
    CopperMiner,//铜矿工人
    SaveMoney,//储钱罐
    MoneyBox,//储钱罐
    IronMiner,//铁矿工人
    CementManufacture,//水泥生产
    rail,//铁轨
    tramcar,//矿车
    ironPickaxe,//铁镐
    tupid,//铁质大锤
    Chisel,//凿子
    StoneHammer,//石质大锤
    CopperHammer,//铜质大锤
    IronWarehouse,//铁质仓库
    ConcreteBuilding,//水泥房屋
    shore,//支撑柱
    StrengthenWarehouse,//加强仓库
    painted,//画饼

    FindLight,//发现光芒

    BrokenStone,//破开石头

    //==============================================矿场===================================================

    /// <summary>
    /// 大型机器
    /// </summary>
    LargeMachine,//
    /// <summary>
    /// 矿物筛选器
    /// </summary>
    MineralFilter,//
    /// <summary>
    /// 煤炭挖掘
    /// </summary>
    DigCoal,//
    /// <summary>
    /// 炼制钢铁
    /// </summary>
    RefinedIronSteel,//
    /// <summary>
    /// 矿石传送带
    /// </summary>
    ConveyorBelt,//
    /// <summary>
    /// 火车
    /// </summary>
    Train,//
    /// <summary>
    /// 钢筋混凝土
    /// </summary>
    ReinforcedConcrete,//
    /// <summary>
    /// 房屋保险箱
    /// </summary>
    HouseSafe,//
    /// <summary>
    /// 集装箱
    /// </summary>
    Container,//
    /// <summary>
    /// 钢镐
    /// </summary>
    SteelPick,//
    /// <summary>
    /// 钢梁
    /// </summary>
    SteelBeam,//
    /// <summary>
    /// 工业储备站
    /// </summary>
    IndustrialReserveStation,//
    /// <summary>
    /// 高炉
    /// </summary>
    BlastFurnace,//
    /// <summary>
    /// 回转炉
    /// </summary>
    RotaryFurnace,//
    /// <summary>
    /// 高级炼钢术
    /// </summary>
    AdvancedSteelmaking,//
    /// <summary>
    /// 起重机
    /// </summary>
    Crane,//
    /// <summary>
    /// 螺旋输送机
    /// </summary>
    ScrewConveyor,//
    /// <summary>
    /// 银行
    /// </summary>
    Bank,//
    /// <summary>
    /// 五险一金
    /// </summary>
    SocialInsurance,//
    /// <summary>
    /// 投资
    /// </summary>
    Invest,//
    /// <summary>
    /// 利息
    /// </summary>
    Interest,//
    /// <summary>
    /// 钻头
    /// </summary>
    DrillBit,//
    /// <summary>
    /// 挖掘机
    /// </summary>
    Excavator,//
    /// <summary>
    /// 挖掘硅矿
    /// </summary>
    SiliconMining,//
    /// <summary>
    /// 硅石精炼器
    /// </summary>
    SilicaRefinery,//
    /// <summary>
    /// 硅酸盐水泥
    /// </summary>
    PortlandCement,//
    /// <summary>
    /// 手持电钻
    /// </summary>
    ElectricDrill,//
    /// <summary>
    /// 互联网
    /// </summary>
    Internet,//
    /// <summary>
    /// 安全摄像头
    /// </summary>
    SecurityCamera,//
    /// <summary>
    /// 电视
    /// </summary>
    Television,//
    /// <summary>
    /// 职业培训
    /// </summary>
    VocationalTraining,//
    /// <summary>
    /// 钢制大锤
    /// </summary>
    SteelSledgehammer,//
    /// <summary>
    /// 定点采矿机
    /// </summary>
    SpotMiner,//
    /// <summary>
    /// 金属探测器
    /// </summary>
    MetalDetector,
    /// <summary>
    /// 发现奇妙的金属棒
    /// </summary>
    MetalBarFound,
    /// <summary>
    /// 检查奇妙的金属棒
    /// </summary>
    InspectWonderfulRod,//
    /// <summary>
    /// 探索矿山
    /// </summary>
    ExploratoryMine,//




}
public static class TechTypeHelper
{
    // 将字符串转换为 TechType 枚举值
    public static TechType StringToTechType(string value)
    {
        if (Enum.TryParse(value, out TechType techType))
        {
            return techType;
        }
        // 如果解析失败，返回默认值 empty
        return TechType.empty;
    }

    // 将 TechType 枚举值转换为字符串
    public static string TechTypeToString(TechType techType)
    {
        return techType.ToString();
    }
}
