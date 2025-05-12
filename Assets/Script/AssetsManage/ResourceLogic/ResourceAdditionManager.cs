using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

/// <summary>
/// 资源的全局加成
/// </summary>
public class ResourceAdditionManager : MonoBehaviour
{

    ////////////////////////////////////////////////////资源类////////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        Instance = this;
    }
    double tool = 1;//挖矿工具加成
    double miningWorker = 1;//采矿工人加成
    double worker = 1;//员工加成
    double MineBonus = 1;//矿车的额外加成

    double fabricator = 1;//制造工人加成

    double stoneFactory = 1;//石料工厂加成
    double copperWorks = 1;//铜矿工厂加成
    double cementFactory = 1;//水泥工厂加成
    double alloyFactory = 1;//合金工厂加成


    double collectorMarkup = 1;//采集器加成
    double science = 1;//科技点加成

    double technological = 1;//科技点上限

    double factory = 1;//工厂加成

    ////////////////////////////////////////////////////专项加成////////////////////////////////////////////////////////////////////////////
    double minerStone = 1;//石矿专项加成
    double CopperMine = 1;//铜矿专项加成
    double IronMine = 1;//铁矿专项加成
    double cement = 1;//水泥的专项加成
    double steel = 1;//钢的专项加成
    double aluminum = 1;//铝的专项加成

    double titanium = 1;//钛的专项加成


    double mithril = 1;//秘银的专项加成

    double neutron = 1;//中子专项加成


    ////////////////////////////////////////////////////房屋类////////////////////////////////////////////////////////////////////////////
    double tenementComfort = 1;//房屋坚固程度加成
    double tenementBasics = 1;//房屋基础加成
    double tenementRent = 1;//房屋房租加成

    double tavernrmb = 1;//酒馆软妹币加成

    double tenementSaveMoney = 1;//房屋软妹币储量加成
    double container = 1;//集装箱加成
    double RMBboost = 1;//软妹币总产量
    double excavator = 1;//挖掘机提供的加成的加成
    double bankReserveSurcharge = 1;//银行储量加成

    double allReserves = 1;//所有储量加成

    double RegenerateCrystalSpaceBonus = 1;//重生晶体对储量上限效果提升

    double allAssets = 1;//所有资源的产量

    double altar = 1;//祭坛的效率提升

    double power = 1;//玩家力量加成

    double combatPower = 1;//战斗力加成

    double trainingCampEfficiency = 1;//训练营效率

    double temple = 1;//寺庙效率

    double reserve = 1;//储备站加成
    double stash = 1;//仓库加成

    double marsResearch = 1;//火星研究站的效率

    double lunarMaterialStation = 1;//月球物资站加成

    double travel = 1;//旅游加成

    int colonization = 0;//额外殖民加成

    double pureGold = 1;//精金的加成


    public OreCarManager oreCarManager;//矿车管理
    public MineralScreeningMachineManager mineralScreeningMachineManager;//矿物筛选器
    public BlastFurnaceManager blastFurnaceManager;//高炉
    public ContainerManager containerManager;//集装箱加成
    public BankManager bankManager;//银行

    public StoneMillManager stoneMillManager;//石料加工厂

    public CopperMillManager copperMillManager;//铜矿加工厂

    public CementMillManager cementMillManager;//水泥加工厂

    public TavernManager tavernManager;//酒馆

    public MetalRefineryManager metalRefineryManager;//金属精炼厂

    public MaterialCompressorManager materialCompressorManager;//物质压缩器

    public StockExchangeManager stockExchangeManager;//证券交易所

    public ArtificialMineManager artificialMineManager;//人造矿井

    public GeocentricResearchInstituteManager geocentricResearchInstituteManager;//地心研究所
    public AltarManager altarManager;//祭坛
    public TempleManager templeManager;//寺庙

    public GiantMonumentManager giantMonumentManager;//纪念碑

    public ArtificialSatelliteManager artificialSatelliteManager;//人造卫星


    public LunarMaterialStationManager lunarMaterialStationManager;//月球物资站

    public LunarDataStationManager lunarDataStationManager;//月球资料站

    public MarsSpaceStationManager marsSpaceStationManager;//火星空间站

    public MarsResearchStationManager marsResearchStationManager;//火星研究台

    public DistributedWarehousingNetworkManager distributedWarehousingNetworkManager;//分布式仓储网络

    public InterstellarTradeHubManager interstellarTradeHubManager;//星际交易枢纽


    public InterstellarResearchInstituteManager interstellarResearchInstituteManager;//星际研究所

    public RingWorldManager ringWorldManager;//环世界

    public static ResourceAdditionManager Instance { get; private set; }


    /// <summary>
    /// 初始化所有加成数据
    /// </summary>
    public void Initialize()
    {
        // 获取当前类的所有字段
        FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (FieldInfo field in fields)
        {
            // 只处理 double 类型的字段
            if (field.FieldType == typeof(double))
            {
                field.SetValue(this, 1.0);
            }
        }
    }

    // 以下是原有的各种提升和获取加成的方法，保持不变
    /// <summary>
    /// 提升挖矿工具加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTool(double count)
    {
        tool *= 1 + count;
    }

    /// <summary>
    /// 获取挖矿工具加成
    /// </summary>
    public double GetToolUp()
    {
        double basics = 0; //基础加成是0
        basics += tool;
        return basics;
    }

    /// <summary>
    /// 提升采矿工人加成
    /// </summary>
    public void AddMiningWorker(double count)
    {
        miningWorker *= 1 + count;
    }
    /// <summary>
    /// 获取采矿工人加成
    /// </summary>
    /// <returns></returns>
    public double GetMiningWorkerUp()
    {
        double basics = 0; //基础加成是0
        basics += miningWorker;   //采矿工人基础加成
        basics += oreCarManager.GetOreCarUp();//矿车提升的采矿工人加成
        return basics;
    }
    //↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓专项加成↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    /// <summary>
    /// 提升石头专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddMinerStone(double count)
    {
        minerStone *= 1 + count;
    }
    /// <summary>
    /// 获取石头专项加成
    /// </summary>
    /// <returns></returns>
    public double GetMinerStoneUp()
    {
        double basics = 0; //基础加成是0
        basics += minerStone;   //大锤采集加成（石头

        basics += stoneMillManager.GetStoneMillUp();//加上石料加工厂提升

        return basics;
    }


    /// <summary>
    /// 提升铜矿专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddMinerCopper(double count)
    {
        CopperMine *= 1 + count;
    }
    /// <summary>
    /// 获取铜矿专项加成
    /// </summary>
    /// <returns></returns>
    public double GetMineCopperUp()
    {
        double basics = 0; //基础加成是0
        basics += CopperMine;   //铜矿专项加成
        basics += mineralScreeningMachineManager.GetscreenUp();//矿物筛选器加成

        basics += copperMillManager.GetCopperMillUp();//加上铜矿加工厂提升
        return basics;
    }

    /// <summary>
    /// 提升铁矿专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddIronMine(double count)
    {
        IronMine *= 1 + count;
    }
    /// <summary>
    /// 获取铁矿专项加成
    /// </summary>
    /// <returns></returns>
    public double GetIronMineUp()
    {
        double basics = 0; //基础加成是0
        basics += IronMine;   //铁矿专项加成
        basics += mineralScreeningMachineManager.GetscreenUp();//矿物筛选器加成
        basics += blastFurnaceManager.GetBlastFurnaceUp();//高炉加成

        return basics;
    }

    /// <summary>
    /// 提升水泥专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddCement(double count)
    {
        cement *= 1 + count;
    }
    /// <summary>
    /// 获取水泥专项加成
    /// </summary>
    /// <returns></returns>
    public double GetCementUp()
    {
        double basics = 0; //基础加成是0
        basics += cement;   //水泥专项加成

        basics += cementMillManager.GetCementMillUp();//水泥加工厂提升
        return basics;
    }


    /// <summary>
    /// 提升钢专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddSteel(double count)
    {
        steel *= 1 + count;
    }
    /// <summary>
    /// 获取钢专项加成
    /// </summary>
    /// <returns></returns>
    public double GetSteelUp()
    {
        double basics = 0; //基础加成是0
        basics += steel;   //钢专项加成

        return basics;
    }



    /// <summary>
    /// 提升铝专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddAluminum(double count)
    {
        aluminum *= 1 + count;
    }
    /// <summary>
    /// 获取铝专项加成
    /// </summary>
    /// <returns></returns>
    public double GetAluminumUp()
    {
        double basics = 0; //基础加成是0
        basics += aluminum;   //铝专项加成

        basics += metalRefineryManager.GetMetalRefineryUp();//金属精炼厂的提升

        return basics;
    }
    /// <summary>
    /// 提升钛专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTitanium(double count)
    {
        titanium *= 1 + count;
    }
    /// <summary>
    /// 获取钛专项加成
    /// </summary>
    /// <returns></returns>
    public double GetTitaniumUp()
    {
        double basics = 0; //基础加成是0
        basics += titanium;   //钛专项加成

        return basics;
    }



    //↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑专项加成↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    //↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓仓储加成↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓





    /// <summary>
    /// 提升房屋软妹币储量加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTenementSaveMoney(double count)
    {
        tenementSaveMoney *= 1 + count;
    }
    /// <summary>
    /// 获取房屋软妹币储量加成
    /// </summary>
    /// <returns></returns>
    public double GetTenementSaveMoneyUp()
    {
        double basics = 0; //基础加成是0
        basics += tenementSaveMoney;   //矿洞员工加成

        return basics;
    }
    //↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑仓储加成↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑





    /// <summary>
    /// 提升房屋坚固程度加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTenementComfort(double count)
    {
        tenementComfort *= 1 + count;
    }
    /// <summary>
    /// 获取房屋坚固程度 = 房屋总加成
    /// </summary>
    /// <returns></returns>
    public double GetTenementComfortUp()
    {
        double basics = 0; //基础加成是0
        basics += tenementComfort;   //房屋坚固程度加成

        basics += tavernManager.GetUp();//提升房屋产量
        return basics;
    }

    /// <summary>
    /// 提升房屋基础加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTenementBasics(double count)
    {
        tenementBasics *= 1 + count;
    }
    /// <summary>
    /// 获取房屋基础加成
    /// </summary>
    /// <returns></returns>
    public double GetTenementBasicsUp()
    {
        double basics = 0; //基础加成是0
        basics += tenementBasics;   //房屋坚固程度加成
        return basics;
    }


    /// <summary>
    /// 提升房屋房租加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTenementRent(double count)
    {
        tenementRent *= 1 + count;
    }
    /// <summary>
    /// 获取房屋房租加成
    /// </summary>
    /// <returns></returns>
    public double GetTenementRentUp()
    {
        double basics = 0; //基础加成是0
        basics += tenementRent;   //房屋坚固程度加成
        return basics;
    }


    /// <summary>
    /// 提升矿洞员工加成
    /// </summary>
    /// <param name="count"></param>
    public void AddWorker(double count)
    {
        worker *= 1 + count;
    }
    /// <summary>
    /// 获取矿洞员工加成
    /// </summary>
    /// <returns></returns>
    public double GetWorkerUp()
    {
        double basics = 0; //基础加成是0
        basics += worker;   //矿洞员工加成
        return basics;
    }
    /// <summary>
    /// 提升矿车加成的加成
    /// </summary>
    /// <param name="count"></param>
    public void AddMineBonus(double count)
    {
        MineBonus *= 1 + count;
    }
    /// <summary>
    /// 获取矿车加成的加成
    /// </summary>
    /// <returns></returns>
    public double GetMineBonusUp()
    {
        double basics = 0; //基础加成是0
        basics += MineBonus;   //矿车加成
        return basics;
    }





    /// <summary>
    /// 提升软妹币加成
    /// </summary>
    /// <param name="count"></param>
    public void AddRMBboost(double count)
    {
        RMBboost *= 1 + count;
    }
    /// <summary>
    /// 获取软妹币加成
    /// </summary>
    /// <returns></returns>
    public double GetRMBboostUp()
    {
        double basics = 0; //基础加成是0
        basics += RMBboost;   //软妹币加成

        basics += bankManager.GetUp();

        basics += stockExchangeManager.GetYieldUp();
        basics += interstellarTradeHubManager.GetYieldUp();

        return basics;
    }

    /// <summary>
    /// 提升集装箱加成
    /// </summary>
    /// <param name="count"></param>
    public void AddContainer(double count)
    {
        container *= 1 + count;
    }
    /// <summary>
    /// 获取集装箱加成
    /// </summary>
    /// <returns></returns>
    public double GetContainerUp()
    {
        double basics = 0; //基础加成是0
        basics += container;   //集装箱加成
        basics += materialCompressorManager.GeUp();
        return basics;
    }






    /// <summary>
    /// 提升挖掘机加成
    /// </summary>
    /// <param name="count"></param>
    public void AddExcavator(double count)
    {
        excavator *= 1 + count;
    }
    /// <summary>
    /// 获取挖掘机加成
    /// </summary>
    /// <returns></returns>
    public double GetExcavatorUp()
    {
        double basics = 0; //基础加成是0
        basics += excavator;   //挖掘机加成
        return basics;
    }




    /// <summary>
    /// 提升银行储量加成
    /// </summary>
    /// <param name="count"></param>
    public void AddBankReserveSurcharge(double count)
    {
        bankReserveSurcharge *= 1 + count;
    }
    /// <summary>
    /// 获取银行储量加成
    /// </summary>
    /// <returns></returns>
    public double GetBankReserveSurchargeUp()
    {
        double basics = 0; //基础加成是0
        basics += bankReserveSurcharge;   //银行储量加成
        return basics;
    }


    /// <summary>
    /// 提升所有储量加成
    /// </summary>
    /// <param name="count"></param>
    public void AddAllReserves(double count)
    {
        allReserves *= 1 + count;
    }
    /// <summary>
    /// 获取所有储量加成
    /// </summary>
    /// <returns></returns>
    public double GetAllReservesUp()
    {
        double basics = 0; //基础加成是0
        basics += allReserves;   //所有储量加成
        basics += GetSecondLifeReservesUp();
        basics *= 1 + containerManager.GetUp();//计算集装箱的储量加成

        return basics;
    }



    /// <summary>
    /// 获取重生水晶储量加成
    /// </summary>
    /// <returns></returns>
    public double GetSecondLifeReservesUp()
    {
        double basics = 0; //基础加成是0
        if (TechManager.Instance.GetTechFlag(TechType.SpaceStorage))
        {
            //每个重生晶体提升0.0004
            double count = ResourceManager.Instance.GetResource(ResourceType.RegeneratedCrystal);
            count *= 0.0004;
            count *= GetRegenerateCrystalSpaceBonusUp();//获取重生晶体对储量上限效果提升加成
            basics += count;//计算重生晶体的储量加成
        }
        return basics;
    }





    /// <summary>
    /// 提升重生晶体对储量上限效果提升
    /// </summary>
    /// <param name="count"></param>
    public void AddRegenerateCrystalSpaceBonus(double count)
    {
        RegenerateCrystalSpaceBonus *= 1 + count;
    }
    /// <summary>
    /// 获取重生晶体对储量上限效果提升加成
    /// </summary>
    /// <returns></returns>
    public double GetRegenerateCrystalSpaceBonusUp()
    {
        double basics = 0; //基础加成是0
        basics += RegenerateCrystalSpaceBonus;   //重生晶体对储量上限效果提升

        return basics;
    }




    /// <summary>
    /// 提升力量对挖矿的提升
    /// </summary>
    /// <param name="count"></param>
    public void AddPower(double count)
    {
        power *= 1 + count;
    }
    /// <summary>
    /// 获取力量对挖矿加成
    /// </summary>
    /// <returns></returns>
    public double GetPowerUp()
    {
        double basics = 0; //基础加成是0
        basics += power;   //重生晶体对储量上限效果提升

        return basics;
    }



    /// <summary>
    /// 提升制造工人的提升
    /// </summary>
    /// <param name="count"></param>
    public void AddFabricator(double count)
    {
        fabricator *= 1 + count;
    }
    /// <summary>
    /// 获取制造工人加成
    /// </summary>
    /// <returns></returns>
    public double GetFabricatorUp()
    {
        double basics = 0; //基础加成是0
        basics += fabricator;   //重生晶体对储量上限效果提升

        return basics;
    }


    /// <summary>
    /// 提升酒馆软妹币的提升
    /// </summary>
    /// <param name="count"></param>
    public void AddTavernrmb(double count)
    {
        tavernrmb *= 1 + count;
    }
    /// <summary>
    /// 获取酒馆软妹币的加成
    /// </summary>
    /// <returns></returns>
    public double GetTavernrmbUp()
    {
        double basics = 0; //基础加成是0
        basics += tavernrmb;   //重生晶体对储量上限效果提升

        return basics;
    }





    /// <summary>
    /// 提升石料工厂的提升
    /// </summary>
    /// <param name="count"></param>
    public void AddStoneFactory(double count)
    {
        stoneFactory *= 1 + count;
    }
    /// <summary>
    /// 获取石料工厂的加成
    /// </summary>
    /// <returns></returns>
    public double GetStoneFactoryUp()
    {
        double basics = 0; //基础加成是0
        basics += stoneFactory;

        return basics;
    }

    /// <summary>
    /// 提升铜矿工厂的提升
    /// </summary>
    /// <param name="count"></param>
    public void AddCopperWorks(double count)
    {
        copperWorks *= 1 + count;
    }
    /// <summary>
    /// 获取铜矿工厂的加成
    /// </summary>
    /// <returns></returns>
    public double GetCopperWorksUp()
    {
        double basics = 0; //基础加成是0
        basics += copperWorks;

        return basics;
    }
    /// <summary>
    /// 提升水泥工厂的提升
    /// </summary>
    /// <param name="count"></param>
    public void AddCementFactory(double count)
    {
        cementFactory *= 1 + count;
    }
    /// <summary>
    /// 获取水泥工厂的加成
    /// </summary>
    /// <returns></returns>
    public double GetCementFactoryUp()
    {
        double basics = 0; //基础加成是0
        basics += cementFactory;

        return basics;
    }

    /// <summary>
    /// 提升合金工厂的提升
    /// </summary>
    /// <param name="count"></param>
    public void AddAlloyFactory(double count)
    {
        alloyFactory *= 1 + count;
    }
    /// <summary>
    /// 获取合金工厂的加成
    /// </summary>
    /// <returns></returns>
    public double GetAlloyFactoryUp()
    {
        double basics = 0; //基础加成是0
        basics += alloyFactory;

        return basics;
    }


    /// <summary>
    /// 提升采集器的提升
    /// </summary>
    /// <param name="count"></param>
    public void AddCollectorMark(double count)
    {
        collectorMarkup *= 1 + count;
    }
    /// <summary>
    /// 获取采集器的加成
    /// </summary>
    /// <returns></returns>
    public double GetCollectorMarkUp()
    {
        double basics = 0; //基础加成是0
        basics += collectorMarkup;
        basics += artificialMineManager.GetUp();//人造矿井的提升
        return basics;
    }

    /// <summary>
    /// 提升科技点的提升
    /// </summary>
    /// <param name="count"></param>
    public void AddScience(double count)
    {
        science *= 1 + count;
    }
    /// <summary>
    /// 获取科技点的加成
    /// </summary>
    /// <returns></returns>
    public double GetScienceUp()
    {
        double basics = 0; //基础加成是0
        basics += science;
        basics += geocentricResearchInstituteManager.GetUp();//地心研究所的提升
        basics += artificialSatelliteManager.GetUp();//人造卫星的提升
        basics += marsResearchStationManager.GetUp();//火星研究台的提升
        basics += interstellarResearchInstituteManager.GetUp();//星际研究所的提升

        return basics;
    }



    /// <summary>
    /// 提升所有资源产量
    /// </summary>
    /// <param name="count"></param>
    public void AddAllAssets(double count)
    {
        allAssets *= 1 + count;
    }
    /// <summary>
    /// 获取所有资源产量加成
    /// </summary>
    /// <returns></returns>
    public double GetAllAssetsUp()
    {
        double basics = 0; //基础加成是0
        basics += allAssets;
        basics += altarManager.GetUp();//祭坛的提升
        basics += templeManager.GetUp();//寺庙的提升
        basics += ringWorldManager.GetUp();//环世界的提升


        return basics;
    }


    /// <summary>
    /// 提升祭坛的效率
    /// </summary>
    /// <param name="count"></param>
    public void AddAltar(double count)
    {
        altar *= 1 + count;
    }


    /// <summary>
    /// 获取祭坛的效率加成
    /// </summary>
    /// <returns></returns>
    public double GetAltarUp()
    {
        double basics = 0; //基础加成是0
        basics += altar;
        basics += giantMonumentManager.GetUp();//纪念碑的提升
        return basics;
    }


    /// <summary>
    /// 提升工厂的效率
    /// </summary>
    /// <param name="count"></param>
    public void AddFactory(double count)
    {
        factory *= 1 + count;
    }
    /// <summary>
    /// 获取工厂的效率加成
    /// </summary>
    /// <returns></returns>
    public double GetFactoryUp()
    {
        double basics = 0; //基础加成是0
        basics += factory;

        return basics;
    }



    /// <summary>
    /// 提升战斗力加成
    /// </summary>
    /// <param name="count"></param>
    public void AddCombatPower(double count)
    {
        combatPower *= 1 + count;
    }
    /// <summary>
    /// 获取战斗力加成
    /// </summary>
    /// <returns></returns>
    public double GetCombatPowerUp()
    {
        double basics = 0; //基础加成是0
        basics += combatPower;

        return basics;
    }





    /// <summary>
    /// 提升训练营效率
    /// </summary>
    /// <param name="count"></param>
    public void AddTrainingCampEfficiency(double count)
    {
        trainingCampEfficiency *= 1 + count;
    }
    /// <summary>
    /// 获取训练营加成
    /// </summary>
    /// <returns></returns>
    public double GetTrainingCampEfficiencyUp()
    {
        double basics = 0; //基础加成是0
        basics += trainingCampEfficiency;

        return basics;
    }


    /// <summary>
    /// 提升寺庙效率
    /// </summary>
    /// <param name="count"></param>
    public void AddTemple(double count)
    {
        temple *= 1 + count;
    }
    /// <summary>
    /// 获取寺庙加成
    /// </summary>
    /// <returns></returns>
    public double GetTempleUp()
    {
        double basics = 0; //基础加成是0
        basics += temple;

        return basics;
    }




    /// <summary>
    /// 提升储备站加成
    /// </summary>
    /// <param name="count"></param>
    public void AddReserve(double count)
    {
        reserve *= 1 + count;
    }
    /// <summary>
    /// 获取储备站加成
    /// </summary>
    /// <returns></returns>
    public double GetReserveUp()
    {
        double basics = 0; //基础加成是0
        basics += reserve;
        basics *= lunarMaterialStationManager.GetUp();
        basics *= distributedWarehousingNetworkManager.GetReservesUp();
        return basics;
    }



    /// <summary>
    /// 提升仓库加成
    /// </summary>
    /// <param name="count"></param>
    public void AddStash(double count)
    {
        stash *= 1 + count;
    }
    /// <summary>
    /// 获取仓库加成
    /// </summary>
    /// <returns></returns>
    public double GetStashUp()
    {
        double basics = 0; //基础加成是0
        basics += stash;
        basics *= distributedWarehousingNetworkManager.GetReservesUp();
        return basics;
    }



    /// <summary>
    /// 提升科技上限加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTechnological(double count)
    {
        technological *= 1 + count;
    }
    /// <summary>
    /// 获取科技上限加成
    /// </summary>
    /// <returns></returns>
    public double GetTechnologicalUp()
    {
        double basics = 0; //基础加成是0
        basics += technological;
        basics += lunarDataStationManager.GetUp();
        return basics;
    }


    /// <summary>
    /// 提升月球物资站加成
    /// </summary>
    /// <param name="count"></param>
    public void AddLunarMaterialStation(double count)
    {
        lunarMaterialStation *= 1 + count;
    }
    /// <summary>
    /// 获取月球物资站加成
    /// </summary>
    /// <returns></returns>
    public double GetLunarMaterialStationUp()
    {
        double basics = 0; //基础加成是0
        basics += lunarMaterialStation;

        return basics;
    }



    /// <summary>
    /// 提升旅游加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTravel(double count)
    {
        travel *= 1 + count;
    }
    /// <summary>
    /// 获取旅游加成
    /// </summary>
    /// <returns></returns>
    public double GetTravelUp()
    {
        double basics = 0; //基础加成是0
        basics += travel;

        return basics;
    }

    // <summary>
    /// 提升额外殖民点数
    /// </summary>
    /// <param name="count"></param>
    public void AddColonization(int count)
    {
        colonization += count;
    }
    /// <summary>
    /// 获取额外殖民点数
    /// </summary>
    /// <returns></returns>
    public int GetColonizationUp()
    {
        int basics = 0; //基础加成是0
        basics += colonization;
        basics += marsSpaceStationManager.GetUp();
        return basics;
    }



    /// <summary>
    /// 提升秘銀加成
    /// </summary>
    /// <param name="count"></param>
    public void AddMithril(double count)
    {
        travel *= 1 + mithril;
    }
    /// <summary>
    /// 获取秘銀加成
    /// </summary>
    /// <returns></returns>
    public double GetMithrilUp()
    {
        double basics = 0; //基础加成是0
        basics += mithril;

        return basics;
    }


    /// <summary>
    /// 提升火星研究站加成
    /// </summary>
    /// <param name="count"></param>
    public void AddMarsResearch(double count)
    {
        travel *= 1 + marsResearch;
    }
    /// <summary>
    /// 获取火星研究站加成
    /// </summary>
    /// <returns></returns>
    public double GetMarsResearchUp()
    {
        double basics = 0; //基础加成是0
        basics += marsResearch;

        return basics;
    }




    /// <summary>
    /// 提升中子加成
    /// </summary>
    /// <param name="count"></param>
    public void AddNeutron(double count)
    {
        travel *= 1 + neutron;
    }
    /// <summary>
    /// 获取中子加成
    /// </summary>
    /// <returns></returns>
    public double GetNeutronUp()
    {
        double basics = 0; //基础加成是0
        basics += neutron;

        return basics;
    }



    /// <summary>
    /// 提升精金加成
    /// </summary>
    /// <param name="count"></param>
    public void AddPureGold(double count)
    {
        pureGold *= 1 + neutron;
    }
    /// <summary>
    /// 获取精金加成
    /// </summary>
    /// <returns></returns>
    public double GetPureGoldUP()
    {
        double basics = 0; //基础加成是0
        basics += pureGold;

        return basics;
    }



}