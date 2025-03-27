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
    SteelPick,

    /// <summary>
    /// 钢制仓库
    /// </summary>
    SteelWarehouse,
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
    /// 金库
    /// </summary>
    Treasury,
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
    /// 发现山脉
    /// </summary>
    DiscoveredMine,//

    /// <summary>
    /// 探索山脉
    /// </summary>
    ExploratoryMine,//

    //==============================================矿山===================================================
    /// <summary>
    /// 工厂建设
    /// </summary>
    PlantConstruction,

    /// <summary>
    /// 实验室
    /// </summary>
    Laboratory,

    /// <summary>
    /// 研究金属棒
    /// </summary>
    ResearchRod,

    /// <summary>
    /// 佐里旬矿探测器
    /// </summary>
    ZoriMineDetector,

    /// <summary>
    /// 物质压缩
    /// </summary>
    MaterialCompression,
    /// <summary>
    /// 压缩增幅器
    /// </summary>
    Compressor,
    /// <summary>
    /// 科技探索塔
    /// </summary>
    DiscoveryTower,
    /// <summary>
    /// 科学记录室
    /// </summary>
    RecordScience,
    /// <summary>
    /// 储备规划
    /// </summary>
    ReservePlanning,

    /// <summary>
    /// 克洛尔法
    /// </summary>
    KrolProcess,


    /// <summary>
    /// 电弧炉
    /// </summary>
    ElectricFurnace,

    /// <summary>
    /// 证券交易所
    /// </summary>
    StockExchange,
    /// <summary>
    /// 灯红酒绿
    /// </summary>
    DazzleColour,

    /// <summary>
    /// 流水线
    /// </summary>
    AssemblyLine,





    /// <summary>
    /// 石料加工厂
    /// </summary>
    StoneFactory,

    /// <summary>
    /// 铜矿加工厂
    /// </summary>
    CopperWorks,

    /// <summary>
    /// 水泥加工厂
    /// </summary>
    CementFactory,

    /// <summary>
    /// 酒馆
    /// </summary>
    Tavern,

    /// <summary>
    /// 奢侈品
    /// </summary>
    Luxury,


    /// <summary>
    /// 对冲基金
    /// </summary>
    HedgeFund,

    /// <summary>
    /// 物流卡车
    /// </summary>
    LogisticsTruck,

    /// <summary>
    /// 铝矿采集
    /// </summary>
    AluminiumMining,

    /// <summary>
    /// 金属精炼厂
    /// </summary>
    MetalRefinery,

    /// <summary>
    /// 钛矿采集器
    /// </summary>
    TitaniumCollector,

    /// <summary>
    /// 钛制大锤
    /// </summary>
    TitaniumHammer,

    /// <summary>
    /// 钛制钻头
    /// </summary>
    TitaniumDrill,

    /// <summary>
    /// 钛板条
    /// </summary>
    TitaniumStrip,

    /// <summary>
    /// 钛金库
    /// </summary>
    TitaniumVault,
    /// <summary>
    /// 钛合金
    /// </summary>
    TitaniumAlloy,
    /// <summary>
    /// 钛制仓库
    /// </summary>
    TitaniumWarehouse,

    /// <summary>
    /// 合金工厂
    /// </summary>
    AlloyFactory,

    /// <summary>
    /// 自动化
    /// </summary>
    Automate,

    /// <summary>
    /// 机械外骨骼
    /// </summary>
    MechanicalExoskeleton,

    /// <summary>
    /// 合金钻头
    /// </summary>
    AlloyBit,

    /// <summary>
    /// 合金锤
    /// </summary>
    AlloyHammer,
    /// <summary>
    /// 合金仓库,
    /// </summary>
    AlloyWarehouse,
    /// <summary>
    /// 合金金库
    /// </summary>
    AlloyVault,

    /// <summary>
    /// 合金货架
    /// </summary>
    AlloyRack,

    /// <summary>
    /// 巨型建筑
    /// </summary>
    Megastructure,

    /// <summary>
    /// 盾构机
    /// </summary>
    ShieldTunnelingMachine,

    /// <summary>
    /// 人造矿井
    /// </summary>
    ArtificialMine,

    /// <summary>
    /// 地心计划
    /// </summary>
    GeocentricProject,

    /// <summary>
    /// 深井电梯
    /// </summary>
    DeepShaftElevator,

    /// <summary>
    /// 到达地心
    /// </summary>
    ReachCore,
    //==============================================地核===================================================

    /// <summary>
    /// 探测地核深处
    /// </summary>
    SoundingDepth,
    /// <summary>
    /// 防护服
    /// </summary>
    ProtectiveSuit,
    /// <summary>
    /// 熔岩防护服
    /// </summary>
    LavaProtectiveSuit,
    /// <summary>
    /// 银矿工人
    /// </summary>
    SilverMiner,
    /// <summary>
    /// 银制房梁
    /// </summary>
    SilverBeams,
    /// <summary>
    /// 银制仓库
    /// </summary>
    SilverWarehouse,

    /// <summary>
    /// 钨矿采集器
    /// </summary>
    TungstenHarvester,
    /// <summary>
    /// 实施人造矿井
    /// </summary>
    RealizeArtificialMine,
    /// <summary>
    /// 钨制集装箱
    /// </summary>
    TungstenContainer,
    /// <summary>
    /// 钨钻
    /// </summary>
    TungstenDrill,
    /// <summary>
    /// 钨制仓库
    /// </summary>
    TungstenWarehouse,
    /// <summary>
    /// 镍矿采集器
    /// </summary>
    NickelHarvester,
    /// <summary>
    /// 镍合金
    /// </summary>
    NickelAlloy,
    /// <summary>
    /// 高精密仪器
    /// </summary>
    HighPrecisionInstrument,
    /// <summary>
    /// 纳米材料
    /// </summary>
    Nanomaterials,
    /// <summary>
    /// 纳米工厂
    /// </summary>
    Nanofactory,
    /// <summary>
    /// 高精度机床
    /// </summary>
    HighPrecisionMachine,
    /// <summary>
    /// 高性能计算机
    /// </summary>
    HighPerformanceComputer,
    /// <summary>
    /// 地心研究所
    /// </summary>
    GeocentricResearch,
    /// <summary>
    /// 结构研究
    /// </summary>
    StructuralStudy,
    /// <summary>
    /// 白金银行卡
    /// </summary>
    PlatinumBankCard,
    /// <summary>
    /// 额外资金
    /// </summary>
    AdditionalFunds,
    /// <summary>
    /// 投注
    /// </summary>
    betting,
    /// <summary>
    /// 资源管理
    /// </summary>
    ResourceManagement,
    /// <summary>
    /// 古遗迹研究
    /// </summary>
    Ichnography,
    /// <summary>
    /// 古代战术
    /// </summary>
    AncientTactics,
    /// <summary>
    ///祭坛
    /// </summary>
    altar,
    /// <summary>
    /// 巨型纪念碑
    /// </summary>
    GiantMonument,
    /// <summary>
    /// 祭拜仪式
    /// </summary>
    WorshipCeremony,
    /// <summary>
    /// 发现痕迹
    /// </summary>
    FindTrace,
    /// <summary>
    /// 探索痕迹
    /// </summary>
    trace,
    /// <summary>
    /// 解析语言
    /// </summary>
    AnalyticLanguage,
    /// <summary>
    /// 找到大门
    /// </summary>
    DiscoverGate,
    /// <summary>
    /// 研究大门
    /// </summary>
    OpenGate,
    /// <summary>
    /// 军事化
    /// </summary>
    Militarize,
    /// <summary>
    /// 兵营
    /// </summary>
    Barracks,
    /// <summary>
    /// 白银甲胄
    /// </summary>
    SilverArmour,
    /// <summary>
    /// 钨制战甲
    /// </summary>
    TungstenArmor,
    /// <summary>
    /// 纳米修复器
    /// </summary>
    Nanoprosthesis,
    /// <summary>
    /// 冷兵器
    /// </summary>
    ColdWeapon,
    /// <summary>
    /// 热武器
    /// </summary>
    ThermalWeapon,
    /// <summary>
    /// 穿甲弹
    /// </summary>
    ArmourPiercingProjectile,
    /// <summary>
    /// 双层床
    /// </summary>
    BunkBed,
    /// <summary>
    /// 军营宿舍
    /// </summary>
    BarracksQuarters,
    /// <summary>
    /// 素质训练
    /// </summary>
    QualityTraining,
    /// <summary>
    /// 探测队
    /// </summary>
    ExplorationTeam,
    /// <summary>
    /// 搜寻到地心岩
    /// </summary>
    Geocentric,
    /// <summary>
    /// 防御阵地
    /// </summary>
    DefensivePosition,
    /// <summary>
    /// 自动炮塔
    /// </summary>
    AutomaticTurret,

    /// <summary>
    /// 激光炮塔
    /// </summary>
    LaserTurret,
    /// <summary>
    /// 战斗力大于100
    /// </summary>
    CombatPower100,
    /// <summary>
    /// 继续深入
    /// </summary>
    GoDeeper,
    /// <summary>
    /// 派遣侦察队
    /// </summary>
    DispatchReconnaissanceParty,
    /// <summary>
    /// 纳米仓库
    /// </summary>
    Nanowarehouse,

    /// <summary>
    /// 链式机枪
    /// </summary>
    ChainMachineGun,
    /// <summary>
    /// 新兵训练营
    /// </summary>
    BootCamp,
    /// <summary>
    /// 加急训练
    /// </summary>
    RushTraining,
    /// <summary>
    /// 七天速成班
    /// </summary>
    SevenDaysCrashCourse,
    /// <summary>
    /// 广泛招生
    /// </summary>
    WideEnrollment,
    /// <summary>
    /// 不道德协议
    /// </summary>
    UnethicalAgreement,
    //==============================================系统科技===================================================

    /// <summary>
    /// 空间储物
    /// </summary>
    SpaceStorage,
    /// <summary>
    /// 空间优势
    /// </summary>
    SpatialAdvantage,
    /// <summary>
    /// 空间霸权
    /// </summary>
    SpatialHegemony,
    /// <summary>
    /// 模糊的记忆
    /// </summary>
    DimMemory,
    /// <summary>
    /// 前世的回忆
    /// </summary>
    MemoriesPastLives,
    /// <summary>
    /// 昔日的记忆
    /// </summary>
    PastMemory,
    /// <summary>
    /// 神圣的回忆
    /// </summary>
    SacredMemory,
    /// <summary>
    /// 极耀圣念
    /// </summary>
    TheThoughts,
    /// <summary>
    /// 肌肉强化
    /// </summary>
    MuscleStrengthening,
    /// <summary>
    /// 技工天赋
    /// </summary>
    Artisanship,
    /// <summary>
    /// 注重细节
    /// </summary>
    AttentionDetail,
    /// <summary>
    /// 一丝不苟
    /// </summary>
    meticulous,


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
