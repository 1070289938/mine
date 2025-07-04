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
    Temple,//寺庙
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
    /// 地心资料库
    /// </summary>
    DataBank,
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
    ///狂热信徒
    /// </summary>
    fanatic,
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
    /// 发现地心犬
    /// </summary>
    GeocentricDog,
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

    /// <summary>
    /// 登天计划
    /// </summary>
    LandingPlan,
    /// <summary>
    /// 太空电梯
    /// </summary>
    SpaceElevator,
    /// <summary>
    /// 太空电梯搭建完成
    /// </summary>
    CompletionConstruction,


    //==============================================太空科技===================================================

    /// <summary>
    /// 近太空探索
    /// </summary>
    NearSpaceExploration,


    /// <summary>
    /// 太空铁矿船
    /// </summary>
    SpaceIronShip,


    /// <summary>
    /// 太空铁
    /// </summary>
    SpaceTrain,
    /// <summary>
    /// 小行星捕获器
    /// </summary>
    AsteroidCatcher,
    /// <summary>
    /// 真空仓库
    /// </summary>
    VacuumWarehouse,

    /// <summary>
    /// 人造卫星
    /// </summary>
    ArtificialSatellite,
    /// <summary>
    /// 大气研究
    /// </summary>
    AtmosphericResearch,

    /// <summary>
    /// 真空实验
    /// </summary>
    VacuumLaboratory,

    /// <summary>
    /// 太空酒馆
    /// </summary>
    SpaceTavern,

    /// <summary>
    /// 零重力实验室
    /// </summary>
    ZeroGravityLaboratory,


    /// <summary>
    /// 零重力制造业
    /// </summary>
    ZeroGravityManufacturing,


    /// <summary>
    /// 太空生态系统
    /// </summary>
    ecosystem,

    /// <summary>
    /// 月球探索计划
    /// </summary>
    LunarExploration,

    /// <summary>
    /// 铱矿采集器
    /// </summary>
    DependentCollector,
    /// <summary>
    /// 月球物资站
    /// </summary>
    LunarMaterialStation,

    /// <summary>
    /// 月心储物
    /// </summary>
    MoonHeartStorage,


    /// <summary>
    /// 铱钻
    /// </summary>
    IridiumDiamond,
    /// <summary>
    /// 铱金库
    /// </summary>
    IridiumVault,

    /// <summary>
    /// 铱仓库
    /// </summary>
    IridiumDepot,


    /// <summary>
    /// 铱制武装
    /// </summary>
    IridiumArmor,

    /// <summary>
    /// 秘银锻造
    /// </summary>
    MithrilForging,

    /// <summary>
    /// 秘银钻
    /// </summary>
    MithrilDiamond,

    /// <summary>
    /// 秘银金库
    /// </summary>
    MithrilVault,


    /// <summary>
    /// 秘银集装箱
    /// </summary>
    MithrilContainer,


    /// <summary>
    /// 秘银武装
    /// </summary>
    MithrilArmament,


    /// <summary>
    /// 月球资料站
    /// </summary>
    LunarDataStation,

    /// <summary>
    /// 星际合金
    /// </summary>
    InterstellarAlloy,

    /// <summary>
    /// 月球研究
    /// </summary>
    LunarResearch,

    /// <summary>
    /// 涡旋钛熔工艺
    /// </summary>
    TitaniumProcess,
    /// <summary>
    /// 银矿探测技术
    /// </summary>
    SilverTechnology,
    /// <summary>
    /// 月球酒馆
    /// </summary>
    MoonTavern,

    /// <summary>
    /// 月球房屋
    /// </summary>
    MoonHouse,


    /// <summary>
    /// 月球加工厂
    /// </summary>
    LunarProcessingPlant,

    /// <summary>
    /// 月球物流
    /// </summary>
    LunarLogistics,

    /// <summary>
    /// 远航技术
    /// </summary>
    OceangoingTechnology,

    /// <summary>
    /// 火星探索
    /// </summary>
    MarsExploration,

    /// <summary>
    /// 火星殖民船
    /// </summary>
    MarsColonyShip,

    /// <summary>
    /// 火星殖民地
    /// </summary>
    MarsColony,

    /// <summary>
    /// 星际旅游
    /// </summary>
    InterstellarTourism,

    /// <summary>
    /// 火星旅游站
    /// </summary>
    MarsTouristStation,

    /// <summary>
    /// 旅游一条路服务
    /// </summary>
    TravelService,

    /// <summary>
    /// 火星工业
    /// </summary>
    MarsIndustry,

    /// <summary>
    /// 火星空间站
    /// </summary>
    MarsSpaceStation,

    /// <summary>
    /// 行星航道
    /// </summary>
    PlanetaryChannel,

    /// <summary>
    /// 引力弹弓
    /// </summary>
    GravitySlingshot,

    /// <summary>
    /// 空间站仓储
    /// </summary>
    SpaceStationStorage,

    /// <summary>
    /// 火星研究站
    /// </summary>
    MarsResearchStation,

    /// <summary>
    /// AI
    /// </summary>
    AI,

    /// <summary>
    /// 技能机床
    /// </summary>
    IntelligentMachineTool,

    /// <summary>
    /// 精化秘银
    /// </summary>
    RefinedMithril,

    /// <summary>
    /// AI导游
    /// </summary>
    AITourGuide,

    /// <summary>
    /// 智能仓储
    /// </summary>
    IntelligentStorage,

    /// <summary>
    /// 火星储物站
    /// </summary>
    MarsStorageStation,

    /// <summary>
    /// 火星地理研究
    /// </summary>
    MarsGeographicalResearch,

    /// <summary>
    /// 高危物质研究
    /// </summary>
    HighRiskSubstanceResearch,

    /// <summary>
    /// 中子采集
    /// </summary>
    NeutronAcquisition,

    /// <summary>
    /// 中子采集器
    /// </summary>
    NeutronCollector,

    /// <summary>
    /// 中子屋
    /// </summary>
    NeutronHouse,

    /// <summary>
    /// 中子金库
    /// </summary>
    ChineseTreasury,

    /// <summary>
    /// 中子研究室
    /// </summary>
    NeutronLaboratory,

    /// <summary>
    /// 中子储物
    /// </summary>
    NeutronStorage,

    /// <summary>
    /// 粒子波采集
    /// </summary>
    ParticleWaveAcquisition,

    /// <summary>
    /// 曲率技术
    /// </summary>
    CurvatureTechnique,

    /// <summary>
    /// 跃迁引擎
    /// </summary>
    TransitionEngine,

    /// <summary>
    /// 太空制造业
    /// </summary>
    SpaceManufacturingIndustry,

    /// <summary>
    /// 太空工厂
    /// </summary>
    SpaceFactory,

    /// <summary>
    /// 太空船坞
    /// </summary>
    SpaceDock,
    /// <summary>
    /// 完成船坞
    /// </summary>
    CompleteSpaceDock,
    /// <summary>
    /// 实验性探索者
    /// </summary>
    ExperimentalExplorer,

    /// <summary>
    /// 完成探索者的搭建
    /// </summary>
    CompleteExperimentalExplorer,



    /// <summary>
    /// 气态采集
    /// </summary>
    GaseousCollection,

    /// <summary>
    /// 奥德修斯号
    /// </summary>
    Odysseus,
    /// <summary>
    /// 完成奥德修斯号的搭建
    /// </summary>
    CompleteOdysseus,
    /// <summary>
    /// 计算木星轨道
    /// </summary>
    CalculatingJupiterOrbit,

    /// <summary>
    /// 探索木星
    /// </summary>
    ExploringJupiter,

    /// <summary>
    /// 轨道采集器
    /// </summary>
    OrbitCollector,

    /// <summary>
    /// 金属氢采集器
    /// </summary>
    MetallicHydrogenCollector,

    /// <summary>
    /// 超导体
    /// </summary>
    Superconductor,

    /// <summary>
    /// 改良跃迁引擎
    /// </summary>
    ImprovedJumpEngine,

    /// <summary>
    /// 启动改良型探索者
    /// </summary>
    StartImprovedExplorer,

    //==============================================外星域科技===================================================


    /// <summary>
    /// 探索半人马座
    /// </summary>
    ExploringCentaurus,

    /// <summary>
    /// 深空信标
    /// </summary>
    DeepSpaceBeacon,

    /// <summary>
    /// 耀斑矿研究
    /// </summary>
    FlareMineralResearch,

    /// <summary>
    /// 耀斑矿采集器
    /// </summary>
    FlareMineralCollector,

    /// <summary>
    /// 高能激光
    /// </summary>
    HighEnergyLaser,

    /// <summary>
    /// 超脉冲激光器
    /// </summary>
    UltraPulseLaser,

    /// <summary>
    /// 激光机床
    /// </summary>
    LaserMachineTool,

    /// <summary>
    /// 激光步枪
    /// </summary>
    LaserRifle,

    /// <summary>
    /// 激光采矿
    /// </summary>
    LaserMining,
    /// <summary>
    /// 激光防卫陷阱
    /// </summary>
    LaserTrap,
    /// <summary>
    /// 精金采集
    /// </summary>
    AdamantiteCollection,

    /// <summary>
    /// 精金采集器
    /// </summary>
    AdamantiteCollector,

    /// <summary>
    /// 精金集装箱
    /// </summary>
    AdamantiteContainer,

    /// <summary>
    /// 精金钻
    /// </summary>
    AdamantiteDrill,

    /// <summary>
    /// 精金仓库
    /// </summary>
    AdamantiteWarehouse,
    /// <summary>
    /// 精金护具
    /// </summary>
    AdamantiteArmor,
    /// <summary>
    /// 精金定位罗盘
    /// </summary>
    AdamantitePositioningCompass,

    /// <summary>
    /// 精金量子分析仪
    /// </summary>
    AdamantiteQuantumAnalyzer,

    /// <summary>
    /// 外星材料实验室
    /// </summary>
    ExtraterrestrialMaterialLaboratory,

    /// <summary>
    /// 采矿无人机
    /// </summary>
    MiningDrone,

    /// <summary>
    /// AI星际物流
    /// </summary>
    AIInterstellarLogistics,

    /// <summary>
    /// 量子技术
    /// </summary>
    QuantumTechnology,

    /// <summary>
    /// 量子探矿仪
    /// </summary>
    QuantumProspector,

    /// <summary>
    /// 量子计算机
    /// </summary>
    QuantumComputer,

    /// <summary>
    /// 量子储存
    /// </summary>
    QuantumStorage,

    /// <summary>
    /// 子空间星标
    /// </summary>
    SubspaceStarMarker,

    /// <summary>
    /// 星际万维网
    /// </summary>
    InterstellarWorldWideWeb,
    /// <summary>
    /// 精金保险库
    /// </summary>
    VaultPureGold,
    /// <summary>
    /// 仓储机械臂
    /// </summary>
    WarehousingRoboticArm,

    /// <summary>
    /// 超导轨道
    /// </summary>
    SuperconductingOrbit,

    /// <summary>
    /// 量子云经济
    /// </summary>
    QuantumCloudEconomy,
    /// <summary>
    /// 信用芯片
    /// </summary>
    CreditChip,

    /// <summary>
    /// 量子加密网
    /// </summary>
    QuantumEncryptionNetwork,

    /// <summary>
    /// 智能调度中枢
    /// </summary>
    IntelligentSchedulingHub,

    /// <summary>
    /// 等离子熔炉
    /// </summary>
    PlasmaFurnace,

    /// <summary>
    /// 全系储仓图
    /// </summary>
    HolographicWarehouseMap,

    /// <summary>
    /// 金融预测器
    /// </summary>
    FinancialPredictor,

    /// <summary>
    /// 分布式仓储网络
    /// </summary>
    DistributedWarehousingNetwork,

    /// <summary>
    /// 探测虫洞
    /// </summary>
    DetectingWormhole,

    /// <summary>
    /// 重力压缩仓
    /// </summary>
    GravityCompressionChamber,

    /// <summary>
    /// 虫洞跃迁计划
    /// </summary>
    WormholeJumpPlan,

    /// <summary>
    /// 搭建星渊号
    /// </summary>
    ConstructingXingyuanShip,

    /// <summary>
    /// 深空信号探测
    /// </summary>
    DeepSpaceSignalDetection,

    /// <summary>
    /// 试图联系未知信号
    /// </summary>
    AttemptingtoContactUnknownSignal,

    /// <summary>
    /// 翻译语言
    /// </summary>
    TranslatingLanguage,

    /// <summary>
    /// 试图交好
    /// </summary>
    AttemptingtoMakeFriendly,

    /// <summary>
    /// 建立大使馆
    /// </summary>
    EstablishEmbassy,

    /// <summary>
    /// 外星商业
    /// </summary>
    ExtraterrestrialBusiness,

    /// <summary>
    /// 星际贸易枢纽
    /// </summary>
    InterstellarTradeHub,

    /// <summary>
    /// 通用物流
    /// </summary>
    GeneralLogistics,

    /// <summary>
    /// 星际商人
    /// </summary>
    InterstellarMerchant,

    /// <summary>
    /// 外星技术分享
    /// </summary>
    ExtraterrestrialTechnologySharing,

    /// <summary>
    /// 星际研究所
    /// </summary>
    InterstellarResearchInstitute,

    /// <summary>
    /// 反重力仓储
    /// </summary>
    AntiGravityWarehousing,

    /// <summary>
    /// 环世界
    /// </summary>
    RingWorld,

    /// <summary>
    /// 星际之门
    /// </summary>
    Stargate,

    /// <summary>
    /// 完成深渊号的搭建
    /// </summary>
    CompleteAbyss,



    /// <summary>
    /// 引力波增幅器
    /// </summary>
    Gravitational,
    /// <summary>
    /// 量子纠缠导航仪
    /// </summary>
    EntanglementNavigator,
    /// <summary>
    /// 光子护盾生成器
    /// </summary>
    PhotonShield,
    /// <summary>
    /// 反物质湮灭引擎
    /// </summary>
    AnnihilationEngine,
    /// <summary>
    /// 进入虫洞
    /// </summary>
    WormholeEnter,



    //==============================================暮晶科技===================================================






    /// <summary>
    /// 探索暮晶星系
    /// </summary>
    ExploreTwilightGalaxy,

    /// <summary>
    /// 分析太空空间
    /// </summary>
    AnalyzeSpace,

    /// <summary>
    /// 查找信号干扰源
    /// </summary>
    FindSignalInterferenceSource,

    /// <summary>
    /// 启用飞船自检仪器
    /// </summary>
    ActivateShipSelfCheck,

    /// <summary>
    /// 尝试重新激活
    /// </summary>
    TryReactivate,

    /// <summary>
    /// 检查维生系统
    /// </summary>
    CheckLifeSupportSystem,

    /// <summary>
    /// 修复维生系统
    /// </summary>
    RepairLifeSupportSystem,

    /// <summary>
    /// 试图查找损坏原因
    /// </summary>
    AttemptToFindDamageCause,

    /// <summary>
    /// 清理暮晶
    /// </summary>
    CleanTwilightCrystals,

    /// <summary>
    /// 激活维生装置
    /// </summary>
    ActivateLifeSupportDevice,

    /// <summary>
    /// 研究暮晶
    /// </summary>
    StudyTwilightCrystals,

    /// <summary>
    /// 挖掘暮晶
    /// </summary>
    MineTwilightCrystals,

    /// <summary>
    /// 激活防护服
    /// </summary>
    ActivateProtectiveSuit,

    /// <summary>
    /// 激活动力装置
    /// </summary>
    ActivatePowerDevice,

    /// <summary>
    /// 激活工具车
    /// </summary>
    ActivateToolVehicle,

    /// <summary>
    /// 自动捕捉装置
    /// </summary>
    AutomaticCaptureDevice,

    /// <summary>
    /// 升级机械臂
    /// </summary>
    UpgradeMechanicalArm,

    /// <summary>
    /// 升级捕捉装置
    /// </summary>
    UpgradeCaptureDevice,

    /// <summary>
    /// 残存探测器
    /// </summary>
    ResidualDetector,

    /// <summary>
    /// 监听未知广播
    /// </summary>
    MonitorUnknownBroadcast,

    /// <summary>
    /// 解析广播语言
    /// </summary>
    ParseBroadcastLanguage,

    /// <summary>
    /// 探索异常点
    /// </summary>
    ExploreAnomalyPoint,

    /// <summary>
    /// 检查实验仓状态
    /// </summary>
    CheckLabChamberStatus,

    /// <summary>
    /// 光脉干扰控制台
    /// </summary>
    LightPulseJammingConsole,

    /// <summary>
    /// 完成光脉干扰控制台
    /// </summary>
    CompleteInterference,

    /// <summary>
    /// 研究基础科技
    /// </summary>
    StudyBasicTechnology,

    /// <summary>
    /// 研究冷冻科技
    /// </summary>
    StudyCryogenicTechnology,

    /// <summary>
    /// 研究光谱知识
    /// </summary>
    StudySpectralKnowledge,

    /// <summary>
    /// 研究时间知识
    /// </summary>
    StudyTemporalKnowledge,

    /// <summary>
    /// 修复1号实验仓
    /// </summary>
    RepairLabChamber1,

    /// <summary>
    /// 研究暮晶收集舱
    /// </summary>
    StudyTwilightCrystalHarvestBay,

    /// <summary>
    /// 激活1号晶体提取仪
    /// </summary>
    ActivateCrystalExtractor1,

    /// <summary>
    /// 激活2号晶体提取仪
    /// </summary>
    ActivateCrystalExtractor2,

    /// <summary>
    /// 实验仓库
    /// </summary>
    LaboratoryWarehouse,

    /// <summary>
    /// 培养室
    /// </summary>
    CultivationChamber,

    /// <summary>
    /// 时间结构简史
    /// </summary>
    BriefHistoryOfTemporalStructure,

    /// <summary>
    /// 光速结构器
    /// </summary>
    LightSpeedStructurer,

    /// <summary>
    /// 控制室
    /// </summary>
    ControlRoom,

    /// <summary>
    /// 探索矿层入口
    /// </summary>
    ExploreMineLayerEntrance,

    /// <summary>
    /// 检查冻晶共鸣
    /// </summary>
    CheckFrozenCrystalResonance,

    /// <summary>
    /// 查看矿内遗迹封印
    /// </summary>
    ViewMineRelicSeal,

    /// <summary>
    /// 记忆回溯
    /// </summary>
    MemoryBacktracking,

    /// <summary>
    /// 折叠记忆碎片
    /// </summary>
    FoldMemoryFragments,

    /// <summary>
    /// 记忆提炼炉建造
    /// </summary>
    ConstructMemoryRefiner,

    /// <summary>
    /// 启动科研交互台
    /// </summary>
    ActivateScientificInteractionDesk,

    /// <summary>
    /// 中枢模型拟合
    /// </summary>
    CentralModelFitting,

    /// <summary>
    /// 采集矩阵运行
    /// </summary>
    CollectionMatrixOperation,

    /// <summary>
    /// 启动晶锚维护工程
    /// </summary>
    InitiateCrystalAnchorMaintenance,

    /// <summary>
    /// 时锚构建指令启动
    /// </summary>
    TimeAnchorConstructionCommandStart,

    /// <summary>
    /// 第一锚核心
    /// </summary>
    AnchorCoreOne,

    /// <summary>
    /// 锚区内部采样
    /// </summary>
    InternalAnchorZoneSampling,

    /// <summary>
    /// 启动锚体复制程序
    /// </summary>
    StartAnchorReplicationProgram,

    /// <summary>
    /// 第二锚核心
    /// </summary>
    AnchorCoreTwo,

    /// <summary>
    /// 锚间互扰
    /// </summary>
    InterAnchorInterference,

    /// <summary>
    /// 锚频突变
    /// </summary>
    AnchorFrequencyMutation,

    /// <summary>
    /// 晶析装置初次启动
    /// </summary>
    FirstActivationOfCrystalAnalyzer,

    /// <summary>
    /// 记忆存储装置
    /// </summary>
    MemoryStorageDevice,

    /// <summary>
    /// 记忆副本结构出现
    /// </summary>
    MemoryCopyStructureEmergence,

    /// <summary>
    /// 记忆副本干扰模块
    /// </summary>
    MemoryCopyInterferenceModule,

    /// <summary>
    /// 译码程序
    /// </summary>
    DecodingProgram,

    /// <summary>
    /// 锚区虚像
    /// </summary>
    AnchorZoneVirtualImage,

    /// <summary>
    /// 构建记忆终端
    /// </summary>
    ConstructMemoryTerminal,

    /// <summary>
    /// 晶析装置
    /// </summary>
    CrystalAnalyzer,

    /// <summary>
    /// 激活记忆终端
    /// </summary>
    ActivateMemoryTerminal,

    /// <summary>
    /// 共鸣试炼
    /// </summary>
    ResonanceTrial,

    /// <summary>
    /// 时间祭坛
    /// </summary>
    TimeAltar,

    /// <summary>
    /// 祭坛试炼层一
    /// </summary>
    AltarTrialLevelOne,

    /// <summary>
    /// 完成祭坛试炼层一
    /// </summary>
    CompleteAltarTrialLevelOne,
    /// <summary>
    /// 时相投影装置
    /// </summary>
    TimePhaseProjector,

    /// <summary>
    /// 祭坛试炼层二
    /// </summary>
    AltarTrialLevelTwo,
    /// <summary>
    /// 完成祭坛试炼层二
    /// </summary>
    CompleteAltarTrialLevelTwo,

    /// <summary>
    /// 逻辑困阵
    /// </summary>
    LogicPuzzleArray,

    /// <summary>
    /// 祭坛试炼层三
    /// </summary>
    AltarTrialLevelThree,
    /// <summary>
    /// 完成祭坛试炼层三
    /// </summary>
    CompleteAltarTrialLevelThree,

    /// <summary>
    /// 幻象重叠区
    /// </summary>
    PhantasmOverlapZone,


    /// <summary>
    /// 前往坐标位置
    /// </summary>
    GoToCoordinatePosition,

    /// <summary>
    /// 研究巨型装置
    /// </summary>
    StudyGiantDevice,

    /// <summary>
    /// 嵌入四维宝石
    /// </summary>
    EmbedFourDimensionalGem,


    /// <summary>
    /// 拆解巨构
    /// </summary>
    Disassemble,




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
    /// 时空记忆
    /// </summary>
    SpatioMemory,
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

    /// <summary>
    /// 四维制品
    /// </summary>
    dimensionalProduct,
    /// <summary>
    /// 商业头脑
    /// </summary>
    BusinessAcumen,
    /// <summary>
    /// 企业家
    /// </summary>
    Entrepreneur,
    /// <summary>
    /// 四维仓库
    /// </summary>
    DimensionalWarehouse,
    /// <summary>
    /// 多维仓库
    /// </summary>
    MultidimensionalWarehouse,
    /// <summary>
    /// 信仰
    /// </summary>
    Faith,
    /// <summary>
    /// 敬拜
    /// </summary>
    Worship,
    /// <summary>
    /// 聪明头脑
    /// </summary>
    IntelligentMind,
    /// <summary>
    /// 科学天赋
    /// </summary>
    ScientificTalent,
    /// <summary>
    /// 天生科学家
    /// </summary>
    BornScientist,

    /// <summary>
    /// 多维集装箱
    /// </summary>
    DimensionalContainer,
    /// <summary>
    /// 亚空间集装箱
    /// </summary>
    SubspaceContainer,

    /// <summary>
    /// 天才
    /// </summary>
    Genius,

    /// <summary>
    /// 智囊
    /// </summary>
    ThinkTank,

    /// <summary>
    /// 黑心企业
    /// </summary>
    UnscrupulousEnterprise,

    /// <summary>
    /// 世界级野心
    /// </summary>
    WorldAmbition,


    /// <summary>
    /// 玄学
    /// </summary>
    Metaphysics,

    /// <summary>
    /// 采集器精通
    /// </summary>
    ProficientCollectors,

    /// <summary>
    /// 四维采集器
    /// </summary>
    DimensionalCollector,

    /// <summary>
    /// 飞升经验
    /// </summary>
    AscensionExperience,









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
